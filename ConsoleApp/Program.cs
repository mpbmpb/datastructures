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
            // tree.Add(15);
            // tree.Add(7);
            // tree.Add(2);
            // tree.Add(13);
            // tree.Add(14);
            // tree.Add(9);
            // tree.Add(17);
            // tree.Add(1);
            
            tree.InOrder().ToList().ForEach(Console.WriteLine);

            var tree2 = new BST<int>();
            Console.WriteLine(tree2.Min() is null);
        }
    }
}