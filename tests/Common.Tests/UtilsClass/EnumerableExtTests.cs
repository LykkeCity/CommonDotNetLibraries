using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Common.Tests.UtilsClass
{
    public class EnumerableExtTests
    {
        [Theory]
        [InlineData(0, 10)]
        [InlineData(0, 0)]
        [InlineData(10, 10)]
        [InlineData(10, 10)]
        [InlineData(10, 10)]
        public void GenerateIntsTest(int from, int to)
        {
            var seq = Utils.GenerateInts(from, to);
            var oldSeq = OldGenerateInts(from, to);

            Assert.Equal(seq, oldSeq);
        }

        [Fact]
        public void GenerateIntsInvalidInputTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Utils.GenerateInts(10, 0));
        }

        private IEnumerable<int> OldGenerateInts(int from, int to)
        {
            for (var i = from; i <= to; i++)
                yield return i;
        }

        [Fact]
        public void ToChunksTest()
        {
            var seq = Enumerable.Range(0, 100);
            var cunks = Utils.ToChunks(seq, 11);
            var oldCunks = OldToChunks(seq, 11);

            Assert.Equal(cunks, oldCunks);
        }

        [Fact]
        public void ToPiesesTest()
        {
            var seq = Enumerable.Range(0, 100);
            var cunks = Utils.ToChunks(seq, 11);
            var oldCunks = OldToPieses(seq, 11);

            Assert.Equal(cunks, oldCunks);
        }

        [Fact]
        public void LimitTest()
        {
            var seq = Enumerable.Range(0, 100);
            var lim = Utils.Limit(seq, 42);
            var oldLimit = OldLimit(seq, 42);

            Assert.Equal(lim, oldLimit);
        }

        [Fact]
        public void CutFromTest()
        {
            var seq = Enumerable.Range(0, 100);
            var cut = Utils.CutFrom(seq, 24, 42);
            var oldCut = OldCutFrom(seq, 24, 42);

            Assert.Equal(cut, oldCut);
        }

        private static IEnumerable<IEnumerable<T>> OldToChunks<T>(IEnumerable<T> src, int chunkSize)
        {
            var chunk = new List<T>();

            foreach (var item in src)
            {
                chunk.Add(item);

                if (chunk.Count >= chunkSize)
                {
                    yield return chunk.ToArray();
                    chunk.Clear();
                }

            }

            if (chunk.Count > 0)
                yield return chunk;
        }

        private static IEnumerable<IEnumerable<T>> OldToPieses<T>(IEnumerable<T> src, int countInPicese)
        {
            var result = new List<T>();

            foreach (var itm in src)
            {
                result.Add(itm);
                if (result.Count >= countInPicese)
                {
                    yield return result;
                    result = new List<T>();
                }
            }

            if (result.Count > 0)
                yield return result;
        }

        private static IEnumerable<T> OldLimit<T>(IEnumerable<T> src, int limit)
        {
            var no = 0;
            foreach (var itm in src)
            {
                yield return itm;
                no++;
                if (no >= limit)
                    break;
            }
        }

        private static IEnumerable<T> OldCutFrom<T>(IEnumerable<T> src, int from, int length)
        {
            var i = 0;

            var indexTo = from + length;

            foreach (var itm in src)
            {
                if (i >= indexTo)
                    yield break;

                if (i >= from && i < indexTo)
                    yield return itm;

                i++;
            }
        }
    }
}
