using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Datastructures
{
    public class AVLTree<T> where T : IComparable<T>
    {
        private AVLNode<T> _root;
        public int Count { get; private set; }
        private static bool IsLeftChild(AVLNode<T> node) => node?.Parent?.Left?.Equals(node) ?? false;
        private static bool IsRightChild(AVLNode<T> node) => node?.Parent?.Right?.Equals(node) ?? false;

        public AVLTree()
        {
        }

        public AVLTree(T value)
        {
            _root = new(value);
            Count = 1;
        }

        public bool Add(T value)
        {
            if (_root is null)
            {
                _root = new(value);
                Count = 1;
                return true;
            }
            return Add(_root, value);
        }

        private bool Add(AVLNode<T> node, T value)
        {
            while (true)
            {
                var compare = value.CompareTo(node.Value);
                if (compare == 0) return false;
                if (compare > 0)
                {
                    if (node.Right is null)
                    {
                        node.Right = new(value, node);
                        Count++;
                        BalanceRecursive(node);
                        return true;
                    }

                    node = node.Right;
                    continue;
                }

                if (node.Left is null)
                {
                    node.Left = new(value, node);
                    Count++;
                    BalanceRecursive(node);
                    return true;
                }

                node = node.Left;
            }
        }

        public void Clear()
        {
            _root = null;
            Count = 0;
        }

        public bool Delete(T value) => value.CompareTo(Delete(Search(_root, value)) ) == 0;
            
        private T Delete(AVLNode<T> nodeToDelete)
        {
            if (nodeToDelete is null)
                return default;
            var value = nodeToDelete.Value;
            
            if (nodeToDelete.Left is null)
            {
                if (nodeToDelete.Right is null)
                    RemoveNodeFromParent(nodeToDelete);
                else if (IsLeftChild(nodeToDelete))
                    SwapLeftOf(nodeToDelete.Parent, nodeToDelete.Right);
                else if (IsRightChild(nodeToDelete))
                    SwapRightOf(nodeToDelete.Parent, nodeToDelete.Right);
                else
                    _root = nodeToDelete.Right;
                BalanceRecursive(nodeToDelete.Right);
                Count--;
                return value;
            }

            if (nodeToDelete.Right is null)
            {
                if (IsLeftChild(nodeToDelete))
                    SwapLeftOf(nodeToDelete.Parent, nodeToDelete.Left);
                else if (IsRightChild(nodeToDelete))
                    SwapRightOf(nodeToDelete.Parent, nodeToDelete.Left);
                else
                    _root = nodeToDelete.Left;
                BalanceRecursive(nodeToDelete.Left);
                Count--;
                return value;
            }

            if (IsLeftChild(nodeToDelete) || (nodeToDelete.Parent is null && nodeToDelete.BalanceFactor > 0))
                nodeToDelete.Value = Delete(Min(nodeToDelete.Right));
            else
                nodeToDelete.Value = Delete(Max(nodeToDelete.Left));

            BalanceRecursive(nodeToDelete);
            return value;
        }

        private void RemoveNodeFromParent(AVLNode<T> nodeToDelete)
        {
            if (nodeToDelete.Parent is null)
                _root = null;
            else if (IsLeftChild(nodeToDelete))
                nodeToDelete.Parent.Left = null;
            else
                nodeToDelete.Parent.Right = null;
        }


        private void BalanceRecursive(AVLNode<T> node)
        {
            while (node is not null)
            {
                node.UpdateHeight();
                var parent = node.Parent;
                if (parent is null)
                {
                    if (node.IsNotBalanced) _root = Balance(node);
                    return;
                }

                if (node.IsNotBalanced)
                {
                    if (IsLeftChild(node))
                        parent.Left = Balance(node);
                    else if (IsRightChild(node)) parent.Right = Balance(node);
                }
                node = node.Parent;
            }
        }

        private static AVLNode<T> Balance(AVLNode<T> node)
        {
            if (node.BalanceFactor > 0)
            {
                if (node.Right.BalanceFactor < 0)
                    node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            if (node.Left.BalanceFactor > 0)
                node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        private static AVLNode<T> RotateLeft(AVLNode<T> node)
        {
            var child = node.Right;
            SwapRightOf(node, child.Left);
            child.Parent = node.Parent;
            SwapLeftOf(child, node);
            node.UpdateHeight();
            child.UpdateHeight();
            return child;
        }

        private static void SwapRightOf(AVLNode<T> first, AVLNode<T> second)
        {
            if (second is not null)
                second.Parent = first;
            first.Right = second;
        }
        
        private static void SwapLeftOf(AVLNode<T> first, AVLNode<T> second)
        {
            if (second is not null)
                second.Parent = first;
            first.Left = second;
        }

        private static AVLNode<T> RotateRight(AVLNode<T> node)
        {
            var child = node.Left;
            SwapLeftOf(node, child.Right);
            child.Parent = node.Parent;
            SwapRightOf(child, node);
            node.UpdateHeight();
            child.UpdateHeight();
            return child;
        }

        public IEnumerable<T> InOrder()
        {
            var node = _root;
            if (node is null) yield break;
            node = Min(node);

            while (node is not null)
            {
                yield return node.Value;
                if (node.Right is not null)
                    node = Min(node.Right);
                else
                {
                    while (IsRightChild(node))
                        node = node.Parent;
                    node = node.Parent;
                }
            }
        }

        public IEnumerable<T> InReverseOrder()
        {
            var node = _root;
            if (node is null) yield break;
            node = Max(node);

            while (node is not null)
            {
                yield return node.Value;
                if (node.Left is not null)
                    node = Max(node.Left);
                else
                {
                    while (IsLeftChild(node))
                        node = node.Parent;
                    node = node.Parent;
                }
            }
        }

        public T Max() => Max(_root).Value ?? default;

        private static AVLNode<T> Max(AVLNode<T> node)
        {
            while (node.Right is not null)
                node = node.Right;

            return node;
        }

        public T Min() => Min(_root).Value ?? default;
        
        private static AVLNode<T> Min(AVLNode<T> node)
        {
            while (node.Left is not null)
                node = node.Left;

            return node;
        }

        public bool Search(T value) => Search(_root, value) is not null;

        private static AVLNode<T> Search(AVLNode<T> node, T value)
        {
            while (true)
            {
                if (node is null) return null;

                var compare = value.CompareTo(node.Value);
                switch (compare)
                {
                    case 0:
                        return node;
                    case > 0:
                        node = node.Right;
                        continue;
                    default:
                        node = node.Left;
                        break;
                }
            }
        }
    }

    public class AVLTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private AVLNode<TKey, TValue> _root;
        public int Count { get; private set; }
        private static bool IsLeftChild(AVLNode<TKey, TValue> node) => node.Parent?.Left?.Equals(node) ?? false;
        private static bool IsRightChild(AVLNode<TKey, TValue> node) => node.Parent?.Right?.Equals(node) ?? false;

        public AVLTree()
        {
        }

        public AVLTree(TKey key, TValue value)
        {
            _root = new(key, value);
            Count = 1;
        }

    }
}