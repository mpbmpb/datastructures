using System;
using System.Linq;
using Datastructures;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new BST<int>();
            // tree.Insert(15);
            // tree.Insert(7);
            // tree.Insert(2);
            // tree.Insert(13);
            // tree.Insert(14);
            // tree.Insert(9);
            // tree.Insert(17);
            // tree.Insert(1);
            
            tree.InOrder().ToList().ForEach(Console.WriteLine);

            var tree2 = new BST<int>();
            Console.WriteLine(tree2.Min() is null);
        }
    }
}