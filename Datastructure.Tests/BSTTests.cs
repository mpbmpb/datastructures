using System;
using System.Threading.Tasks;
using Datastructures;
using Xunit;
using FluentAssertions;

namespace Datastructure.Tests
{
    public class BSTTests
    {
        [Fact]
        public async Task New_BST_has_count_0()
        {
            var tree = new BST<int>();
            
            var result = tree.Count;

            result.Should().Be(0);
        }

        [Fact]
        public async Task Add_unique_value_to_BST_returns_true()
        {
            var tree = new BST<int>();

            var result = tree.Insert(1);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task Add_duplicate_value_to_BST_returns_false()
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
        public async Task Add_unique_value_increases_count(int n)
        {
            var tree = new BST<int>();

            for (int i = 1; i <= n; i++)
                tree.Insert(i);
            
            var result = tree.Count;
            
            result.Should().Be(n);
        }

        [Fact]
        public async Task Search_returns_true_if_value_exists()
        {
            var tree = new BST<int>();

            tree.Insert(1);

            var result = tree.Search(1);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task Search_returns_false_if_value_doesnt_exist()
        {
            var tree = new BST<int>();

            var result = tree.Search(1);

            result.Should().BeFalse();
        }
        
        [Fact]
        public async Task Constructor_inserts_value_if_provided()
        {
            var tree = new BST<int>(5);

            var result = tree.Search(5);

            tree.Count.Should().Be(1);
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Delete_returns_true_if_value_exists()
        {
            var tree = new BST<int>(5);
        
            var result = tree.Delete(5);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task Delete_returns_false_if_value_doesnt_exist()
        {
            var tree = new BST<int>(10);

            var result = tree.Delete(5);

            result.Should().BeFalse();
        }
        
    }
}