using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeConsole
{
    class Snake
    {
        int _size = 3;
        public const ConsoleKey BUTTON_UP = ConsoleKey.UpArrow;
        public const ConsoleKey BUTTON_DOWN = ConsoleKey.DownArrow;
        public const ConsoleKey BUTTON_LEFT = ConsoleKey.LeftArrow;
        public const ConsoleKey BUTTON_RIGHT = ConsoleKey.RightArrow;

        List<SnakePart> _snake = new List<SnakePart>();

        List<Actions> actions;
        List<Actions> removeList = new List<Actions>();

        public int Size
        {
            get
            {
                return _size;
            }
        }

        public Snake ()
        {
            actions = new List<Actions>();
            for(int i = 0; i < _size; ++i )
            {
                _snake.Add( new SnakePart(new Position(_size - i + 1, 2), Direction.Right ));
            }
        }

        public void DrawSnake ()
        {
            foreach ( SnakePart part in _snake )
            {
                Console.SetCursorPosition( part.Position.X, part.Position.Y);
                if ( isHead( part ) )
                    Console.Write( "@" );
                else if ( isTail( part ) )
                    Console.Write( "%" );
                else
                    Console.Write( "#" );
            }
        }

        public SnakePart getHead ()
        {
            return _snake.First();
        }

        public SnakePart getTail ()
        {
            return _snake.Last();
        }

        public void Move ()
        {
            foreach( SnakePart node in _snake )
            {
                if(actions.Count != 0 )
                {
                    foreach ( Actions action in actions )
                    {
                        if ( node.Position.Equals( action.position ) )
                        {
                            if ( node.Direction != action.direction )
                                node.Direction = action.direction;

                            if ( isTail( node ) )
                                removeList.Add( action );
                        }
                    }
                }
           
                switch ( node.Direction )
                {
                    case Direction.Right: node.MoveRight();
                        break;
                    case Direction.Left: node.MoveLeft();
                        break;
                    case Direction.Up: node.MoveUp();
                        break;
                    case Direction.Down: node.MoveDown();
                        break;
                }
            }

            if ( removeList.Count != 0 )
            {
                foreach ( Actions remove in removeList )
                {
                    actions.Remove( remove );
                }
            }

            if ( _isColision() )
                throw new SnakeException("You lose!");
        }

        public void addAction( Position pos, Direction direction )
        {
            actions.Add( new Actions( pos, direction ) );
        }

        public bool isTail( SnakePart part )
        {
            return part.Equals( getTail() );
        }

        public bool isHead( SnakePart part )
        {
            return part.Equals( getHead() );
        }

        public void Grow ()
        {
            int X = 0, Y = 0;
            SnakePart tail = getTail();
            switch ( tail.Direction )
            {
                case Direction.Right:
                    X = tail.Position.X - 1;
                    Y = tail.Position.Y;
                    break;
                case Direction.Left:
                    X = tail.Position.X + 1;
                    Y = tail.Position.Y;
                    break;
                case Direction.Down:
                    X = tail.Position.X;
                    Y = tail.Position.Y - 1;
                    break;
                case Direction.Up:
                    X = tail.Position.X;
                    Y = tail.Position.Y + 1;
                    break;
            }

            _snake.Add( new SnakePart( new Position( X, Y ), tail.Direction ) );
            _size++;
        }

        bool _isColision ()
        {
            SnakePart head = getHead();
            for(int i = 1; i < _snake.Count; i++ )
            {
                if ( _snake[i].Position.Equals(head.Position) )
                    return true;
            }

            if ( head.Position.X == 0 || head.Position.X == Game.Size_W + 1 || head.Position.Y == 1 || head.Position.Y == Game.Size_H + 2 )
                return true;

            return false;
        }
    }
}
