using System;
using System.Collections.Generic;

namespace Datastructures
{
    public class MaxHeap<T> where T : IComparable<T>
    {
        private T[] _array;
        public int Count { get; private set; }
        private int _capacity = 4;
        private static int parentOf(int index) => (index - 1) / 2;
        private static int goLeft(int index) => index * 2 + 1;
        private static int goRight(int index) => index * 2 + 2;
        public T Max() => _array[0];

        public IEnumerable<T> Values()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _array[i];
            }
        }
        
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
            if (value is null || value.CompareTo(default) == 0)
                throw new InvalidOperationException(
                    "Default value can not be inserted because the array is already initialised with default values.");
            if (Count == _capacity)
                Grow();
            _array[Count] = value;
            BubbleUp(Count);
            Count++;
        }

        private void BubbleUp(int index)
        {
            while (true)
            {
                if (_array[parentOf(index)].CompareTo(_array[index]) < 0)
                {
                    Swap(parentOf(index), index);
                    index = parentOf(index);
                }
                else break;
            }
        }

        private void BubbleDown(int index)
        {
            while (true)
            {
                if (goLeft(index) >= Count)
                    break;
                var compare = goRight(index) >= Count ? 1 : _array[goLeft(index)].CompareTo(_array[goRight(index)]);
                var childIndex = compare > 0 ? goLeft(index) : goRight(index);
                if (_array[childIndex].CompareTo(_array[index]) > 0)
                {
                    Swap(index, childIndex);
                    index = childIndex;
                }
                else break;
            }
        }

        private void Swap(int first, int second)
        {
            var copy = _array[first];
            _array[first] = _array[second];
            _array[second] = copy;
        }

        private void Grow()
        {
            _capacity *= 2;
            var newArray = new T[_capacity];
            Array.Copy(_array, newArray, Count);
            _array = newArray;
        }

        public bool Search(T value)
        {
            if (value is null || value.CompareTo(default) == 0)
                throw new InvalidOperationException(
                    "Default value can not be inserted because the array is already initialised with default values.");
            return Array.IndexOf(_array, value) != -1;
        }        
        public T Delete() => DeleteAtIndex(0);
         
        public bool Delete(T value)
        {
            var index = Array.IndexOf(_array, value);
            if (index == -1)
                return false;
            DeleteAtIndex(index);
            return true;
        }
        public T DeleteAtIndex(int index)
        {
            var value = _array[index];
            _array[index] = _array[Count - 1];
            _array[Count - 1] = default;
            Count--;
            if (_array[index].CompareTo(_array[parentOf(index)]) > 0)
                BubbleUp(index);
            else BubbleDown(index);
            return value;
        }
    }
        public class MaxHeap
    {
        private int?[] _array;
        public int Count { get; private set; }
        private int _capacity = 4;
        private static int parentOf(int index) => (index - 1) / 2;
        private static int goLeft(int index) => index * 2 + 1;
        private static int goRight(int index) => index * 2 + 2;
        public int? Max() => _array[0];
        public IEnumerable<int> Values()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return (int)_array[i];
            }
        }
        
        public MaxHeap()
        {
            _array = new int?[_capacity];
        }
        
        public MaxHeap(int value, int initialCapacity = 4)
        {
            _capacity = initialCapacity;
            _array = new int?[_capacity];
            Insert(value);
        }


        public void Insert(int value)
        {
            if (Count == _capacity)
                Grow();
            _array[Count] = value;
            BubbleUp(Count);
            Count++;
        }

        private void BubbleUp(int index)
        {
            while (true)
            {
                if (_array[parentOf(index)] <_array[index])
                {
                    Swap(parentOf(index), index);
                    index = parentOf(index);
                }
                else break;
            }
        }

        private void BubbleDown(int index)
        {
            while (true)
            {
                if (goLeft(index) >= Count)
                    break;
                var compare = goRight(index) >= Count ? -1 : _array[goLeft(index)] - _array[goRight(index)];
                var childIndex = compare < 0 ? goLeft(index) : goRight(index);
                if (_array[childIndex] < _array[index])
                {
                    Swap(index, childIndex);
                    index = childIndex;
                }
                else break;
            }
        }

        private void Swap(int first, int second)
        {
            var copy = _array[first];
            _array[first] = _array[second];
            _array[second] = copy;
        }

        private void Grow()
        {
            _capacity *= 2;
            var newArray = new int?[_capacity];
            Array.Copy(_array, newArray, Count);
            _array = newArray;
        }

        public bool Search(int value) => Array.IndexOf(_array, value) != -1;


        public int? Delete() => DeleteAtIndex(0);
        
        public bool Delete(int value)
        {
            var index = Array.IndexOf(_array, value);
            if (index == -1)
                return false;
            DeleteAtIndex(index);
            return true;
        }
        public int? DeleteAtIndex(int index)
        {
            var value = _array[index];
            _array[index] = _array[--Count];
            _array[Count] = null;
            if (_array[index] > _array[parentOf(index)])
                BubbleUp(index);
            else BubbleDown(index);
            return value;
        }
    }

}