using System;
using System.Linq;
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

        [Fact]
        public void Add_balances_tree_to_correct_height()
        {
            var tree = new AVLTree<int>(45);
            tree.Add(70);
            tree.Add(35);
            tree.Add(3);
            tree.Add(74);
            tree.Add(25);
            tree.Add(81);

            var result = GetRootHeight(tree);

            result.Should().Be(2);
        }
        
        [Theory]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(32)]
        [InlineData(144)]
        [InlineData(1264)]
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

        [Fact]
        public void Delete_removes_value_from_tree()
        {
            var tree = new AVLTree<int>(42);
            tree.Add(40);
            tree.Add(50);
            tree.Add(49);
            tree.Add(-12);
            tree.Add(141);

            tree.Delete(42);
            var result = tree.Search(42);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        [InlineData(42)]
        public void Delete_decreases_count(int n)
        {
            var tree = new AVLTree<int>();

            for (int i = 0; i < n; i++)
            {
                tree.Add(i);
            }
            for (int i = 0; i < n; i++)
            {
                tree.Delete(i);
            }
            var result = tree.Count;

            result.Should().Be(0);
        }

        [Fact]
        public void Delete_balances_tree_to_correct_height()
        {
            var tree = new AVLTree<int>(45);
            tree.Add(74);
            tree.Add(35);
            tree.Add(3);
            tree.Add(70);
            tree.Add(25);
            tree.Add(1);

            var heightBeforeDeletion = GetRootHeight(tree);

            tree.Delete(74);
            var result = GetRootHeight(tree);

            heightBeforeDeletion.Should().Be(3);
            result.Should().Be(2);
        }
        
        [Theory]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(32)]
        [InlineData(144)]
        [InlineData(1264)]
        public void Delete_preserves_AVL_property_max_height(int n)
        {
            var tree = new AVLTree<int>();
            for (int i = 0; i < n * 2; i++)
            {
                tree.Add(i + 1);
            }
            for (int i = 0; i < n; i++)
            {
                tree.Delete(i + 1);
            }

            var result = GetRootHeight(tree);
            var max = (int)(1.44 * Math.Log2(n));

            result.Should().BeLessOrEqualTo(max);
        }
        
        
        [Fact]
        public void Max_returns_max_value()
        {
            var tree = new AVLTree<int>(42);
            tree.Add(40);
            tree.Add(50);
            tree.Add(49);
            tree.Add(-12);
            tree.Add(141);

            var result = tree.Max();

            result.Should().Be(141);
        }

        [Fact]
        public void Min_returns_min_value()
        {
            var tree = new AVLTree<int>(42);
            tree.Add(40);
            tree.Add(50);
            tree.Add(49);
            tree.Add(-12);
            tree.Add(141);

            var result = tree.Min();

            result.Should().Be(-12);
        }

        [Fact]
        public void InOrder_returns_values_in_ascending_order()
        {
            var tree = new AVLTree<int>(42);
            tree.Add(40);
            tree.Add(50);
            tree.Add(49);
            tree.Add(-12);
            tree.Add(141);

            var result = tree.InOrder();
            var expected = new int[] {-12, 40, 42, 49, 50, 141};

            result.Should().Equal(expected);
        }

        [Fact]
        public void InOrder_returns_empty_coll_when_tree_is_empty()
        {
            var tree = new AVLTree<int>();

            var result = tree.InOrder();

            result.Should().BeEmpty();
        }
        
        [Fact]
        public void InReverseOrder_returns_values_in_descending_order()
        {
            var tree = new AVLTree<int>(42);
            tree.Add(40);
            tree.Add(50);
            tree.Add(49);
            tree.Add(-12);
            tree.Add(141);

            var result = tree.InReverseOrder();
            var expected = new int[] { 141, 50, 49, 42, 40, -12};

            result.Should().Equal(expected);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(32)]
        [InlineData(144)]
        [InlineData(1264)]
        public void Add_preserves_AVL_ordering(int n)
        {
            var tree = new AVLTree<int>();
            for (int i = 0; i < n; i++)
            {
                tree.Add(i + 1);
            }

            var result = tree.InOrder();
            var expected = Enumerable.Range(1, n);

            result.Should().Equal(expected);

        }
        
        [Theory]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(32)]
        [InlineData(144)]
        [InlineData(1264)]
        public void Delete_preserves_AVL_ordering(int n)
        {
            var tree = new AVLTree<int>();
            for (int i = 0; i < n * 2; i++)
            {
                tree.Add(i + 1);
            }
            for (int i = 0; i < n; i++)
            {
                tree.Delete(i + 1);
            }

            var result = tree.InOrder();
            var expected = Enumerable.Range(n + 1, n);

            result.Should().Equal(expected);
        }

        [Fact]
        public void Clear_clears_tree()
        {
            var tree = new AVLTree<int>(42);
            tree.Add(40);
            tree.Add(50);
            tree.Add(49);
            tree.Add(-12);
            tree.Add(141);

            tree.Clear();

            var result = tree.InOrder();
            var expected = Enumerable.Empty<int>();

            result.Should().Equal(expected);
            tree.Count.Should().Be(0);
        }
    }
}