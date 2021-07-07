using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Lykke.Common.Cache;
using Xunit;

namespace Common.Tests.Cache
{
    public class InMemoryCacheExtensionsTest
    {
        private readonly MemoryCache _cache;
        private int _longTaskCalledNo;

        public InMemoryCacheExtensionsTest()
        {
            _cache = new MemoryCache(new MemoryCacheOptions { ExpirationScanFrequency = TimeSpan.MaxValue });
        }

        [Fact]
        public void GetOrCreateAsyncShouldHandleConcurrency()
        {
            var mock = new CacheMock(_cache);

            var kv1 = new { key = "key1", value = 42 };
            var kv2 = new { key = "key2", value = 43 };

            var kvs = new[] { kv1, kv2 };
            const int repeats = 100000;
            var tasks = new ConcurrentBag<(int val, Task<int> task)>();
            Parallel.For(0, repeats, (i, state) =>
            {
                var expected = kvs[i % 2];

                tasks.Add((expected.value, mock.GetOrCreateAsync(expected.key, () => LongTask(expected.value))));
            });

            Task.WaitAll(tasks.Select(t => t.task).ToArray());

            foreach (var task in tasks)
            {
                Assert.Equal(task.val, task.task.Result);
            }

            Assert.Equal(repeats, mock.TryGetValueCalledNo);
            Assert.Equal(2, _longTaskCalledNo);


            _cache.TryGetValue(kv1.key, out var cachedResult);
            Assert.Equal(kv1.value, cachedResult);

            _cache.TryGetValue(kv2.key, out var cachedResult2);
            Assert.Equal(kv2.value, cachedResult2);

            Assert.Equal(2, mock.CreateEntryCalledNo);
        }

        [Fact]
        public async Task ShouldCorrectlyHandleCancellation()
        {
            var mock = new CacheMock(_cache);

            const string key = "key";
            var tasks = new List<Task<int>>();
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(mock.GetOrCreateAsync(key, LongTaskWithCancellation));
            }

            // ReSharper disable once CoVariantArrayConversion

            try
            {
                await Task.WhenAll(tasks.ToArray());
            }
            catch (TaskCanceledException)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }


        [Fact]
        public void FailedFactoryShouldNotAffectCache()
        {
            var exception = new InvalidOperationException("This is my exception");




            var mock = new CacheMock(_cache);

            const string key = "key";
            var tasks = new List<Task<int>>();
            for (var i = 0; i < 5; i++)
            {
                tasks.Add(mock.GetOrCreateAsync(key, () => LongTaskWithException(exception)));
            }

            try
            {
                // ReSharper disable once CoVariantArrayConversion
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException)
            {
            }
            Assert.All(tasks, task =>
            {
                Assert.True(task.IsFaulted);
                Assert.Equal(exception, task.Exception.InnerException);
            });

            Assert.Equal(1, _longTaskCalledNo);
            Assert.Equal(0, mock.CreateEntryCalledNo);
        }

        private async Task<int> LongTaskWithException(Exception exception)
        {
            Interlocked.Increment(ref _longTaskCalledNo);
            await Task.Delay(1000);
            throw exception;
        }

        private async Task<int> LongTask(int result)
        {
            Interlocked.Increment(ref _longTaskCalledNo);
            await Task.Delay(1000).ConfigureAwait(false);
            return result;
        }

        private async Task<int> LongTaskWithCancellation()
        {
            Interlocked.Increment(ref _longTaskCalledNo);
            using (var cst = new CancellationTokenSource(500))
            {
                await Task.Delay(1000, cst.Token).ConfigureAwait(false);
            }
            return 42;
        }
    }


    internal class CacheMock : IMemoryCache
    {
        private readonly IMemoryCache _impl;
        public int CreateEntryCalledNo;
        public int TryGetValueCalledNo;

        public CacheMock(IMemoryCache impl)
        {
            _impl = impl;
        }

        public void Dispose()
        {
            _impl.Dispose();
        }

        public bool TryGetValue(object key, out object value)
        {
            Interlocked.Increment(ref TryGetValueCalledNo);
            return _impl.TryGetValue(key, out value);
        }

        public ICacheEntry CreateEntry(object key)
        {
            Interlocked.Increment(ref CreateEntryCalledNo);
            return _impl.CreateEntry(key);
        }

        public void Remove(object key)
        {
            _impl.Remove(key);
        }
    }
}
