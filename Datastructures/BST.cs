using System;

namespace Datastructures
{
    public class BST<T> where T : IComparable
    {
        private Node<T> _root;
        public int Count;

        public BST()
        {
            _root = new();
            Count = 0;
        }
        public BST(T value)
        {
            _root = new (value);
            Count = 1;
        }

        public bool Add(T value)
        {
            if (Count == 0)
            {
                _root.Value = value;
                Count = 1;
                return true;
            }

            var current = _root;
            while (current.HasValue)
            {
                if (value.CompareTo(current.Value) == 0)
                    return false;
                if (value.CompareTo(current.Value) > 0)
                    current = current.Right;
                else current = current.Left;
            }

            current.Value = value;
            Count++;
            return true;
        }
    }
}