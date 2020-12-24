using System;
using Datastructures;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BST<int> Tree = new(10);
            Tree.Insert(5);
            Tree.Insert(6);
            Console.WriteLine(Tree.Insert(5));
        }
    }
}