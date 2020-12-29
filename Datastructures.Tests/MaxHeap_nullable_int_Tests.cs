using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using FluentAssertions;

namespace Datastructures.Tests
{
    public class MaxHeap_nullable_int_Tests
    {
        
                [Fact]
        public void New_MaxHeap_has_count_0()
        {
            var heap = new MaxHeap();

            var result = heap.Count;

            result.Should().Be(0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(23)]
        public void Insert_increases_count(int n)
        {
            var heap = new MaxHeap();

            for (int i = 1; i <= n; i++)
            {
                heap.Insert(i);
            }
            var result = heap.Count;

            result.Should().Be(n);
        }

        [Fact]
        public void Constructor_inserts_value_if_provided()
        {
            var heap = new MaxHeap(1);
            var result = heap.Count;

            result.Should().Be(1);
        }
        
        
        [Fact]
        public void New_Heap_has_array_with_capacity_4()
        {
            var heap = new MaxHeap();

            var capacity = heap.GetType().GetField("_capacity", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (int)capacity.GetValue(heap);

            result.Should().Be(4);
        }

        [Fact]
        public void Heap_array_grows_after_4_insertions()
        {
            var heap = new MaxHeap(1);
            heap.Insert(3);
            heap.Insert(10);
            heap.Insert(8);
            heap.Insert(-20);
            
            var capacity = heap.GetType().GetField("_capacity", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (int)capacity.GetValue(heap);

            result.Should().Be(8);
        }

        [Fact]
        public void Insert_places_max_value_at_pos_0_of_array()
        {
            var heap = new MaxHeap(1);
            heap.Insert(3);
            heap.Insert(10);
            heap.Insert(8);
            heap.Insert(-20);
            
            var array = heap.GetType().GetField("_array", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = array.GetValue(heap);

            result.As<IEnumerable<int?>>().First().Should().Be(10);
        }

        [Fact]
        public void Values_returns_all_elements()
        {
            var heap = new MaxHeap(1);
            heap.Insert(3);
            heap.Insert(10);
            heap.Insert(-1);
            heap.Insert(-20);

            var result = heap.Values();
            var expected = new int[] {10, 1, 3, -1, -20 };

            result.Should().Equal(expected);
        }

        [Fact]
        public void Insert_preserves_MaxHeap_property()
        {
            var heap = new MaxHeap(1);
            heap.Insert(3);
            heap.Insert(10);
            heap.Insert(-1);
            heap.Insert(-20);
            heap.Insert(8);
            heap.Insert(9);
            
            var result = heap.Values();
            var expected = new int[] {10, 1, 9, -1, -20, 3, 8 };

            result.Should().Equal(expected);
        }

        [Theory]
        [InlineData(-1, true)]
        [InlineData(1, true)]
        [InlineData(0, true)]
        [InlineData(3, true)]
        [InlineData(10, true)]
        [InlineData(-20, true)]
        [InlineData(15, false)]
        public void Search_returns_true_if_found(int n, bool expected)
        {
            var heap = new MaxHeap(1);
            heap.Insert(3);
            heap.Insert(0);
            heap.Insert(10);
            heap.Insert(-1);
            heap.Insert(-20);

            var result = heap.Search(n);

            result.Should().Be(expected);
        }

        [Fact]
        public void Max_returns_max_value()
        {
            var heap = new MaxHeap(1);
            heap.Insert(3);
            heap.Insert(10);
            heap.Insert(-1);
            heap.Insert(-20);

            var result = heap.Max();

            result.Should().Be(10);
        }

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

        [Fact]
        public void Delete_removes_max_when_not_given_arguments()
        {
            var heap = new MaxHeap(1);
            heap.Insert(3);
            heap.Insert(5);
            heap.Insert(10);
            heap.Insert(-20);
            
            var result = heap.Delete();

            result.Should().Be(10);
            heap.Search(10).Should().BeFalse();
        }

        [Fact]
        public void Delete_preserves_MaxHeap_property()
        {
            var heap = new MaxHeap(1);
            heap.Insert(3);
            heap.Insert(5);
            heap.Insert(10);
            heap.Insert(-20);
            
            heap.Delete();
            var result = heap.Values();
            var expected = new int[] { 5, 1, 3, -20 };

            result.Should().Equal(expected);
        }
        
        [Fact]
        public void Replace_pops_max_and_inserts_value()
        {
            var heap = new MaxHeap(1);
            heap.Insert(3);
            heap.Insert(5);
            heap.Insert(10);
            heap.Insert(-20);
        
            var result = heap.Replace(-1);
            var expected = new int[] { 5, 1, 3, -1, -20 };
        
            result.Should().Be(10);
            heap.Values().Should().Equal(expected);
        }
        
        [Fact]
        public void Null_tests()
        {
            var heap = new MaxHeap();

            heap.Max().Should().BeNull();
            heap.Search(1).Should().BeFalse();
            heap.Values().Should().BeEmpty();
            heap.Count.Should().Be(0);
            heap.Delete(1).Should().BeFalse();
        }

        
    }
}