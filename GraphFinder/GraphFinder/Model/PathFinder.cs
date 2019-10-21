using GraphFinder.Entity;
using GraphFinder.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFinder.Model
{
    class PathFinder
    {
        public PathConstanse PathConstanse { get; private set; }
        public Graph Graph { get; private set; }

        public PathFinder(PathConstanse pathConstanse, Graph graph)
        {
            PathConstanse = pathConstanse;
            Graph = graph;
        }

        public void Search()
        {
            List<Edge> edges = new List<Edge>();
            List<Edge> pathEdges = new List<Edge>();
            
            Node startNode = null;

            foreach(Node node in Graph.Nodes)
            {
                if(node.Identifier == "s")
                {
                    startNode = node;
                }
            }

            foreach(Edge edge in Graph.Edges)
            {
                if (edge.Start.Equals(startNode))
                {
                    edges.Add(edge);
                }
            }

            pathEdges.Add(edges[0]);

            Path path = new Path();

            foreach(Edge edge in pathEdges)
            {
                path.AddEdge(edge);
            }

            PathConstanse.AddPath(path);
            PathConstanse.SetShortestPath();
        }
    }
}
