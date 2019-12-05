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
        public int Width { get; set; }

        int _currentNode = 0;
        int _maxNodeCount = 0;

        public Graph Generate(int nodeCount)
        {
            _currentNode = 0;
            _maxNodeCount = nodeCount;

            Graph graph = new Graph();

            List<List<int>> matrix = CreateMatrix(_maxNodeCount, _maxNodeCount + Width + 2);

            FillingNodeMatrix(matrix);

            FillingGraphByNodes(graph, matrix);

            FillingGraphByEdges(graph, matrix);

            return graph;
        }

        private void FillingGraphByNodes(Graph graph, List<List<int>> matrix)
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

        private void FillingGraphByEdges(Graph graph, List<List<int>> matrix)
        {
            Node startNode = null;
            Node endNode = null;

            graph.Nodes = graph.Nodes.OrderBy(n => n.Center.X).ThenBy(n => n.Center.Y).ToList<Node>();

            for (int i = 0; i < graph.Nodes.Count; i++)
            {
                startNode = graph.Nodes[i];

                for (int j = 0; j < graph.Nodes.Count; j++)
                {
                    if (i != j)
                    {
                        endNode = graph.Nodes[j];

                        Edge edge = new Edge(startNode, endNode, GetWeight(startNode, endNode));

                        if (graph.IsNotEgdeExist(edge))
                        {
                            graph.AddEdge(edge);
                        }
                    }
                }
            }
        }

        private Node GetNodeByCenter(Graph graph, Point center)
        {
            Node currentNode = null;

            foreach(Node node in graph.Nodes)
            {
                if (node.Center.X == center.X + 1 && node.Center.Y == center.Y + 1)
                {
                    currentNode = node;
                }
            }

            return currentNode;
        }

        private int GetWeight(Node start, Node end)
        {
            double weight = 0;

            double xCatet = Math.Abs(start.Center.X - end.Center.X);
            double yCatet = Math.Abs(start.Center.Y - end.Center.Y);

            double coef = 0;

            weight = Math.Round(Math.Sqrt(Math.Pow(xCatet, 2) + Math.Pow(xCatet, 2)));

            if (xCatet > yCatet)
            {
                coef = xCatet;
            }
            else
            {
                coef = yCatet;
            }

            if (weight == 0)
            {
                weight = coef;
            }
            else
            {
                weight *= coef;
            }

            return (int)weight;
        }
    }
}
