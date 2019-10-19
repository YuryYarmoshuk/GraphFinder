using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GraphFinder.Entity;
using GraphFinder.Util;

namespace GraphFinder.Model
{
    class GraphGenerator
    {
        int _currentNode = 0;
        int _maxNodeCount = 0;

        public Graph Generate(int nodeCount)
        {
            _currentNode = 0;
            _maxNodeCount = nodeCount;

            Graph graph = new Graph();

            List<List<int>> matrix = CreateMatrix(_maxNodeCount, _maxNodeCount + 2);

            FillingNodeMatrix(matrix);

            FillingGraph(graph, matrix);

            return graph;
        }

        private void FillingGraph(Graph graph, List<List<int>> matrix)
        {
            int count = 1;

            for(int i = 0; i< matrix.Count; i++)
            {
                for(int j = 0; j< matrix[i].Count; j++)
                {
                    if(matrix[i][j] != 0)
                    {
                        Node node;

                        if (j == 0)
                        {
                            node = new Node(new Point(j + 1, i + 1), "s");
                        }
                        else if (j == matrix[i].Count - 1)
                        {
                            node = new Node(new Point(j + 1, i + 1), "e");
                        }
                        else
                        {
                            node = new Node(new Point(j + 1, i + 1), count.ToString());
                            count++;
                        }
                        graph.AddNode(node);
                    }
                }
            }
        }

        private List<List<int>> CreateMatrix(int rowCount, int columnCount)
        {
            List<List<int>> matrix = new List<List<int>>();

            for(int i = 0; i < rowCount; i++)
            {
                List<int> row = new List<int>();

                for(int j = 0; j < columnCount; j++)
                {
                    row.Add(0);
                }
                matrix.Add(row);
            }

            return matrix;
        }

        private void FillingNodeMatrix(List<List<int>> matrix)
        {
            RandomGenerator generator = new RandomGenerator();

            int nodeRowIndex = generator.RandomRowNumber(0, matrix.Count - 1);
            matrix[nodeRowIndex][0] = 1;

            nodeRowIndex = generator.RandomRowNumber(0, matrix.Count - 1);
            matrix[nodeRowIndex][matrix[nodeRowIndex].Count - 1] = 1;

            int nodeCount = 0;
            bool isEmpty;

            while (_currentNode < _maxNodeCount)
            {
                for (int i = 1; i < matrix[0].Count - 1; i++)
                {
                    if (_maxNodeCount - _currentNode >= 2)
                    {
                        nodeCount = generator.RandomNodeCount();
                    }
                    else if (_maxNodeCount - _currentNode == 1)
                    {
                        nodeCount = 1;
                    }
                    else
                    {
                        break;
                    }

                    _currentNode += nodeCount;

                    for (int j = 0; j < nodeCount; j++)
                    {
                        isEmpty = true;

                        while (isEmpty)
                        {
                            nodeRowIndex = generator.RandomRowNumber(0, matrix.Count - 1);

                            if (matrix[nodeRowIndex][i] == 0)
                            {
                                isEmpty = false;
                            }
                        }

                        matrix[nodeRowIndex][i] = 1;
                    }
                }
            }
        }
    }
}
