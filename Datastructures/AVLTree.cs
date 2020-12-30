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
                    if (node.Height == 1)
                        UpdateHeightRecursive(node, 2);
                    return true;
                }
                return Add(node.Right, value);
            }

            if (node.Left is null)
            {
                node.Left = new(value, node);
                Count++;
                if (node.Height == 1)
                    UpdateHeightRecursive(node, 2);
                return true;
            }

            return Add(node.Left, value);
        }

        private void UpdateHeightRecursive(AVLNode<T> node, int height)
        {
            node.Height = height;
            var parent = node.Parent;
            if (parent is null)
            {
                if (node.IsNotBalanced)
                    _root = Balance(node);
                _root.UpdateHeight();
                return;
            }

            if (node.IsNotBalanced)
            {
                if (IsLeftChild(node))
                    parent.Left = Balance(node);
                else if (IsRightChild(node))
                    parent.Right = Balance(node);
                parent.UpdateHeight();
            }
            
            if (parent.Height > node.Height)
                return;
            
            UpdateHeightRecursive(node.Parent, height + 1);
        }

        //TODO: Balance(node)
        private AVLNode<T> Balance(AVLNode<T> node)
        {
            if (node.BalanceFactor > 0)
            {
                if (node.Right.BalanceFactor > 0)
                    return RotateLeft(node);
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            if (node.Left.BalanceFactor < 0)
                return RotateRight(node);
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        //TODO: RotateLeft(node)
        private AVLNode<T> RotateLeft(AVLNode<T> parent)
        {
            var child = parent.Right;
            parent.Right = child.Left;
            if (parent.Right is not null)
                parent.Right.Parent = parent;
            child.Left = parent;
            child.Parent = parent.Parent;
            child.Left.Parent = child;
            child.Left.UpdateHeight();
            child.UpdateHeight();
            return child;
        }
        
        //TODO: RotateRight(node)
        private AVLNode<T> RotateRight(AVLNode<T> parent)
        {
            var child = parent.Left;
            parent.Left = child.Right;
            if (parent.Left is not null)
                parent.Left.Parent = parent;
            child.Right = parent;
            child.Parent = parent.Parent;
            child.Right.Parent = child;
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