namespace SnakeConsole
{
    enum Direction
    {
        Right,
        Left,
        Up,
        Down        
    }

    class SnakePart
    {
        Position _position;
        Direction _direction;

        public Position Position
        {
            get
            {
                return _position;
            }
        }

        public Direction Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
            }
        }

        public SnakePart (Position position, Direction direction)
        {
            _position = position;
            _direction = direction;
        }

        public void MoveRight ()
        {
             _position.X++;                
        }

        public void MoveLeft ()
        {
             _position.X--;
        }

        public void MoveDown ()
        {
             _position.Y++;
        }

        public void MoveUp ()
        {
            _position.Y--;         
        }
    }
}
