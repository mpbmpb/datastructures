using System;
using Datastructures;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BST<int> Tree = new(10);
            Tree.Add(5);
            Tree.Add(6);
            Console.WriteLine(Tree.Add(5));
        }
    }
}