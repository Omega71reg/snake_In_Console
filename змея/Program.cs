using System;
using static System.Console;
using System.Threading;
using System.Diagnostics;
using System.Linq;

namespace змея
{
    class Program
    {
        private const int Ширина = 30;
        private const int Длинна = 20;

        private const int FrameMs = 200;

        private static readonly Random rand = new Random();


        private const int РазмерконсолиШирина = Ширина * 3;
        private const int РазмерконсолиДлинна = Длинна * 3;
        private const ConsoleColor BorderColor = ConsoleColor.Red;
        private const ConsoleColor HeadColor = ConsoleColor.Yellow;
        private const ConsoleColor BodyColor = ConsoleColor.Cyan;
        private const ConsoleColor FoodColor = ConsoleColor.Green;
        static void Main(string[] args)
        {
            поле();

            while(true)
            {
                StartGame();
                Thread.Sleep(1000);
                _ = Console.ReadKey();
            }
        }

        static void StartGame()
        {
            Clear();

            DrowBorder();

            Direction currentMovement = Direction.Right;

            var snake = new Snake(10, 5, HeadColor, BodyColor);

            Pixel food = GenFood(snake);
            food.Drow();

            Stopwatch sw = new Stopwatch();

            while (true)
            {
                sw.Restart();
                Direction oldMovement = currentMovement;
                while (sw.ElapsedMilliseconds <= FrameMs)
                {
                    if (currentMovement == oldMovement)
                    {
                        currentMovement = ReadMovement(currentMovement);
                    }
                }
                snake.Move(currentMovement);

                if (snake.Head.X == Ширина - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == Длинна - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                    break;

                //System.Threading.Thread.Sleep(200);
            }
            snake.Clear();

            SetCursorPosition(РазмерконсолиШирина / 3, РазмерконсолиДлинна / 2);
            WriteLine("Game Over");

        }

        static Pixel GenFood(Snake snake)
        {
            Pixel food;
            do
            {
                food = new Pixel(rand.Next(1, Ширина - 2), rand.Next(1, Длинна - 2), FoodColor);
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y
                     || snake.Body.Any(b => b.X == food.X && b.Y == food.Y));
            return food;
        }

        static Direction ReadMovement (Direction currentDirection)
        {
            if (!KeyAvailable)
                return currentDirection;

            ConsoleKey key = ReadKey(true).Key;

            currentDirection = key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                _ => currentDirection
            };
            return currentDirection;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
        static void поле()
        {
            Console.SetBufferSize(РазмерконсолиШирина, РазмерконсолиДлинна);
            Console.SetWindowSize(РазмерконсолиШирина, РазмерконсолиДлинна);
           
            Console.CursorVisible = false;

        }

        static void DrowBorder()
        {
            for (int i = 0; i < Ширина; i++)
            {
                new Pixel(i, 0, BorderColor).Drow();
                new Pixel(i, Длинна - 1, BorderColor).Drow();
            }
            for (int i = 0; i < Длинна; i++)
            {
                new Pixel(0, i, BorderColor).Drow();
                new Pixel(Ширина-1,i, BorderColor).Drow();
            }
        }
    }
}
