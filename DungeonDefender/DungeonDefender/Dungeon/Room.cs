using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonDefender.Dungeon
{
    abstract class Room
    {
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Tuple<int, int> Enter { get; protected set; }
    }
}
