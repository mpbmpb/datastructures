using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Datastructures;

namespace Benchmark_tests
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class AVL_Benchmark_tests
    {
            private const int startSize = 16;
            private const int inserts = 16;
            private const int numberOfTrees = 5;
            private List<AVLTree<int>> _trees;

            public AVL_Benchmark_tests()
            {
                plantTrees();
                LoadTreesWithEvenNums();
            }
            private void plantTrees()
            {
                _trees = new();
                for (int i = 0; i < numberOfTrees; i++)
                {
                    _trees.Add(new AVLTree<int>());
                }
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

            private void InsertInTreeNo(int treeNumber)
            {
                var tree = _trees[treeNumber];
                var size = tree.Count;
                var equalPart = size / inserts;
                if ((equalPart & 1) == 0) equalPart++;
                var numberToAdd = 1;

                for (int i = 0; i < inserts; i++)
                {
                    tree.Add(numberToAdd);
                    numberToAdd += equalPart;
                }
            }

            [Benchmark]
            public void Tree1_inserts()
            {
                InsertInTreeNo(0);
            }
            
            [Benchmark]
            public void Tree2_inserts()
            {
                InsertInTreeNo(1);
            }
            
            [Benchmark]
            public void Tree3_inserts()
            {
                InsertInTreeNo(2);
            }
            
            [Benchmark]
            public void Tree4_inserts()
            {
                InsertInTreeNo(3);
            }
            
            [Benchmark]
            public void Tree5_inserts()
            {
                InsertInTreeNo(4);
            }
            
    }
}