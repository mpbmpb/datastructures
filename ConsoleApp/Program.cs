using System;
using System.Diagnostics;
using System.Linq;
using Datastructures;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new AVLTree<int>();
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 1; i <= 40000; i++)
            {
                tree.Add(i);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.ReadKey();
            sw.Restart();
            Console.WriteLine(tree.Search(38769));
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            var tree2 = new BST<int>();
            Console.WriteLine(tree2.Min() is null);
        }
    }
}