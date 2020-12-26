using System;
using System.Reflection;
using Xunit;
using FluentAssertions;

namespace Datastructures.Tests
{
    public class MaxHeapTests
    {
        [Fact]
        public void New_MaxHeap_has_count_0()
        {
            var heap = new MaxHeap<int>();

            var result = heap.Count;

            result.Should().Be(0);
        }

        [Fact]
        public void Insert_increases_count()
        {
            var heap = new MaxHeap<int>();
            heap.Insert (1);
            var result = heap.Count;

            result.Should().Be(1);
        }

        [Fact]
        public void Constructor_inserts_value_if_provided()
        {
            var heap = new MaxHeap<int>(1);
            var result = heap.Count;

            result.Should().Be(1);
        }

        [Fact]
        public void Heap_array_grows_after_4_insertions()
        {
            var heap = new MaxHeap<int>(1);
            heap.Insert(3);
            heap.Insert(10);
            heap.Insert(0);
            Exception ex = null;
            try
            {
                heap.Insert(-20);
            }
            catch (Exception e)
            {
                ex = e;
            }

            ex.Should().BeNull();
        }

        [Fact]
        public void New_Heap_has_array_with_capacity_4()
        {
            var heap = new MaxHeap<int>();

            var result = heap.GetType().GetProperty("_array", BindingFlags.NonPublic);

            result.As<Array>().Length.Should().Be(4);
        }
        
        
    }
}