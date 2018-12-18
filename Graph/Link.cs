﻿namespace Graph
{
    public class Link<T> : ILink<T>
    {
        public Link(T source, T target)
        {
            Source = source;
            Target = target;
        }

        public T Source { get; private set; }
        public T Target { get; private set; }

        public override string ToString()
        {
            return $"{Source?.ToString()} -> {Target?.ToString()}";
        }
    }
}
