namespace SnakeConsole
{
    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position ( int x, int y )
        {
            X = x;
            Y = y;
        }

        public bool Equals ( Position _position )
        {
            if ( this.X == _position.X && this.Y == _position.Y )
                return true;
            return false;
        }
    }

    class Actions
    {
        public Position position { get; private set; }
        public Direction direction { get; private set; }

        public Actions (Position _position, Direction _direction)
        {
            position = _position;
            direction = _direction;
        }

        public bool Equals ( Actions action )
        {
            if ( this.position.Equals(action.position) && this.direction == action.direction )
                return true;
            return false;
        }
    }
}
