using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Datastructures;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var Test1 = new AVLTests();
            Test1.TestInsertsAtSize(1000, 1024);
            Test1.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Test1 = new AVLTests();
            Test1.TestInsertsAtSize(1000, 1024);
            Test1.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            
            var Test2 = new AVLTests();
            Test2.TestInsertsAtSize(1000, 32768);
            Test2.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            Test2 = new AVLTests();
            Test2.TestInsertsAtSize(1000, 32768);
            Test2.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            var Test3 = new AVLTests();
            Test3.TestInsertsAtSize(1000, 1048576);
            Test3.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            Test3 = new AVLTests();
            Test3.TestInsertsAtSize(1000, 1048576);
            Test3.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        public class AVLTests
        {
            private List<AVLTree<int>> _trees;

            public AVLTests()
            {
                _trees = new();
                for (int i = 0; i < 4; i++)
                {
                    _trees.Add(new AVLTree<int>());
                }
            }

            public void Clear()
            {
                _trees = null;
            }
            public void TestInsertsAtSize(int inserts, int addToSize)
            {
                var times = new List<int>();
                var threads = new List<Thread>();
                for (int thread = 0; thread < 4; thread++)
                {
                    var tree = _trees[thread];
                    threads.Add(new Thread(() =>
                    {
                        var sw = new Stopwatch();
                        for (int i = 1; i < addToSize; i++)
                        {
                            tree.Add(i);
                        }

                        sw.Start();
                        for (int i = addToSize; i < addToSize + inserts; i++)
                        {
                            tree.Add(inserts * 1000 + i);
                        }

                        sw.Stop();
                        times.Add((int)(sw.ElapsedTicks / inserts ));
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        // Thread.Sleep(100);
                        GC.Collect();
                    }));
                }

                foreach (var thread in threads)
                { 
                    thread.Start();
                }

                threads[0].Join();
                threads[1].Join();
                threads[2].Join();
                threads[3].Join();
                Console.WriteLine($"Max: {times.Max()} ticks per insert at size: {addToSize}");
                Console.WriteLine($"Min: {times.Min()} ticks per insert at size: {addToSize}");
                // Console.WriteLine($"Avg: {times.Average()} ticks per insert at size: {addToSize}");
            }
            
        }
    }
}
