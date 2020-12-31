using System;

namespace Datastructures
{
    public class AVLTree<T> where T : IComparable<T>
    {
        private AVLNode<T> _root;
        public int Count { get; private set; }
        private static bool IsLeftChild(AVLNode<T> node) => node.Parent?.Left?.Equals(node) ?? false;
        private static bool IsRightChild(AVLNode<T> node) => node.Parent?.Right?.Equals(node) ?? false;

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
            var compare = value.CompareTo(node.Value);
            if (compare == 0)
                return false;
            if (compare > 0)
            {
                if (node.Right is null)
                {
                    node.Right = new (value, node);
                    Count++;
                    BalanceRecursive(node);
                    return true;
                }
                return Add(node.Right, value);
            }

            if (node.Left is null)
            {
                node.Left = new(value, node);
                Count++;
                BalanceRecursive(node);
                return true;
            }
            return Add(node.Left, value);
        }

        private void BalanceRecursive(AVLNode<T> node)
        {
            node.UpdateHeight();
            var parent = node.Parent;
            if (parent is null)
            {
                if (node.IsNotBalanced)
                    _root = Balance(node);
                return;
            }

            if (node.IsNotBalanced)
            {
                if (IsLeftChild(node))
                    parent.Left = Balance(node);
                else if (IsRightChild(node))
                    parent.Right = Balance(node);
            }
            
            if (parent.Height > node.Height)
                return;
            
            BalanceRecursive(node.Parent);
        }

        private AVLNode<T> Balance(AVLNode<T> node)
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

        private AVLNode<T> RotateLeft(AVLNode<T> parent)
        {
            var child = parent.Right;
            SwapRightOf(parent, child.Left);
            child.Parent = parent.Parent;
            SwapLeftOf(child, parent);
            child.Left.UpdateHeight();
            child.UpdateHeight();
            return child;
        }

        private static void SwapRightOf(AVLNode<T> parent, AVLNode<T> child)
        {
            if (child is not null)
                child.Parent = parent;
            parent.Right = child;
        }
        
        private static void SwapLeftOf(AVLNode<T> parent, AVLNode<T> child)
        {
            if (child is not null)
                child.Parent = parent;
            parent.Left = child;
        }

        private AVLNode<T> RotateRight(AVLNode<T> parent)
        {
            var child = parent.Left;
            SwapLeftOf(parent, child.Right);
            child.Parent = parent.Parent;
            SwapRightOf(child, parent);
            child.Right.UpdateHeight();
            child.UpdateHeight();
            return child;
        }


        public bool Search(T value) => Search(_root, value);

        private bool Search(AVLNode<T> node, T value)
        {
            if (node is null)
                return false;
            
            var compare = value.CompareTo(node.Value);
            if (compare == 0)
                return true;
            
            if (compare > 0)
                return Search(node.Right, value);
            
            return Search(node.Left, value);
        }
    }
}