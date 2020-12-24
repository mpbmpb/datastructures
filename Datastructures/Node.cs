using System;

namespace Datastructures
{
    public class Node<T> where T : IComparable
    {
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public bool HasValue => Value is not null;
        
        

        public Node()
        {
        }
        public Node(T value)
        {
            Value = value;
        }
    }
}