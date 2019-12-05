using GraphFinder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFinder.Model
{
    class PlayerActivity
    {
        public void MovePlayerToNode(Player player, Node nextNode, int minusScore)
        {
            player.CurrentNodeSet(nextNode);
            player.ScoreSet(minusScore);
        }
    }
}
