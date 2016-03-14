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

        Snake snake = new Snake();
        Food food = new Food();

        public void Print ()
        {
            Console.Clear();

            Console.WriteLine("\tScore: {0}, Size: {1}", score, snake.Size);
            Console.WriteLine( new string( '-', Size_W + 2 ) );
            for ( int i = 0; i < Size_H; i++ )
            {
                Console.Write( "|" );
                Console.Write( new string( ' ', Size_W ) );
                Console.WriteLine( "|" );
            }
            Console.WriteLine( new string( '-', Size_W + 2 ) );

            snake.DrawSnake();
            food.DrawFood();
        }

        public void Run ()
        {
            keyPressEvent += onKeyPress;
            foodEatEvent += onFoodEat;
            ConsoleKeyInfo cki = new ConsoleKeyInfo();

            try
            {
                do
                {
                    while ( Console.KeyAvailable == false )
                    {
                        Print();
                        snake.Move( foodEatEvent, food.Position );
                        //foodEatEvent.Invoke( snake );
                        Thread.Sleep( 150 );
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
