using System;

namespace Datastructures
{
    public class AVLNode<T> where T : IComparable<T>
    {
        public T Value { get; set; } 
        public AVLNode<T> Parent { get; set; }
        public AVLNode<T> Left { get; set; }
        public AVLNode<T> Right { get; set; }
        public int Height { get; set; } = 1;
        public int BalanceFactor => Right?.Height ?? 0 - Left?.Height ?? 0;
        public bool IsNotBalanced => Math.Abs(BalanceFactor) > 1;
        
        public AVLNode(T value)
        {
            Value = value;
        }

        public AVLNode(T value, AVLNode<T> parent)
            :this(value)
        {
            Parent = parent;
        }
        
        public int UpdateHeight()
        {
            Height = Math.Max(Right?.Height ?? 0, Left?.Height ?? 0) + 1;
            return Height;
        }

    }

    public class AVLNode<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public AVLNode<TKey, TValue> Parent { get; set; }
        public AVLNode<TKey, TValue> Left { get; set; }
        public AVLNode<TKey, TValue> Right { get; set; }
        public int Height { get; set; } = 1;
        public int BalanceFactor => Right?.Height ?? 0 - Left?.Height ?? 0;
        public bool IsNotBalanced => Math.Abs(BalanceFactor) > 1;

        public AVLNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
        
        public AVLNode(TKey key, TValue value, AVLNode<TKey, TValue> parent)
            : this(key, value)
        {
            Parent = parent;
        }
        
        public int UpdateHeight()
        {
            Height = Math.Max(Right?.Height ?? 0, Left?.Height ?? 0);
            return Height;
        }
    }
}