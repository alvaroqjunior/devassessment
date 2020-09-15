using System;
using System.Collections.Generic;

namespace Graph
{
    public interface IGraph<T>
    {
        IObservable<IEnumerable<T>> RoutesBetween(T source, T target);
    }

    public class Graph<T> : IGraph<T>
    {
        public Graph(IEnumerable<ILink<T>> links)
        {

        }

        public IObservable<IEnumerable<T>> RoutesBetween(T source, T target)
        {
            throw new NotImplementedException();
        }
    }
}
