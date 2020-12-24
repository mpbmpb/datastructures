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

        public bool Search(T value)
        {
            var current = _root;

            while (current is not null)
            {
                if (value.CompareTo(current.Value) == 0)
                    return true;
                if (value.CompareTo(current.Value) > 0)
                    current = current.Right;
                else
                    current = current.Left;
            }
            return false;
        }

        public bool Delete(T value)
        {
            var current = _root;

            while (current is not null)
            {
                if (value.CompareTo(current.Value) == 0)
                {
                    return true;
                }
                if (value.CompareTo(current.Value) > 0)
                    current = current.Right;
                else
                    current = current.Left;
            }

            return false;
        }

    }
}