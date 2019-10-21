using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFinder.Entity
{
    class Path
    {
        public List<Edge> Edges { get; private set; }

        public Path()
        {
            Edges = new List<Edge>();
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        public int GetLength()
        {
            int length = 0;

            foreach(Edge edge in Edges)
            {
                length += edge.Weight;
            }

            return length;
        }
    }
}
