using System;
using DungeonDefender.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonDefender.Units
{
    enum ItemType
    {
        Sword,
        Axe,
        Bow
    }

    enum Ability
    {
        Punch,
        Heal,
        Defense
    }

    abstract class Unit
    {
        protected Position _position;

        public abstract bool isMoveable ();
        public abstract void Draw ();
        public abstract void Interaction ();
    }

    class Monster : Unit
    {
        public override bool isMoveable (){ return true; }
        public override void Draw ()
        {
            Console.SetCursorPosition( _position.X, _position.Y );
            Console.Write("X");
        }

        public override void Interaction ()
        {}
    }
}
