using System;
using System.Linq;
using System.Reactive.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graph.Tests
{
    [TestClass]
    public class GraphTest
    {
        ILink<string>[] _links;
        Graph<string> _graph;
        [TestInitialize]
        public void Initialize()
        {
            _links = new ILink<string>[]
            {
                new Link<string>("a","b"),
                new Link<string>("b","c"),
                new Link<string>("c","b"),
                new Link<string>("b","a"),
                new Link<string>("c","d"),
                new Link<string>("d","e"),
                new Link<string>("d","a"),
                new Link<string>("a","h"),
                new Link<string>("h","g"),
                new Link<string>("g","f"),
                new Link<string>("f","e"),
            };

            _graph = new Graph<string>(_links);
        }

        [TestMethod]
        public void TestRoutesBetweenTwoPoints()
        {
            

            var paths = _graph.RoutesBetween("a", "e");

            var list = paths.ToEnumerable().ToArray();
            Assert.AreEqual(2, list.Length);

            Assert.IsTrue(list.Any(l => String.Join("-", l) == "a-b-c-d-e"));
            Assert.IsTrue(list.Any(l => String.Join("-", l) == "a-h-g-f-e"));
        }


        [TestMethod]
        public void TestRoutesBetween_a_c()
        {
            var paths = _graph.RoutesBetween("a", "c");

            var list = paths.ToEnumerable().ToArray();
            Assert.AreEqual(1, list.Length);

            Assert.IsTrue(list.Any(l => String.Join("-", l) == "a-b-c"));
        }

        [TestMethod]
        public void TestRoutesBetween_a_d()
        {
            var paths = _graph.RoutesBetween("a", "d");

            var list = paths.ToEnumerable().ToArray();
            Assert.AreEqual(1, list.Length);

            Assert.IsTrue(list.Any(l => String.Join("-", l) == "a-b-c-d"));
        }

        [TestMethod]
        public void TestRoutesBetween_d_e()
        {
            var paths = _graph.RoutesBetween("d", "e");

            var list = paths.ToEnumerable().ToArray();
            Assert.AreEqual(2, list.Length);

            Assert.IsTrue(list.Any(l => String.Join("-", l) == "d-e"));
            Assert.IsTrue(list.Any(l => String.Join("-", l) == "d-a-h-g-f-e"));
        }

        [TestMethod]
        public void TestRoutesBetween_a_a()
        {
            var paths = _graph.RoutesBetween("a", "a");

            var list = paths.ToEnumerable().ToArray();
            Assert.AreEqual(1, list.Length);

            Assert.IsTrue(list.Any(l => String.Join("-", l) == "a"));            
        }
    }
}
