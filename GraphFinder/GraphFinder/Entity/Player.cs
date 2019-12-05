using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFinder.Entity
{
    class Player
    {
        public int Score { get; private set; }
        public Node CurrentNode { get; private set; }

        public Player()
        {

        }

        public Player(int score, Node startNode)
        {
            Score = score;
            CurrentNode = startNode;
            CurrentNode.IsPlayerOnNode = true;
        }

        public void CurrentNodeSet(Node nextNode)
        {
            CurrentNode.IsPlayerOnNode = false;
            CurrentNode = nextNode;
            CurrentNode.IsPlayerOnNode = true;
        }

        public void ScoreSet(int minusScore)
        {
            Score -= minusScore;
        }
    }
}
