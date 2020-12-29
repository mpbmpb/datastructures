using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Datastructures.Tests
{
    public class MaxHeap_Key_Value_Tests
    {
                [Fact]
        public void New_MaxHeap_has_count_0()
        {
            var heap = new MaxHeap<int, string>();

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
            var heap = new MaxHeap<int, string>();

            for (int key = 1; key <= n; key++)
            {
                heap.Insert(key, ('a' + key).ToString());
            }
            var result = heap.Count;

            result.Should().Be(n);
        }

        [Fact]
        public void Constructor_inserts_value_if_provided()
        {
            var heap = new MaxHeap<int, string>(1, "house");
            var result = heap.Count;

            result.Should().Be(1);
        }
        
        
        [Fact]
        public void New_Heap_has_array_with_capacity_4()
        {
            var heap = new MaxHeap<int, string>();

            var capacity = heap.GetType().GetField("_capacity", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (int)capacity.GetValue(heap);

            result.Should().Be(4);
        }

        [Fact]
        public void Heap_array_grows_after_4_insertions()
        {
            var heap = new MaxHeap<int, string>(1, "house");
            heap.Insert(3, "horse");
            heap.Insert(10, "hound");
            heap.Insert(8, "Baskerville");
            heap.Insert(-20, "bird");
            
            var capacity = heap.GetType().GetField("_capacity", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (int)capacity.GetValue(heap);

            result.Should().Be(8);
        }

        [Fact]
        public void Insert_throws_exception_when_given_default_value_for_key()
        {
            var heap = new MaxHeap<int, string>();
            var stringHeap = new MaxHeap<string>();
            Exception result = null;
            Exception resultString = null;
            try
            {
                heap.Insert(0, "house");
            }
            catch (Exception e)
            {
                result = e;
            }

            try
            {
                stringHeap.Insert(null);
            }
            catch (Exception e)
            {
                resultString = e;
            }

            result.Should().BeOfType<InvalidOperationException>();
            resultString.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public void Insert_places_max_value_at_pos_0_of_array()
        {
            var heap = new MaxHeap<int, string>(1, "house");
            heap.Insert(3, "horse");
            heap.Insert(1000, "hound");
            heap.Insert(8, "Baskerville");
            heap.Insert(-20, "bird");
            
            var result = heap.Max();

            result.Item1.Should().Be(1000);
        }

        [Fact]
        public void Values_returns_all_values()
        {
            var heap = new MaxHeap<int, string>(1, "house");
            heap.Insert(3, "horse");
            heap.Insert(10, "hound");
            heap.Insert(-1, "Baskerville");
            heap.Insert(-20, "bird");

            var result = heap.Values();
            var expected = new string[] {"hound", "house", "horse", "Baskerville", "bird" };

            result.Should().Equal(expected);
        }

        [Fact]
        public void Insert_preserves_MaxHeap_property()
        {
            var heap = new MaxHeap<int, string>(1, "house");
            heap.Insert(3, "horse");
            heap.Insert(10, "hound");
            heap.Insert(-1, "Baskerville");
            heap.Insert(-20, "bird");
            heap.Insert(8, "dog");
            heap.Insert(9, "bird-dog");
            
            var result = heap.Values();
            var expected = new string[] {"hound", "house", "bird-dog", "Baskerville", "bird", "horse", "dog" };

            result.Should().Equal(expected);
        }

        [Theory]
        [InlineData(-1, "Baskerville")]
        [InlineData(1, "house")]
        [InlineData(3, "horse")]
        [InlineData(10, "hound")]
        [InlineData(-20, "bird")]
        [InlineData(15, null)]
        public void Search_returns_value_if_found(int key, string expected)
        {
            var heap = new MaxHeap<int, string>(1, "house");
            heap.Insert(3, "horse");
            heap.Insert(10, "hound");
            heap.Insert(-1, "Baskerville");
            heap.Insert(-20, "bird");

            var result = heap.Search(key);

            result?.FirstOrDefault()?.Should().Be(expected);
        }

        [Fact]
        public void Max_returns_tuple_with_max_key()
        {
            var heap = new MaxHeap<int, string>(1, "house");
            heap.Insert(3, "horse");
            heap.Insert(10, "hound");
            heap.Insert(-1, "Baskerville");
            heap.Insert(-20, "bird");

            var result = heap.Max();

            result.Item1.Should().Be(10);
        }

        [Theory]
        [InlineData(-1, "Baskerville")]
        [InlineData(1, "house")]
        [InlineData(3, "horse")]
        [InlineData(10, "hound")]
        [InlineData(-20, "bird")]
        [InlineData(-27, null)]
        public void Delete_returns_value_when_successful(int key, string expected)
        {
            var heap = new MaxHeap<int, string>(1, "house");
            heap.Insert(3, "horse");
            heap.Insert(10, "hound");
            heap.Insert(-1, "Baskerville");
            heap.Insert(-20, "bird");

            var result = heap.Delete(key);

            result.Should().Be(expected);
        }

        [Fact]
        public void Delete_returns_null_when_notfound()
        {
            var heap = new MaxHeap<int, string>(1, "house");

            var result = heap.Delete(42);

            result.Should().BeNull();
        }
        
        [Theory]
        [InlineData(-20, 4)]
        [InlineData(1, 4)]
        [InlineData(3, 4)]
        [InlineData(10, 4)]
        [InlineData(-27, 5)]
        public void Delete_removes_value_if_found(int key, int expected)
        {
            var heap = new MaxHeap<int, string>(1, "house");
            heap.Insert(3, "horse");
            heap.Insert(10, "hound");
            heap.Insert(-1, "Baskerville");
            heap.Insert(-20, "bird");
            heap.Delete(key);

            var result = heap.Search(key);

            result.Should().BeEmpty();
            heap.Count.Should().Be(expected);
        }

        [Fact]
        public void Delete_removes_max_when_not_given_arguments()
        {
            var heap = new MaxHeap<int, string>(1, "house");
            heap.Insert(3, "horse");
            heap.Insert(10, "hound");
            heap.Insert(-1, "Baskerville");
            heap.Insert(-20, "bird");
            
            var result = heap.Delete();

            result.Item1.Should().Be(10);
            heap.Search(10).Should().BeEmpty();
        }

        [Fact]
        public void Delete_preserves_MaxHeap_property()
        {
            var heap = new MaxHeap<int, string>(1, "house");
            heap.Insert(3, "horse");
            heap.Insert(5, "hound");
            heap.Insert(10, "Baskerville");
            heap.Insert(-20, "bird");
            
            heap.Delete();
            var result = heap.Values();
            var expected = new string[] { "hound", "house", "horse", "bird" };

            result.Should().Equal(expected);
        }

        [Fact]
        public void Replace_pops_max_and_inserts_new_key_value()
        {
            var heap = new MaxHeap<int, string>(1, "house");
            heap.Insert(3, "horse");
            heap.Insert(5, "hound");
            heap.Insert(10, "Baskerville");
            heap.Insert(-20, "bird");

            var result = heap.Replace(-1, "dog");
            var expected = new string[] { "hound", "house", "horse", "dog", "bird" };

            result.Item1.Should().Be(10);
            heap.Values().Should().Equal(expected);
        }
        
        [Fact]
        public void Null_tests()
        {
            var heap = new MaxHeap<int, string>();

            heap.Max().Should().BeNull();
            heap.Search(1).Should().BeEmpty();
            heap.Values().Should().BeEmpty();
            heap.Count.Should().Be(0);
            heap.Delete(1).Should().BeNull();
        }

    }
}