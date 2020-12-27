using Xunit;
using FluentAssertions;

namespace Datastructures.Tests
{
    public class MaxHeap_nullable_int_Tests
    {
        [Theory]
        [InlineData(-20, true)]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(3, true)]
        [InlineData(10, true)]
        [InlineData(-27, false)]
        public void Delete_returns_true_when_successful(int n, bool expected)
        {
            var heap = new MaxHeap(1);
            heap.Insert(3);
            heap.Insert(10);
            heap.Insert(0);
            heap.Insert(-20);

            var result = heap.Delete(n);

            result.Should().Be(expected);
        }
        
        [Theory]
        [InlineData(-20, 4)]
        [InlineData(0, 4)]
        [InlineData(1, 4)]
        [InlineData(3, 4)]
        [InlineData(10, 4)]
        [InlineData(-27, 5)]
        public void Delete_removes_value_if_found(int n, int expected)
        {
            var heap = new MaxHeap(1);
            heap.Insert(3);
            heap.Insert(10);
            heap.Insert(0);
            heap.Insert(-20);
            heap.Delete(n);

            var result = heap.Search(n);

            result.Should().BeFalse();
            heap.Count.Should().Be(expected);
        }
    }
}