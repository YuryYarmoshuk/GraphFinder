using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphFinder.Entity;
using GraphFinder.Util;

namespace GraphFinder.Model
{
    class GraphGenerator
    {
        int _currentNode = 0;

        public Graph Generate(int nodeCount)
        {
            _currentNode = 0;

            Graph graph = new Graph();

            List<List<int>> matrix = CreateMatrix(nodeCount);

            FillNodeMatrix(matrix);

            return graph;
        }

        private List<List<int>> CreateMatrix(int nodeCount)
        {
            List<List<int>> matrix = new List<List<int>>();

            for(int i = 0; i < nodeCount; i++)
            {
                List<int> row = new List<int>();

                for(int j = 0; j < nodeCount + 2; j++)
                {
                    row.Add(0);
                }
                matrix.Add(row);
            }

            return matrix;
        }

        private void FillNodeMatrix(List<List<int>> matrix)
        {
            RandomGenerator generator = new RandomGenerator();

            int nodeRowIndex = generator.RandomRowNumber(0, matrix.Count - 1);
            matrix[nodeRowIndex][0] = 1;

            nodeRowIndex = generator.RandomRowNumber(0, matrix.Count - 1);
            matrix[nodeRowIndex][matrix.Count - 1] = 1;
        }
    }
}
