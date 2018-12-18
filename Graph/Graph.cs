using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Graph
{
    public class Graph<T> : IGraph<T>
    {
        private readonly List<List<T>> _routes;
        private readonly IEnumerable<ILink<T>> _links;

        public Graph(IEnumerable<ILink<T>> links)
        {
            _links = links;
            _routes = new List<List<T>>();
        }


        /// <summary>
        /// Find all paths from 'source' to 'target'
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public IObservable<IEnumerable<T>> RoutesBetween(T source, T target)
        {
            return Observable.Create<IEnumerable<T>>(observer =>
            {
                List<T> visiteds = new List<T>();
                List<T> paths = new List<T>
                {
                    source
                };
                AllRoutes(source, target, visiteds, paths);
                foreach (var route in _routes)
                {
                    observer.OnNext(route);
                }

                observer.OnCompleted();
                return Disposable.Empty;
            });
        }

        /// <summary>
        /// A recursive function to find all paths from 'source' to 'target' and stores the result in _routes variable. 
        /// 'visisteds' variable keeps track of vertices in current path. 
        /// 'paths' stores actual vertices in the current path 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="visiteds"></param>
        /// <param name="paths"></param>
        private void AllRoutes(T source, T target, List<T> visiteds, List<T> paths)
        {
            visiteds.Add(source);

            if (source.Equals(target))
            {
                var path = new List<T>();
                foreach (var p in paths)
                {
                    path.Add(p);
                }
                _routes.Add(path);
                visiteds.Remove(source);
                return;
            }

            foreach (var i in _links.Where(a => a.Source.Equals(source) && !visiteds.Any(b => b.Equals(a.Target))))
            {
                paths.Add(i.Target);
                AllRoutes(i.Target, target, visiteds, paths);

                paths.Remove(i.Target);
            }

            visiteds.Remove(source);
        }
    }
}
