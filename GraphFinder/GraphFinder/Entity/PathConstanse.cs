using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFinder.Entity
{
    class PathConstanse
    {
        public List<Path> Paths { get; private set; }

        public PathConstanse()
        {
            Paths = new List<Path>();
        }

        public void AddPath(Path path)
        {
            Paths.Add(path);
        }

        public void Clear()
        {
            Paths.Clear();
        }

        public int GetIndexOfPathWithMinLength()
        {
            int minIndex = 0;

            for(int i = 0; i < Paths.Count - 1; i++)
            {
                if (Paths[i + 1].GetLength() < Paths[minIndex].GetLength())
                {
                    minIndex = i + 1;
                }
            }

            return minIndex;
        }

        public void SetShortestPath()
        {
            foreach (Edge edge in Paths[GetIndexOfPathWithMinLength()].Edges)
            {
                edge.IsPath = true;
            }
        }

        public int GetMinLength()
        {
            return Paths[GetIndexOfPathWithMinLength()].GetLength();
        }

        public Path GetPathWithMinLength()
        {
            return Paths[GetIndexOfPathWithMinLength()];
        }
    }
}
