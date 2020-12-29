using System;
using System.Collections.Generic;
using System.Linq;

namespace Datastructures
{
    public class MaxHeap<T> where T : IComparable<T>
    {
        private T[] _array;
        public int Count { get; private set; }
        private int _capacity = 4;
        private static int ParentOf(int index) => (index - 1) / 2;
        private static int LeftOf(int index) => index * 2 + 1;
        private static int RightOf(int index) => index * 2 + 2;
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
                if (_array[ParentOf(index)].CompareTo(_array[index]) < 0)
                {
                    Swap(ParentOf(index), index);
                    index = ParentOf(index);
                }
                else break;
            }
        }

        private void BubbleDown(int index)
        {
            while (true)
            {
                if (LeftOf(index) >= Count)
                    break;
                var compare = RightOf(index) >= Count ? 1 : _array[LeftOf(index)].CompareTo(_array[RightOf(index)]);
                var childIndex = compare > 0 ? LeftOf(index) : RightOf(index);
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
            if (index >= Count)
                throw new IndexOutOfRangeException("Index does not exist.");
            var value = _array[index];
            _array[index] = _array[Count - 1];
            _array[Count - 1] = default;
            Count--;
            if (_array[index].CompareTo(_array[ParentOf(index)]) > 0)
                BubbleUp(index);
            else BubbleDown(index);
            return value;
        }
        
        public T Replace(T value)
        {
            if (value is null || value.CompareTo(default) == 0)
                throw new InvalidOperationException(
                    "Default value can not be inserted because the array is already initialised with default values.");
            var max = _array[0];
            _array[0] = value;
            BubbleDown(0);
            return max;
        }
    }
        public class MaxHeap<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Tuple<TKey, TValue>[] _array;
        public int Count { get; private set; }
        private int _capacity = 4;
        private static int ParentOf(int index) => (index - 1) / 2;
        private static int LeftOf(int index) => index * 2 + 1;
        private static int RightOf(int index) => index * 2 + 2;
        public Tuple<TKey, TValue>Max() => _array[0];

        public IEnumerable<TValue> Values()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _array[i].Item2;
            }
        }
        
        public MaxHeap()
        {
            _array = new Tuple<TKey, TValue>[_capacity];
        }
        
        public MaxHeap(TKey key, TValue value, int initialCapacity = 4)
        {
            _capacity = initialCapacity;
            _array = new Tuple<TKey, TValue>[_capacity];
            Insert(key, value);
        }


        public void Insert(TKey key, TValue value)
        {
            if (key is null || key.CompareTo(default) == 0)
                throw new InvalidOperationException(
                    "Default key can not be inserted because the array is already initialised with default keys.");
            if (Count == _capacity)
                Grow();
            _array[Count] = new(key, value);
            BubbleUp(Count);
            Count++;
        }

        private void BubbleUp(int index)
        {
            while (true)
            {
                if (_array[ParentOf(index)].Item1.CompareTo(_array[index].Item1) < 0)
                {
                    Swap(ParentOf(index), index);
                    index = ParentOf(index);
                }
                else break;
            }
        }

        private void BubbleDown(int index)
        {
            while (true)
            {
                if (LeftOf(index) >= Count)
                    break;
                var compare = RightOf(index) >= Count ? 1 : _array[LeftOf(index)].Item1.CompareTo(_array[RightOf(index)].Item1);
                var childIndex = compare > 0 ? LeftOf(index) : RightOf(index);
                if (_array[childIndex].Item1.CompareTo(_array[index].Item1) > 0)
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
            var newArray = new Tuple<TKey, TValue>[_capacity];
            Array.Copy(_array, newArray, Count);
            _array = newArray;
        }

        public IEnumerable<TValue> Search(TKey key)
        {
            if (key is null || key.CompareTo(default) == 0)
                throw new InvalidOperationException(
                    "Default key can not be inserted nor used as a search argument because the array " +
                    "is already initialised with default keys.");
            return _array.Where(x => x != null && x.Item1.CompareTo(key) == 0)
                .Select(t => t.Item2);
        }

        private IEnumerable<int> IndexOf(TKey key)
        {
            if (key is null || key.CompareTo(default) == 0)
                return Enumerable.Empty<int>();
            return _array.Where( entry => entry is not null)
                .Select((x, i) => new Tuple<TKey, int>(x.Item1, i))
                .Where(t => t.Item1.CompareTo(key) == 0).Select(t => t.Item2);
        }

        public Tuple<TKey, TValue> Delete() => Count == 0 ? default : DeleteAtIndex(0);
         
        public TValue Delete(TKey key)
        {
            var indices = IndexOf(key);
            if (!indices.Any())
                return default;
            var index = indices.FirstOrDefault();
            var value = _array[index].Item2;
            DeleteAtIndex(index);
            return value;
        }
        public Tuple<TKey, TValue> DeleteAtIndex(int index)
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();
            var tuple = _array[index];
            _array[index] = _array[Count - 1];
            _array[Count - 1] = default;
            Count--;
            if (index == Count)
                return tuple;
            if (_array[index].Item1.CompareTo(_array[ParentOf(index)].Item1) > 0)
                BubbleUp(index);
            else BubbleDown(index);
            return tuple;
        }
        
        public Tuple<TKey, TValue>Replace(TKey key, TValue value)
        {
            if (key is null || key.CompareTo(default) == 0)
                throw new InvalidOperationException(
                    "Default key can not be inserted because the array is already initialised with default keys.");
            var max = _array[0];
            _array[0] = new(key, value);
            BubbleDown(0);
            return max;
        }
    }

        public class MaxHeap
    {
        private int?[] _array;
        public int Count { get; private set; }
        private int _capacity = 4;
        private static int ParentOf(int index) => (index - 1) / 2;
        private static int LeftOf(int index) => index * 2 + 1;
        private static int RightOf(int index) => index * 2 + 2;
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
                if (_array[ParentOf(index)] <_array[index])
                {
                    Swap(ParentOf(index), index);
                    index = ParentOf(index);
                }
                else break;
            }
        }

        private void BubbleDown(int index)
        {
            while (true)
            {
                if (LeftOf(index) >= Count)
                    break;
                var compare = RightOf(index) >= Count ? 1 : _array[LeftOf(index)] - _array[RightOf(index)];
                var childIndex = compare > 0 ? LeftOf(index) : RightOf(index);
                if (_array[childIndex] > _array[index])
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
            if (index >= Count)
                throw new IndexOutOfRangeException("Index does not exist.");
            var value = _array[index];
            _array[index] = _array[Count - 1];
            _array[Count - 1] = null;
            Count--;
            if (index != 0 && _array[index] > _array[ParentOf(index)])
                BubbleUp(index);
            else BubbleDown(index);
            return value;
        }
        
        
        public int? Replace(int value)
        {
            var max = _array[0];
            _array[0] = value;
            BubbleDown(0);
            return max;
        }
    }

}