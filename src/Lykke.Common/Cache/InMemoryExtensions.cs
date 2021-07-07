using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;

namespace Lykke.Common.Cache
{
    public static class InMemoryExtensions
    {
        private static readonly ConcurrentDictionary<IMemoryCache, object> Caches = new ConcurrentDictionary<IMemoryCache, object>();

        /// <summary>
        /// Returns an existed value or creates,caches and returns a new using the <paramref name="factory"/>
        /// </summary>
        /// <typeparam name="TItem">An cached item type</typeparam>
        /// <param name="cache">An instance of <see cref="IMemoryCache"/></param>
        /// <param name="key">A key</param>
        /// <param name="factory">A factory method that creates a new value in case the key is absent in the cache</param>
        /// <returns></returns>
        public static Task<TItem> GetOrCreateAsync<TItem>([NotNull] this IMemoryCache cache, [NotNull] object key, [NotNull] Func<Task<TItem>> factory)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (cache.TryGetValue(key, out var obj))
            {
                return Task.FromResult((TItem)obj);
            }

            var missedCalls = (ConcurrentDictionary<object, Lazy<Task<TItem>>>)Caches.GetOrAdd(cache, _ => new ConcurrentDictionary<object, Lazy<Task<TItem>>>());
            // It is a very little chance that factory() can be called twice.
            // For this a thread must stop before calling missedCalls.GetOrAdd() and other thread in parallel must call  missedCalls.TryRemove(key, out _);
            // But it practically impossible
            var lazy = missedCalls.GetOrAdd(key, newKey =>
                new Lazy<Task<TItem>>(() =>
                {
                    var factoryTask = factory();

                    var t = factoryTask.ContinueWith(ancestor =>
                    {

                        var result = ancestor.GetAwaiter().GetResult(); // Use GetAwaiter() only for persisting a correct exception stack 
                        var entry = cache.CreateEntry(newKey);
                        entry.SetValue(result);
                        entry.Dispose();
                        return result;
                    }, TaskContinuationOptions.ExecuteSynchronously);

                    return t;
                })
            );

            var promise = lazy.Value;
            promise.ContinueWith(task => { missedCalls.TryRemove(key, out _); }, TaskContinuationOptions.ExecuteSynchronously);
            return promise;


        }
    }
}
