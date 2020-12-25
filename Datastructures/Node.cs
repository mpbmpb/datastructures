using System;

namespace Datastructures
{
    public class Node<T> where T : IComparable
    {
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public bool IsParentOf(T value)
        {
            return (Left is not null && Left.Value.Equals(value))  
                   || (Right is not null && Right.Value.Equals(value));
        }
        
        

        public Node()
        {
        }
        public Node(T value)
        {
            Value = value;
        }
    }
}