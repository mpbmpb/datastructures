using System.Linq;
using Xunit;
using FluentAssertions;

namespace Datastructures.Tests
{
    public class BSTTests
    {
        [Fact]
        public void New_BST_has_count_0()
        {
            var tree = new BST<int>();
            
            var result = tree.Count;

            result.Should().Be(0);
        }

        [Fact]
        public void Add_unique_value_to_BST_returns_true()
        {
            var tree = new BST<int>();

            var result = tree.Insert(1);

            result.Should().BeTrue();
        }

        [Fact]
        public void Add_duplicate_value_to_BST_returns_false()
        {
            var tree = new BST<int>();

            tree.Insert(1);
            
            var result = tree.Insert(1);
            
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Add_unique_value_increases_count(int n)
        {
            var tree = new BST<int>();

            for (int i = 1; i <= n; i++)
                tree.Insert(i);
            
            var result = tree.Count;
            
            result.Should().Be(n);
        }

        [Fact]
        public void Search_returns_true_if_value_exists()
        {
            var tree = new BST<int>();

            tree.Insert(1);

            var result = tree.Search(1);

            result.Value.Should().Be(1);
        }

        [Fact]
        public void Search_returns_null_if_value_doesnt_exist()
        {
            var tree = new BST<int>();

            var result = tree.Search(1);

            result.Should().BeNull();
        }
        
        [Fact]
        public void Constructor_inserts_value_if_provided()
        {
            var tree = new BST<int>(5);

            var result = tree.Search(5);

            tree.Count.Should().Be(1);
            result.Value.Should().Be(5);
        }

        [Fact]
        public void Delete_returns_true_if_value_exists()
        {
            var tree = new BST<int>(5);
        
            var result = tree.Delete(5);

            result.Should().BeTrue();
        }

        [Fact]
        public void Delete_returns_false_if_value_doesnt_exist()
        {
            var tree = new BST<int>(10);

            var result = tree.Delete(5);

            result.Should().BeFalse();
        }

        [Fact]
        public void Delete_removes_value()
        {
            var tree = new BST<int>(10);
            tree.Delete(10);

            var result = tree.Search(10);

            result.Should().BeNull();
        }

        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(13)]
        [InlineData(17)]
        [InlineData(7)]
        [InlineData(2)]
        [InlineData(9)]
        [InlineData(1)]
        public void Delete_only_removes_given_value(int n)
        {
            var tree = new BST<int>(10);
            tree.Insert(15);
            tree.Insert(7);
            tree.Insert(2);
            tree.Insert(13);
            tree.Insert(14);
            tree.Insert(9);
            tree.Insert(17);
            tree.Insert(1);
            tree.Delete(n);

            var result = tree.Search(n);

            result.Should().BeNull();
            tree.Count.Should().Be(8);
        }

        [Fact]
        public void Max_returns_largest_value()
        {
            var tree = new BST<int>(10);
            tree.Insert(15);
            tree.Insert(7);
            tree.Insert(2);
            tree.Insert(13);
            tree.Insert(14);
            tree.Insert(9);
            tree.Insert(17);

            tree.Max().Value.Should().Be(17);
        }

        [Fact]
        public void Min_returns_smallest_value()
        {
            var tree = new BST<int>(10);
            tree.Insert(15);
            tree.Insert(7);
            tree.Insert(2);
            tree.Insert(13);
            tree.Insert(14);
            tree.Insert(9);
            tree.Insert(17);

            tree.Min().Value.Should().Be(2);
        }

        [Fact]
        public void InOrder_returns_ordered_results()
        {
            var tree = new BST<int>(10);
            tree.Insert(15);
            tree.Insert(7);
            tree.Insert(2);
            tree.Insert(13);
            tree.Insert(14);
            tree.Insert(9);
            tree.Insert(17);
            tree.Insert(1);

            var result = tree.InOrder();
            var expected = new int[] {
                1, 2, 7, 9, 10, 13, 14, 15, 17
            };

            result.Should().Equal(expected);
        }

        [Fact]
        public void Null_tests()
        {
            var tree = new BST<int>();

            tree.Max().Should().BeNull();
            tree.Min().Should().BeNull();
            tree.Search(1).Should().BeNull();
            tree.InOrder().Should().BeEmpty();
            tree.Count.Should().Be(0);
            tree.Delete(1).Should().BeFalse();
        }
    }
}