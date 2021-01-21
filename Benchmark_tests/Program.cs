using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using BenchmarkDotNet.Running;
using Datastructures;

namespace Benchmark_tests
{
    class Program
    {
        static void Main(string[] args)
        {

            BenchmarkRunner.Run<AVL_Benchmark_tests>();
        }

        public class AVLTests
        {
            private readonly int startSize = 32;
            private readonly int inserts = 16;
            private List<AVLTree<int>> _trees;
            private List<long> times;

            public AVLTests()
            {
                init();
            }

            private void init()
            {
                _trees = new();
                for (int i = 0; i < 5; i++)
                {
                    _trees.Add(new AVLTree<int>());
                }

                times = new();
            }

            public void Clear()
            {
                _trees = null;
                times = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                init();
            }
            
            public void TestInserts()
            {
                LoadTreesWithEvenNums();

                TimeInserts();

                PrintResults();
                
                Clear();
            }

            private void LoadTreesWithEvenNums()
            {
                for (int t = 0; t < _trees.Count; t++)
                {
                    var tree = _trees[t];
                    var size = (int)Math.Pow(startSize, t + 1);
                    for (int i = 2; i < size * 2; i += 2)
                    {
                        tree.Add(i);
                    }
                }
            }

            private void TimeInserts()
            {
                for (int t = 0; t < _trees.Count; t++)
                {
                    var tree = _trees[t];
                    var size = (int)Math.Pow(startSize, t + 1);
                    var equalPart = size / inserts;
                    if ((equalPart & 1) == 0) equalPart++;
                    var numberToAdd = 1;

                    var sw = Stopwatch.StartNew();
                    for (int i = 0; i < inserts; i++)
                    {
                        tree.Add(numberToAdd);
                        numberToAdd += equalPart;
                    }

                    sw.Stop();

                    times.Add((sw.ElapsedTicks / inserts));
                }
            }

            private void PrintResults()
            {
                for (int t = 0; t < _trees.Count; t++)
                {
                    double growth = t == 0 ? 1.0 : (double)times[t] / (double)times[t - 1];
                    Console.WriteLine($"{times[t]} ticks per insert at tree size: {(int)Math.Pow(startSize, t + 1)}, "
                    + $"growth factor: {growth}");
                }
            }
        }
    }
}
