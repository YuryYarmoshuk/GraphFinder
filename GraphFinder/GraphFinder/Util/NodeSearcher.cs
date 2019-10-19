using GraphFinder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFinder.Util
{
    class NodeSearcher
    {

        public NodeSearcher()
        {

        }

        public int MaxCountX(List<Node> nodes)
        {
            int maxIndex = 0;

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                if (nodes[maxIndex].Center.X < nodes[i + 1].Center.X)
                {
                    maxIndex = i + 1;
                }
            }

            return (int)nodes[maxIndex].Center.X;
        }

        public int MaxCountY(List<Node> nodes)
        {
            int maxIndex = 0;

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                if (nodes[maxIndex].Center.Y < nodes[i + 1].Center.Y)
                {
                    maxIndex = i + 1;
                }
            }

            return (int)nodes[maxIndex].Center.Y;
        }
    }
}
