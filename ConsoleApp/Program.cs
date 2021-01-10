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
            for (int i = 1; i <= 32000; i++)
            {
                tree.Add(i);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            var order = tree.InOrder().ToArray();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
