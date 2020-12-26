using System;

namespace Datastructures
{
    public class MaxHeap<T> where T : IComparable<T>
    {
        private T[] _array;
        public int Count { get; private set; }
        private int _capacity = 4;
        
        public MaxHeap()
        {
            _array = new T[_capacity];
        }
        
        public MaxHeap(T value, int initialCapacity = 4)
        {
            _capacity = initialCapacity;
            _array = new T[_capacity];
            Insert(value);
        }


        public void Insert(T value)
        {
            if (Count == _capacity)
                Grow();
            _array[Count] = value;
            Count++;
        }

        private void Grow()
        {
            _capacity *= 2;
            var newArray = new T[_capacity];
            Array.Copy(_array, newArray, Count - 1);
            _array = newArray;
        }
    }
}