using System;
using System.Collections.Generic;

namespace Datastructures
{
    public class BST<T> where T : IComparable<T>
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

        public Node<T> Search(T value)
        {
            var current = _root;

            while (current is not null)
            {
                if (current.Value.Equals(value))
                    return current;
                current = value.CompareTo(current.Value) > 0 ? current.Right : current.Left;
            }
            return null;
        }
        public bool Delete(T value)
        {
            var parent = _root;
            var current = _root;

            while (current is not null && !current.Value.Equals(value))
            {
                parent = current;
                current = value.CompareTo(current.Value) > 0 ? current.Right : current.Left;
            }
            if (current is null) return false;

            if (current.Left is null && current.Right is null)
            {
                    RemoveCurrentNode();
                    return true;
            }
            if (current.Left is null)
            {
                if (parent.Right?.Value.Equals(value) ?? false)
                    pullUpNodeToRight(current.Right);
                else pullUpNodeToLeft(current.Right);
            }
            else if (current.Right is null)
            {
                if (parent.Right?.Value.Equals(value) ?? false)
                    pullUpNodeToRight(current.Left);
                else pullUpNodeToLeft(current.Left);
            }
            else
            {
                var nextOrderedSuccessor = Min(current.Right).Value;
                var nodeToUpdate = current;
                Delete(nextOrderedSuccessor);
                nodeToUpdate.Value = nextOrderedSuccessor;
                return true;
            }
            Count--;
            return true;
            
            
            void RemoveCurrentNode()
            {
                if (_root.Value.Equals(value))
                    _root = null;
                if (parent.Left?.Value.Equals(value) ?? false)
                    parent.Left = null;
                else
                    parent.Right = null;
                Count--;
            }
            
            void pullUpNodeToRight(Node<T> node)
            {
                if (_root.Value.Equals(value))
                    _root = node;
                else parent.Right = node;
            }

            void pullUpNodeToLeft(Node<T> node)
            {
                if (_root.Value.Equals(value))
                    _root = node;
                else parent.Left = node;
            }
        }

        private Node<T> Min(Node<T> node)
        {
            while (node.Left is not null)
                node = node.Left;
            
            return node;
        }

        public Node<T> Min() => Min(_root);

        private Node<T> Max(Node<T> node)
        {
            while (node.Right is not null)
                node = node.Right;
            
            return node;
        }

        public Node<T> Max() => Max(_root);

        public IEnumerable<T> InOrder()
        {
            var lineage = new Stack<Node<T>>();
            var current = _root;
            FindNextMin(current);
            
            while (lineage.Count > 0)
            {
                current = lineage.Pop();
                yield return current.Value;
                while (current.Right is not null)
                {
                    FindNextMin(current.Right);
                    current = lineage.Pop();
                    yield return current.Value;
                }
            }
            yield break;
            
            void FindNextMin(Node<T> node)
            {
                while (node is not null)
                {
                    lineage.Push(node);
                    node = node.Left;
                }
            }
        }
    }
}