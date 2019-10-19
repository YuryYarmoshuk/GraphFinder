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


        public Node(Point point)
        {
            Center = point;
            Identifier = "";
        }

        public Node(Point point, string identifier)
        {
            Center = point;
            Identifier = identifier;
        }
    }
}
