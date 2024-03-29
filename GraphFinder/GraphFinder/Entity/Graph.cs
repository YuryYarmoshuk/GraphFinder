﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphFinder.Entity
{
    class Graph
    {
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        public int Percent { get; set; }


        public Graph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();

            Percent = 70;
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        public bool IsNotEgdeExist(Edge searchedEdge)
        {
            foreach (Edge edge in Edges)
            {
                if (edge.Start.Center.Equals(searchedEdge.Start.Center) && 
                    edge.End.Center.Equals(searchedEdge.End.Center) ||
                    edge.Start.Center.Equals(searchedEdge.End.Center) && 
                    edge.End.Center.Equals(searchedEdge.Start.Center))
                {
                    return false;
                }
            }

            return true;
        }

        public void RefactorGraph()
        {
            RefactorNodes();
            RefactorEdges();
        }

        public void RefactorNodes()
        {
            Nodes = Nodes.OrderBy(n => n.Identifier).ToList();
        }

        public void RefactorEdges()
        {
            Edges = Edges.OrderByDescending(e => e.Weight).ToList();

            int maxWeight = Edges[0].Weight;
            int deletedIndex = 0;

            foreach(Edge edge in Edges)
            {
               if(edge.Weight >= Math.Round(maxWeight * Percent * 0.01))
               {
                   deletedIndex++;
               }
               else
               {
                   break;
               }
            }

            for(int i = deletedIndex; i >= 0; i--)
            {
                Edges.RemoveAt(i);
            }
        }

        public Node GetNodeByCenter(Point center)
        {
            return Nodes.Find(n => n.Center.Equals(center));
        }

        public Node GetFirstNode()
        {
            return Nodes.Find(n => n.Identifier.Equals("s"));
        }

        public int GetEdgeWeight(Node start, Node end)
        {
            int weight = -1;

            try
            {
                weight = Edges.Find(n => n.Start.Equals(start) && n.End.Equals(end)).Weight;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return weight;
        }
    }
}
