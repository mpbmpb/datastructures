using System;
using System.Reflection;
using Xunit;
using FluentAssertions;

namespace Datastructures.Tests
{
    public class AVL_Tests
    {
        [Fact]
        public void New_Tree_has_Count_0()
        {
            var tree = new AVLTree<int>();

            tree.Count.Should().Be(0);
        }

        [Fact]
        public void Add_unique_value_returns_true()
        {
            var tree = new AVLTree<int>();

            var result = tree.Add(42);

            result.Should().BeTrue();
        }

        [Fact]
        public void Add_duplicate_value_returns_false()
        {
            var tree = new AVLTree<int>(42);

            var result = tree.Add(42);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        [InlineData(42)]
        public void Add_unique_value_increases_count(int n)
        {
            var tree = new AVLTree<int>();

            for (int i = 0; i < n; i++)
            {
                tree.Add(i);
            }
            var result = tree.Count;

            result.Should().Be(n);
        }

        [Fact]
        public void Add_duplicate_value_does_not_increase_count()
        {
            var tree = new AVLTree<int>(42);
            tree.Add(42);

            var result = tree.Count;

            result.Should().Be(1);
        }

        [Theory]
        [InlineData(42, 7)]
        [InlineData(103, 42)]
        [InlineData(400, -137)]
        [InlineData(2000, 1664)]
        // [InlineData(15000, -8923)]
        public void Search_returns_true_if_value_was_added(int n, int target)
        {
            var tree = new AVLTree<int>();
            for (int i = -n; i <= n; i++)
            {
                if(i !=0 )
                    tree.Add(i);
            }

            var result = tree.Search(target);

            result.Should().BeTrue();
        }

        [Fact]
        public void Search_returns_false_if_value_was_not_added()
        {
            var tree = new AVLTree<int>(42);

            var result = tree.Search(43);

            result.Should().BeFalse();
        }

        [Fact]
        public void Add_increases_height_of_tree()
        {
            var tree = new AVLTree<int>(42);
            
            var startHeight = GetRootHeight(tree);
            tree.Add(40);
            tree.Add(50);
            tree.Add(49);
            var finalRoot = GetRootHeight(tree);

            startHeight.Should().Be(0);
            finalRoot.Should().Be(2);
        }

        private static int? GetRootHeight(AVLTree<int> tree)
        {
            var root = tree.GetType().GetField("_root", BindingFlags.NonPublic | BindingFlags.Instance);
            var initRoot = (AVLNode<int>) root?.GetValue(tree);
            var height = initRoot?.Height;
            return height;
        }

        [Theory]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(32)]
        [InlineData(144)]
        public void Add_preserves_AVL_property_max_height(int n)
        {
            var tree = new AVLTree<int>();
            for (int i = 0; i < n; i++)
            {
                tree.Add(i + 1);
            }

            var result = GetRootHeight(tree);
            var max = (int)(1.44 * Math.Log2(n));

            result.Should().BeLessOrEqualTo(max);
        }
    }
}