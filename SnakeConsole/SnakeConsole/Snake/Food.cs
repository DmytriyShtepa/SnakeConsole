using System;

namespace SnakeConsole
{
    class Food
    {
        public Position position { get; private set; }

        public Food ()
        {
            int X = new Random().Next( 1, Game.Size_W );
            int Y = new Random().Next( 2, Game.Size_H );

            position = new Position(X, Y);
        }

        public void DrawFood ()
        {
            Console.SetCursorPosition( position.X, position.Y );
            Console.Write("*");
        }
    }
}
