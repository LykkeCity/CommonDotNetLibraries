using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Common.Log;

namespace Common
{
    /// <summary>
    /// Aggregates items in FIFO queue and lets client asynchronously process them grouped to batches in single dedicated thread
    /// </summary>
    public sealed class BackgroundQueueBatchWorker<TItem> :
        IStartable, IStopable
    {
        private readonly BackgroundQueueWorker<TItem> _backgroundQueueWorker;
        private readonly string _componentName;
        private readonly ILog _log;
        private readonly int _maxBatchSize;
        private readonly TimeSpan _maxBatchAge;
        private readonly object _startStopLock;
        private readonly object _batchSwitchingLock;

        private bool _isStarted;
        private List<TItem> _batch;
        private Action<IReadOnlyCollection<TItem>> _handler;
        private CancellationTokenSource _stoppingTokenSource;
        private DateTime _batchDeathMoment;

        /// <param name="componentName">Component name used to log</param>
        /// <param name="log">Logger</param>
        /// <param name="maxItemsInWorkIteration">Max items count will be handled until working thread goes to sleep and lets other threads do their work</param>
        /// <param name="maxBatchSize">The number of items will be collected in single batch until it is handled</param>
        /// <param name="maxBatchAge">The batch age, when exceeded, will be handled</param>
        public BackgroundQueueBatchWorker(
            string componentName, 
            ILog log, 
            int maxItemsInWorkIteration = 1000, 
            int maxBatchSize = 100, 
            TimeSpan? maxBatchAge = null)
        {
            _componentName = componentName;
            _log = log;
            _maxBatchSize = maxBatchSize;
            _maxBatchAge = maxBatchAge ?? TimeSpan.FromSeconds(5);

            _backgroundQueueWorker = new BackgroundQueueWorker<TItem>(componentName, log, maxItemsInWorkIteration)
                .SetHandler(HandleItem);
            _startStopLock = new object();
            _batchSwitchingLock = new object();
        }

        public BackgroundQueueBatchWorker<TItem> SetBatchHandler(Action<IReadOnlyCollection<TItem>> handler)
        {
            _handler = handler;

            return this;
        }

        public void Start()
        {
            if (_isStarted)
            {
                return;
            }

            lock (_startStopLock)
            {
                if (_isStarted)
                {
                    return;
                }

                StartNewBatch();

                _stoppingTokenSource = new CancellationTokenSource();
                
                Task.Factory.StartNew(MonitorBatchAge, _stoppingTokenSource.Token);
                
                _backgroundQueueWorker.Start();
                
                _isStarted = true;
            }
        }

        public void Stop()
        {
            if (!_isStarted)
            {
                return;
            }

            lock (_startStopLock)
            {
                if (!_isStarted)
                {
                    return;
                }

                _backgroundQueueWorker.Stop();
                _stoppingTokenSource.Cancel();

                _isStarted = false;
            }
        }

        private void MonitorBatchAge()
        {
            while (!_stoppingTokenSource.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.UtcNow;

                    if (now > _batchDeathMoment)
                    {
                        List<TItem> batchToHandle = null;

                        lock (_batchSwitchingLock)
                        {
                            // Is HandleItem didn't start new batch in other thread?
                            if (now > _batchDeathMoment)
                            {
                                batchToHandle = _batch;
                                StartNewBatch();
                            }
                        }

                        if (batchToHandle != null)
                        {
                            HandleBatch(batchToHandle);
                        }
                    }

                    Task.Delay(10).Wait();
                }
                catch (Exception ex)
                {
                    _log?.WriteErrorAsync(_componentName, nameof(MonitorBatchAge), nameof(BackgroundQueueBatchWorker<TItem>), ex)?.Wait();
                }
            }
        }

        private void HandleItem(TItem item)
        {
            _batch.Add(item);

            if (_batch.Count >= _maxBatchSize)
            {
                List<TItem> batchToHandle = null;

                lock (_batchSwitchingLock)
                {
                    // Is MonitorBatchAge didn't start new batch in other thread?
                    if (_batch.Count >= _maxBatchSize)
                    {
                        batchToHandle = _batch;
                        StartNewBatch();
                    }                    
                }

                if (batchToHandle != null)
                {
                    HandleBatch(batchToHandle);
                }
            }
        }

        private void StartNewBatch()
        {
            _batch = new List<TItem>(_maxBatchSize);
            _batchDeathMoment = DateTime.UtcNow + _maxBatchAge;
        }

        private void HandleBatch(IReadOnlyCollection<TItem> batch)
        {
            try
            {
                _handler?.Invoke(batch);
            }
            catch (Exception ex)
            {
                _log?.WriteErrorAsync(_componentName, nameof(HandleBatch), nameof(BackgroundQueueBatchWorker<TItem>), ex)?.Wait();
            }
        }
    }
}