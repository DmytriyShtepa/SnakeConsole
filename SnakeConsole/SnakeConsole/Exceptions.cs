using System;

namespace SnakeConsole
{
    class SnakeException : Exception
    {
        public SnakeException ()
        {}

        public SnakeException (string message) : base(message)
        {}
    }
}
