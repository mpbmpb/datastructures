using Xunit;
using FluentAssertions;


namespace Datastructures.Tests
{
    public class BST_Key_Value_Tests
    {
        [Fact]
        public void New_BST_has_count_0()
        {
            var tree = new BST<int, string>();
            
            var result = tree.Count;

            result.Should().Be(0);
        }
        
        [Fact]
        public void Add_unique_key_to_BST_returns_true()
        {
            var tree = new BST<int, string>();

            var result = tree.Insert(1, "house");

            result.Should().BeTrue();
        }

        
        [Fact]
        public void Add_duplicate_key_to_BST_returns_false()
        {
            var tree = new BST<int, string>();

            tree.Insert(1, "house");
            
            var result = tree.Insert(1, "horse");
            
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Add_unique_value_increases_count(int n)
        {
            var tree = new BST<int, string>();

            for (int i = 1; i <= n; i++)
                tree.Insert(i, ('a' + i).ToString());
            
            var result = tree.Count;
            
            result.Should().Be(n);
        }
        
        
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(8)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(17)]
        public void Search_returns_value_if_key_exists(int n)
        {
            var tree = new BST<int, string>(10, "house");
            tree.Insert(15, "horse");
            tree.Insert(7, "hound");
            tree.Insert(2, "Baskerville");
            tree.Insert(13, "bird");
            tree.Insert(14, "dog");
            tree.Insert(9, "bird-dog");
            var expected = "final";
            tree.Insert(n, expected);

            var result = tree.Search(n);

            result.Value.Should().Be(expected);
        }

        
        [Fact]
        public void Search_returns_null_if_value_doesnt_exist()
        {
            var tree = new BST<int, string>();

            var result = tree.Search(1);

            result.Should().BeNull();
        }
        
        [Fact]
        public void Constructor_inserts_value_if_provided()
        {
            var expected = "house";
            var tree = new BST<int, string>(5, expected);

            var result = tree.Search(5);

            tree.Count.Should().Be(1);
            result.Value.Should().Be(expected);
        }

        [Fact]
        public void Delete_returns_true_if_key_exists()
        {
            var tree = new BST<int, string>(5, "house");
        
            var result = tree.Delete(5);

            result.Should().BeTrue();
        }

        [Fact]
        public void Delete_returns_false_if_key_doesnt_exist()
        {
            var tree = new BST<int, string>(10, "house");

            var result = tree.Delete(5);

            result.Should().BeFalse();
        }

        [Fact]
        public void Delete_removes_node()
        {
            var tree = new BST<int, string>(10, "house");
            tree.Delete(10);

            var result = tree.Search(10);

            result.Should().BeNull();
            tree.Count.Should().Be(0);
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
            var tree = new BST<int, string>(10, "house");
            tree.Insert(15, "horse");
            tree.Insert(7, "hound");
            tree.Insert(2, "Baskerville");
            tree.Insert(13, "bird");
            tree.Insert(14, "dog");
            tree.Insert(9, "bird-dog");
            tree.Insert(17, "starling");
            tree.Insert(1, "shrimp");
            tree.Delete(n);

            var result = tree.Search(n);

            result.Should().BeNull();
            tree.Count.Should().Be(8);
        }
        
        [Fact]
        public void Min_returns_node_with_smallest_key()
        {
            var tree = new BST<int, string>(10, "house");
            tree.Insert(15, "horse");
            tree.Insert(7, "hound");
            tree.Insert(2, "Baskerville");
            tree.Insert(13, "bird");
            tree.Insert(14, "dog");
            tree.Insert(9, "bird-dog");
            tree.Insert(17, "starling");

            tree.Min().Value.Should().Be("Baskerville");
        }
        [Fact]
        public void Max_returns_node_with_largest_key()
        {
            var tree = new BST<int, string>(10, "house");
            tree.Insert(15, "horse");
            tree.Insert(7, "hound");
            tree.Insert(2, "Baskerville");
            tree.Insert(13, "bird");
            tree.Insert(14, "dog");
            tree.Insert(9, "bird-dog");
            tree.Insert(17, "starling");

            tree.Max().Value.Should().Be("starling");
        }

        [Fact]
        public void InOrder_returns_ordered_results()
        {
            var tree = new BST<int, string>(10, "house");
            tree.Insert(15, "horse");
            tree.Insert(7, "hound");
            tree.Insert(2, "Baskerville");
            tree.Insert(13, "bird");
            tree.Insert(14, "dog");
            tree.Insert(9, "bird-dog");
            tree.Insert(17, "starling");
            tree.Insert(1, "shrimp");

            var result = tree.InOrder();
            var expected = new string[] 
            { "shrimp", "Baskerville", "hound", "bird-dog", "house", "bird", "dog", "horse", "starling" };

            result.Should().Equal(expected);
        }
        
        [Fact]
        public void Null_tests()
        {
            var tree = new BST<int, string>();

            tree.Max().Should().BeNull();
            tree.Min().Should().BeNull();
            tree.Search(1).Should().BeNull();
            tree.InOrder().Should().BeEmpty();
            tree.Count.Should().Be(0);
            tree.Delete(1).Should().BeFalse();
        }

    }
}