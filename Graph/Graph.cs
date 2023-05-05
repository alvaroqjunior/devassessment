using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Graph
{
    public interface IGraph<T>
    {
        IObservable<IEnumerable<T>> RoutesBetween(T source, T target);
    }

    public class Graph<T> : IGraph<T>
    {
        Dictionary<T, List<T>> graphConnectionsList = new Dictionary<T, List<T>>();

        public Graph(IEnumerable<ILink<T>> links)
        {
            // Cria um dicionário para armazenar os vertices dos grafos e suas possives arestas existentes
            graphConnectionsList = new Dictionary<T, List<T>>();

            foreach (var link in links)
            {
                if (!graphConnectionsList.ContainsKey(link.Source))
                    graphConnectionsList.Add(link.Source, new List<T>());

                graphConnectionsList[link.Source].Add(link.Target);
            }
        }

        public IObservable<IEnumerable<T>> RoutesBetween(T source, T destination)
        {
            return Observable.Create<IEnumerable<T>>(observer =>
            {
                // Cria uma lista para armazenar os vertices visitados e outra lista para o caminho realizado 
                var visited = new HashSet<T>();
                var path = new List<T>();

                FindAllPossiblesRoutes(source, destination, visited, path, observer);

                observer.OnCompleted();

                // Retorna um Action que será chamado quando o Observable for descartado
                return () => { };
            });
        }

        private void FindAllPossiblesRoutes(T current, T target, HashSet<T> visited, List<T> path, IObserver<IEnumerable<T>> observer)
        {
            visited.Add(current);
            path.Add(current);

            if (current.Equals(target))
            {
                observer.OnNext(new List<T>(path));
            }
            else if (graphConnectionsList.ContainsKey(current))
            {
                foreach (var neighbor in graphConnectionsList[current])
                {
                    if (!visited.Contains(neighbor))
                        FindAllPossiblesRoutes(neighbor, target, visited, path, observer);
                }
            }

            visited.Remove(current);
            path.RemoveAt(path.Count - 1);
        }

        //private bool HasConnectionsOnGraph(int i, string destination)
        //{
        //    if (_connections.Contains(array[i].Source) || _connections.Contains(array[i].Target))
        //        return true;        

        //    return false;
        //}
    }
}
//var j = 0;

//_connections.Add(new { array[j].Source, array[j].Target });

//for (int i = 0; i < array.Length; i++) // O (n)
//{
//    if (HasConnectionsOnGraph(i, destination.ToString()))
//        _connections.Add(new { array[i].Source, array[i].Target });

//    if (array[i].Source == destination.ToString() || array[i].Target == destination.ToString())
//        continue;
//}