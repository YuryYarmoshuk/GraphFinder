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

            int min = 0;
            bool isEnded = false;

            foreach(Node node in Graph.Nodes)
            {
                if(node.Identifier == "s")
                {
                    startNode = node;
                }
            }

            while (!isEnded)
            {
                if (startNode == null)
                {
                    startNode = pathEdges[pathEdges.Count - 1].End;
                }

                foreach (Edge edge in Graph.Edges)
                {
                    if (edge.Start.Equals(startNode))
                    {
                        edges.Add(edge);
                    }
                }

                for (int i = 0; i < edges.Count - 1; i++)
                {
                    if (edges[min].Weight > edges[i + 1].Weight)
                    {
                        min = i + 1;
                    }
                }

                pathEdges.Add(edges[min]);
                min = 0;
                startNode = null;
                edges.Clear();

                if (pathEdges[pathEdges.Count - 1].End.Identifier == "e")
                {
                    isEnded = true; ;
                }
            }
            
            Path path = new Path();
          
            foreach(Edge edge in pathEdges)
            {
                path.AddEdge(edge);
            }

            PathConstanse.AddPath(path);
            PathConstanse.SetShortestPath();
        }

        public void DeyxtraSearch()
        {
            List<Edge> edges = new List<Edge>();
            List<Edge> pathEdges = new List<Edge>();

            Node startNode = null;

            int min = 0;
            bool isEnded = false;

            foreach (Node node in Graph.Nodes)
            {
                if (node.Identifier == "s")
                {
                    startNode = node;
                    node.Weight = 0;
                }
            }

            while (!isEnded)
            {
                if (startNode == null)
                {
                    startNode = pathEdges[pathEdges.Count - 1].End;
                }

                foreach (Edge edge in Graph.Edges)
                {
                    if (edge.Start.Equals(startNode))
                    {
                        edges.Add(edge);
                    }
                }

                foreach (Edge edge in edges)
                {
                    if (edge.End.Weight == -1)
                    { 
                        edge.End.Weight = edge.Start.Weight + edge.Weight;
                    }
                }

                
            }
        }
    }
}
