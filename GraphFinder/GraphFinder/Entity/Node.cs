using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphFinder.Entity
{
    class Node
    {
        public Point Center { get; private set; }
        public string Identifier { get; private set; }
        public int Weight { get; set; }
        public bool IsPlayerOnNode { get; set; }


        public Node(Point point)
        {
            Center = point;
            Identifier = "";
            Weight = -1;
            IsPlayerOnNode = false;
        }

        public Node(Point point, string identifier)
        {
            Center = point;
            Identifier = identifier;
            Weight = -1;
            IsPlayerOnNode = false;
        }

        public override string ToString()
        {
            return String.Format("Node: {0} - ({1},{2})", Identifier, Center.X, Center.Y);
        }
    }
}
