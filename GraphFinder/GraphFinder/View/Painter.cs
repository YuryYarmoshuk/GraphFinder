using GraphFinder.Entity;
using GraphFinder.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphFinder.View
{
    class Painter
    {
        public Canvas Canvas { get; set; }
        public int NodeRadius { get; set; }
        public Graph Graph { get; set; }
        public bool IsEdgeShowed { get; set; }

        private NodeSearcher searcher = new NodeSearcher();

        private readonly int _columnCount;
        private readonly int _rowCount;


        public Painter(Canvas canvas, Graph graph)
        {
            Canvas = canvas;
            Graph = graph;

            _columnCount = searcher.MaxCountX(graph.Nodes);
            _rowCount = searcher.MaxCountY(graph.Nodes);

            NodeRadius = GetRadius();

            IsEdgeShowed = false;
        }

        private int GetRadius()
        {
            double width = Math.Round(Canvas.RenderSize.Width / _columnCount);
            double height = Math.Round(Canvas.RenderSize.Height / _rowCount);

            double radius = 0;

            if (width < height)
            {
                radius = width - width * 0.1;
            }
            else
            {
                radius = height - height * 0.1;
            }

            return (int)Math.Round(radius);
        }

        public void Draw()
        {
            List<Node> nodes = Graph.Nodes;
            List<Edge> edges = Graph.Edges;

            ClearingCanvas();

            DrawGrid();

            DrawNodes(nodes);

            if (IsEdgeShowed)
            {
                DrawEdges(edges);
            }

            DrawLabels(nodes, edges);
        }

        private void DrawGrid()
        {
            for (int i = 0; i < _columnCount; i++)
            {
                Line line = new Line();

                line.X1 = Canvas.RenderSize.Width / _columnCount * (i + 1);
                line.Y1 = 0;
                line.X2 = Canvas.RenderSize.Width / _columnCount * (i + 1);
                line.Y2 = Canvas.RenderSize.Height;

                line.Stroke = Brushes.Pink;
                line.StrokeThickness = 0.5;

                Canvas.Children.Add(line);
            }

            for (int i = 0; i < _rowCount; i++)
            {
                Line line = new Line();

                line.X1 = 0;
                line.Y1 = Canvas.RenderSize.Height / _rowCount * (i + 1);
                line.X2 = Canvas.RenderSize.Width;
                line.Y2 = Canvas.RenderSize.Height / _rowCount * (i + 1);

                line.Stroke = Brushes.Pink;
                line.StrokeThickness = 0.5;

                Canvas.Children.Add(line);
            }
        }

        public double GetCellWidth()
        {
            return Canvas.RenderSize.Width / _columnCount * 2 - Canvas.RenderSize.Width / _columnCount * 1;
        }

        public double GetCellHeight()
        {
            return Canvas.RenderSize.Height / _rowCount * 2 - Canvas.RenderSize.Height / _rowCount * 1;
        }

        private void DrawEdges(List<Edge> edges)
        {
            foreach (Edge edge in edges)
            {
                Canvas.Children.Add(CreateLine(edge));
            }
        }

        private void DrawNodes(List<Node> nodes)
        {
            Brush color;

            foreach (Node node in nodes)
            {
                
                if (node.IsPlayerOnNode)
                {
                    color = Brushes.Red;
                }
                else
                {
                    color = Brushes.Black;
                }

                Canvas.Children.Add(CreateEllipse(FindOffsetX(node), FindOffsetY(node), color));
            }
        }

        private void DrawLabels(List<Node> nodes, List<Edge> edges)
        {
            foreach (Node node in nodes)
            {
                Canvas.Children.Add(CreateText(node));
            }

            if (IsEdgeShowed)
            {
                foreach (Edge edge in edges)
                {
                    Canvas.Children.Add(CreateText(edge));
                }
            }
        }

        private void ClearingCanvas()
        {
            Canvas.Children.Clear();
        }

        private double FindOffsetY(Node node)
        {
            return Canvas.RenderSize.Height / _rowCount / 2 +
                Canvas.RenderSize.Height / _rowCount * (node.Center.Y - 1) -
                NodeRadius / 2;
        }

        private double FindOffsetX(Node node)
        {
            return Canvas.RenderSize.Width / _columnCount / 2 +
                Canvas.RenderSize.Width / _columnCount * (node.Center.X - 1) -
                NodeRadius / 2;
        }

        private int FindDirection(double start, double end)
        {
            int direction = 0;

            if (start < end)
            {
                direction = 1;
            }
            else if (start > end)
            {
                direction = -1;
            }

            return direction;
        }

        private Ellipse CreateEllipse(double offsetX, double offsetY, Brush color)
        {
            Ellipse ellipse = new Ellipse();

            ellipse.Width = NodeRadius;
            ellipse.Height = NodeRadius;
            ellipse.StrokeThickness = 1.5;
            ellipse.Stroke = color;

            ellipse.Margin = new Thickness(offsetX, offsetY, 0, 0);

            return ellipse;
        }

        private Line CreateLine(Edge edge)
        {
            Line line = new Line();

            line.X1 = FindOffsetX(edge.Start) + NodeRadius / 2 + 
                FindDirection(edge.Start.Center.X, edge.End.Center.X) * NodeRadius / 2;
            line.Y1 = FindOffsetY(edge.Start) + NodeRadius / 2 +
                FindDirection(edge.Start.Center.Y, edge.End.Center.Y) * NodeRadius / 2;
            line.X2 = FindOffsetX(edge.End) + NodeRadius / 2 +
                FindDirection(edge.End.Center.X, edge.Start.Center.X) * NodeRadius / 2;
            line.Y2 = FindOffsetY(edge.End) + NodeRadius / 2 +
                FindDirection(edge.End.Center.Y, edge.Start.Center.Y) * NodeRadius / 2;

            if (edge.IsPath)
            {
                line.Stroke = Brushes.Red;
            }
            else
            {
                line.Stroke = Brushes.Black;
            }
            line.StrokeThickness = 1.5;

            return line;
        }

        private TextBlock CreateText(Node node)
        {
            TextBlock text = new TextBlock();

            text.FontSize = NodeRadius - NodeRadius * 0.25;
            text.Text = node.Identifier;
            text.Margin = new Thickness(FindOffsetX(node) + NodeRadius / 3.5, FindOffsetY(node), 0, 0);

            return text;
        }

        private TextBlock CreateText(Edge edge)
        {
            TextBlock text = new TextBlock();

            text.FontSize = NodeRadius - NodeRadius * 0.25;
            text.Text = edge.Weight.ToString();
            text.Margin = new Thickness(
                Math.Abs(FindOffsetX(edge.Start) + FindOffsetX(edge.End)) / 2,
                Math.Abs(FindOffsetY(edge.Start) + FindOffsetY(edge.End)) / 2,
                0, 0);

            return text;
        }
    }
}
