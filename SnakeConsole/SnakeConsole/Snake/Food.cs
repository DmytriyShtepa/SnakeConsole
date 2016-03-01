using System;

namespace SnakeConsole
{
    class Food
    {
        Position _position;
        public Position Position
        {
            get
            {
                return _position;
            }
        }

        public Food ()
        {
            int X = new Random().Next( 1, Game.Size_W );
            int Y = new Random().Next( 2, Game.Size_H );

            _position = new Position(X, Y);
        }

        public void DrawFood ()
        {
            Console.SetCursorPosition( _position.X, _position.Y );
            Console.Write("*");
        }
    }
}
