using System;
using System.Threading;

namespace SnakeConsole
{
    class Game
    {
        public delegate void FoodEatHandler ( Snake snake );
        public delegate void ControlHandler ( ConsoleKeyInfo key, SnakePart node );

        public event FoodEatHandler foodEatEvent;
        public event ControlHandler keyPressEvent;        

        public const int Size_W = 70;
        public const int Size_H = 20;
        int score = 0;

        Snake snake;
        Food food;

        void _DrawBoard()
        {
            Console.WriteLine( "\tScore: {0}, Size: {1}", score, snake.Size );
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write( "\u2554" );
            Console.Write( new string( '\u2550', Size_W ) );
            Console.WriteLine( "\u2557" );

            for ( int i = 0; i < Size_H; i++ )
            {
                Console.Write( "\u2551" );
                Console.Write( new string( ' ', Size_W ) );
                Console.WriteLine( "\u2551" );
            }
            Console.Write( "\u255A" );
            Console.Write( new string( '\u2550', Size_W ) );
            Console.Write( "\u255D" );
        }

        void _UpdateScore ()
        {
            Console.SetCursorPosition( 0, 0 );
            Console.Write( "\tScore: {0}, Size: {1}", score, snake.Size );
        }

        public void Run ()
        {
            keyPressEvent += onKeyPress;
            foodEatEvent += onFoodEat;
            ConsoleKeyInfo cki = new ConsoleKeyInfo();

            snake = new Snake( foodEatEvent );
            food = new Food();

            Console.WindowHeight = Size_H + 5;
            Console.WindowWidth = Size_W + 2;
            Console.CursorVisible = false;
            Console.Title = "Snake ^_^";

            _DrawBoard();
            try
            {
                do
                {
                    while ( Console.KeyAvailable == false )
                    {
                        food.DrawFood();
                        snake.Move( food.Position );
                        Thread.Sleep( 100 );
                    }
                    cki = Console.ReadKey( true );
                    keyPressEvent.Invoke( cki, snake.getHead() );

                } while ( true );
            }
            catch ( SnakeException exception )
            {
                Console.SetCursorPosition( 0, Size_H + 3 );
                Console.WriteLine(exception.Message);
            }            
        }

        private void onFoodEat ( Snake snake )
        {
            score += 10;
            food = new Food();
            snake.Grow();
            _UpdateScore();
        }

        private void onKeyPress ( ConsoleKeyInfo key, SnakePart part )
        {
            switch( key.Key )
            {
                case Snake.BUTTON_LEFT:
                    if(part.Direction != Direction.Left && part.Direction != Direction.Right)
                        snake.addAction( new Position(part.Position.X, part.Position.Y), Direction.Left );
                    break;
                case Snake.BUTTON_RIGHT:
                    if ( part.Direction != Direction.Right && part.Direction != Direction.Left)
                        snake.addAction( new Position( part.Position.X, part.Position.Y ), Direction.Right );
                    break;
                case Snake.BUTTON_DOWN:
                    if ( part.Direction != Direction.Down && part.Direction != Direction.Up )
                        snake.addAction( new Position( part.Position.X, part.Position.Y ), Direction.Down );
                    break;
                case Snake.BUTTON_UP:
                    if ( part.Direction != Direction.Up && part.Direction != Direction.Down )
                        snake.addAction( new Position( part.Position.X, part.Position.Y ), Direction.Up );
                    break;
                case ConsoleKey.Escape: 
                    Console.SetCursorPosition( Size_H + 1, 0 );
                    throw new SnakeException("You exit the game!");
            }
        }
    }
}
