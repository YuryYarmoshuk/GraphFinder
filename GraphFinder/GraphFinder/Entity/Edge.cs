using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFinder.Entity
{
    class Edge
    {
        public Node Start { get; private set; }
        public Node End { get; private set; }
        public int Weight { get; private set; }
        public bool IsPath { get; set; }

        public Edge(Node startNode, Node endNode, int weightOfEdge)
        {
            Start = startNode;
            End = endNode;
            Weight = weightOfEdge;
            IsPath = false;
        }
    }
}
