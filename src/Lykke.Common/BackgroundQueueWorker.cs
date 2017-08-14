using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Common.Log;

namespace Common
{
    /// <summary>
    /// Aggregates items in FIFO queue and lets client asynchronously process them in single dedicated thread
    /// </summary>
    public sealed class BackgroundQueueWorker<TItem> :
        IStartable, IStopable
    {
        private readonly string _componentName;
        private readonly ILog _log;
        private readonly int _maxItemsInWorkIteration;
        private readonly object _startStopLock;
        private readonly Queue<TItem> _queue;

        private CancellationTokenSource _stoppingTokenSource;
        private bool _isStarted;
        private Action<TItem> _handler;

        /// <param name="componentName">Component name used to log</param>
        /// <param name="log">Logger</param>
        /// <param name="maxItemsInWorkIteration">Max items count will be handled until working thread goes to sleep and lets other threads do their work</param>
        public BackgroundQueueWorker(string componentName, ILog log, int maxItemsInWorkIteration = 1000)
        {
            _componentName = componentName;
            _log = log;
            _maxItemsInWorkIteration = maxItemsInWorkIteration;

            _queue = new Queue<TItem>();
            _startStopLock = new object();
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

                _stoppingTokenSource = new CancellationTokenSource();

                Task.Factory.StartNew(DoWork, _stoppingTokenSource.Token);

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

                _stoppingTokenSource.Cancel();

                _isStarted = false;
            }
        }

        public void Enqueue(TItem item)
        {
            lock (_queue)
            {
                _queue.Enqueue(item);
            }
        }

        public BackgroundQueueWorker<TItem> SetHandler(Action<TItem> handler)
        {
            _handler = handler;

            return this;
        }

        private void DoWork()
        {
            while (!_stoppingTokenSource.IsCancellationRequested)
            {
                try
                {
                    DoWorkIteration();
                    
                    Task.Delay(10).Wait();
                }
                catch (Exception ex)
                {
                    _log?.WriteErrorAsync(_componentName, nameof(DoWork), nameof(BackgroundQueueWorker<TItem>), ex)?.Wait();
                }
            }
        }

        private void DoWorkIteration()
        {
            int count;

            lock (_queue)
            {
                count = _queue.Count;

                if (count == 0)
                {
                    return;
                }
            }

            var iterationItems = new List<TItem>(count);

            lock (_queue)
            {
                for (var i = 0; i < Math.Min(count, _maxItemsInWorkIteration); i++)
                {
                    iterationItems.Add(_queue.Dequeue());
                }
            }

            foreach (var item in iterationItems)
            {
                Handle(item);
            }
        }

        private void Handle(TItem item)
        {
            try
            {
                _handler?.Invoke(item);
            }
            catch (Exception ex)
            {
                _log?.WriteErrorAsync(_componentName, nameof(Handle), nameof(BackgroundQueueWorker<TItem>), ex)?.Wait();
            }
        }
    }
}