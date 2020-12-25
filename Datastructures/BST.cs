using System;

namespace Datastructures
{
    public class BST<T> where T : IComparable
    {
        private Node<T> _root;
        public int Count;

        public BST()
        {
        }
        public BST(T value)
        {
            _root = new (value);
            Count = 1;
        }

        public bool Insert(T value)
        {
            if (Count == 0)
            {
                _root = new(value);
                Count = 1;
                return true;
            }

            var current = _root;
            while (true)
            {
                if (value.CompareTo(current.Value) == 0)
                    return false;
                if (value.CompareTo(current.Value) > 0)
                {
                    if (current.Right is not null)
                        current = current.Right;
                    else
                    {
                        current.Right = new(value);
                        Count++;
                        return true;
                    }
                }                
                else if (current.Left is not null)
                    current = current.Left;
                else
                {
                    current.Left = new(value);
                    Count++;
                    return true;
                }
            }
        }

        public bool Search(T value) => Count != 0 && _root.Value.Equals(value) || findParentOf(value) is not null;

        public bool Delete(T value)
        {
            if (_root.Value.Equals(value))
            {
                
            }
            var parentOfTarget = _root.Value.Equals(value) ? _root : findParentOf(value);
            if (parentOfTarget is null)
                return false;
            var nodeToRemove = parentOfTarget.Left.Value.Equals(value)  ? parentOfTarget.Left : parentOfTarget.Right;
            if (nodeToRemove.Left is null)
            {
                if (nodeToRemove.Right is null)
                {
                    RemoveNode(value, parentOfTarget);
                    Count--;
                    return true;
                }

                var nextNode = nodeToRemove.Right;
                if (parentOfTarget.Left.Value.Equals(value))
                    parentOfTarget.Left = nextNode;
                else parentOfTarget.Right = nextNode;
            }
            else if (nodeToRemove.Right is null)
            {
                var nextNode = nodeToRemove.Left;
                if (parentOfTarget.Left.Value.Equals(value))
                    parentOfTarget.Left = nextNode;
                else parentOfTarget.Right = nextNode;
            }
            else
            {
                var smallestChild = Min(nodeToRemove.Right);
                nodeToRemove.Right.Value = smallestChild;
                Delete(smallestChild);
                return true;
            }
            Count--;
            return true;
        }

        private T Min(Node<T> node)
        {
            while (node.Left is not null)
                node = node.Left;
            
            return node.Value;
        }

        private T Max(Node<T> node)
        {
            while (node.Right is not null)
                node = node.Right;
            
            return node.Value;
        }

        private static void RemoveNode(T value, Node<T> fromParent)
        {
            if (fromParent.Left.Value.Equals(value))
                fromParent.Left = null;
            else
                fromParent.Right = null;
        }


        private Node<T> findParentOf(T value)
        {
            var current = _root;

            while (current is not null)
            {
                if (current.IsParentOf(value))
                    return current;
                if (value.CompareTo(current.Value) > 0)
                    current = current.Right;
                else
                    current = current.Left;
            }

            return current;
        }

    }
}