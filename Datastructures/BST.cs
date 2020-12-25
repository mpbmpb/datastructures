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
            try
            {
                _root = Insert(_root, value);
            }
            catch (Exception)
            {
                return false;
            }

            Count++;
            return true;
        }

        private Node<T> Insert(Node<T> node, T value)
        {
            if (node is null)
                return new Node<T>(value);
            if (node.Value.Equals(value))
                throw new ArgumentException("Key already exists.");
            if (value.CompareTo(node.Value) > 0)
                node.Right = Insert(node.Right, value);
            else node.Left = Insert(node.Left, value);
            
            return node;
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
            try
            {
                _root = Delete(_root, value);
            }
            catch (Exception)
            {
                return false;
            }

            Count--;
            return true;
        }
        
        private Node<T> Delete(Node<T> node, T value)
        {
            if (node is null) throw new KeyNotFoundException("Key to delete was not found.");
            var comparison = value.CompareTo(node.Value);

            if (comparison > 0)
                node.Right = Delete(node.Right, value);
            else if (comparison < 0)
                node.Left = Delete(node.Left, value);
            else
            {
                if (node.Left is null)
                    return node.Right;
                if (node.Right is null)
                    return node.Left;

                node.Value = Min(node.Right).Value;
                node.Right = Delete(node.Right, node.Value);
            }

            return node;
        }

        public Node<T> Min() => Min(_root);
        
        private Node<T> Min(Node<T> node)
        {
            while (node?.Left is not null)
                node = node.Left;
            
            return node;
        }

        public Node<T> Max() => Max(_root);

        private Node<T> Max(Node<T> node)
        {
            while (node?.Right is not null)
                node = node.Right;
            
            return node;
        }

        public IEnumerable<T> InOrder() => InOrder(_root);

        private IEnumerable<T> InOrder(Node<T> node)
        {
            if (node is null) yield break;
            
            if (node.Left is not null)
            {
                foreach (var val in InOrder(node.Left))
                    yield return val;
            }

            yield return node.Value;

            if (node.Right is not null)
            {
                foreach (var val in InOrder(node.Right))
                    yield return val;
            }        
        }
    }
}