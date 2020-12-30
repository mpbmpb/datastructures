using System;
using System.Collections.Generic;

namespace Datastructures
{
    public class BST<T> where T : IComparable<T>
    {
        private Node<T> _root;
        public int Count { get; private set; }

        public BST()
        {
        }
        public BST(T value)
        {
            _root = new (value);
            Count = 1;
        }

        public bool Add(T value)
        {
            try
            {
                _root = Add(_root, value);
            }
            catch (Exception)
            {
                return false;
            }

            Count++;
            return true;
        }

        private Node<T> Add(Node<T> node, T value)
        {
            if (node is null)
                return new Node<T>(value);
            if (node.Value.Equals(value))
                throw new ArgumentException("Key already exists.");
            if (value.CompareTo(node.Value) > 0)
                node.Right = Add(node.Right, value);
            else node.Left = Add(node.Left, value);
            
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

        public void Clear()
        {
            _root = null;
            Count = 0;
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

    public class BST<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node<TKey, TValue> _root;
        public int Count;

        public BST()
        {
        }
        public BST(TKey key, TValue value)
        {
            _root = new (key, value);
            Count = 1;
        }
        
        public bool Add(TKey key, TValue value)
        {
            try
            {
                _root = Add(_root, key, value);
            }
            catch (Exception)
            {
                return false;
            }

            Count++;
            return true;
        }

        private Node<TKey, TValue> Add(Node<TKey, TValue> node, TKey key, TValue value)
        {
            if (node is null)
                return new (key, value);
            if (node.Key.Equals(key))
                throw new ArgumentException("Key already exists.");
            if (key.CompareTo(node.Key) > 0)
                node.Right = Add(node.Right, key, value);
            else node.Left = Add(node.Left, key, value);
            
            return node;
        }
        
        public Node<TKey, TValue> Search(TKey key)
        {
            var current = _root;

            while (current is not null)
            {
                if (current.Key.Equals(key))
                    return current;
                current = key.CompareTo(current.Key) > 0 ? current.Right : current.Left;
            }
            return null;
        }
        
                
        public bool Delete(TKey key)
        {
            try
            {
                _root = Delete(_root, key);
            }
            catch (Exception)
            {
                return false;
            }

            Count--;
            return true;
        }
        
        private Node<TKey, TValue> Delete(Node<TKey, TValue> node, TKey key)
        {
            if (node is null) throw new KeyNotFoundException("Key to delete was not found.");
            var comparison = key.CompareTo(node.Key);

            if (comparison > 0)
                node.Right = Delete(node.Right, key);
            else if (comparison < 0)
                node.Left = Delete(node.Left, key);
            else
            {
                if (node.Left is null)
                    return node.Right;
                if (node.Right is null)
                    return node.Left;

                node.Key = Min(node.Right).Key;
                node.Right = Delete(node.Right, node.Key);
            }

            return node;
        }
        
        public Node<TKey, TValue> Min() => Min(_root);
        
        private Node<TKey, TValue> Min(Node<TKey, TValue> node)
        {
            while (node?.Left is not null)
                node = node.Left;
            
            return node;
        }

        public Node<TKey, TValue> Max() => Max(_root);

        private Node<TKey, TValue> Max(Node<TKey, TValue> node)
        {
            while (node?.Right is not null)
                node = node.Right;
            
            return node;
        }
        
        public IEnumerable<TValue> InOrder() => InOrder(_root);

        private IEnumerable<TValue> InOrder(Node<TKey, TValue> node)
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