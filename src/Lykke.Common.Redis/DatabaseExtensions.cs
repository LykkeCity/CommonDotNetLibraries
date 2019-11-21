using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;
using StackExchange.Redis;

namespace Lykke.Common.Redis
{
    /// <summary>
    /// Redis database extensions
    /// </summary>
    public static class DatabaseExtensions
    {
        public const int DefaultBatchSize = 100;
        
        /// <summary>
        /// Get values for array of keys
        /// </summary>
        /// <param name="src">The database interface</param>
        /// <param name="keys">The keys array</param>
        /// <param name="flags">The command flags</param>
        /// <param name="batchSize">The maximum batch size (if set more than 100 will be used 100)</param>
        /// <returns></returns>
        public static async Task<RedisValue[]> StringGetMultiKeyAsync(
            this IDatabase src, 
            RedisKey[] keys,
            CommandFlags flags = CommandFlags.None, 
            int batchSize = DefaultBatchSize)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            if (keys == null || keys.Length == 0)
                return new RedisValue[0];

            if (batchSize < 0 || batchSize > DefaultBatchSize)
                batchSize = DefaultBatchSize;

            var tasks = keys.Select(k => src.StringGetAsync(k, flags));

            List<RedisValue> result = new List<RedisValue>();

            foreach (var batch in tasks.Batch(batchSize))
            {
                var batchResults = await Task.WhenAll(batch);

                result.AddRange(batchResults);
            }

            return result.ToArray();
        }
    }
}
