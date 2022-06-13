using System;
using System.Collections.Generic;
using змея;

namespace змея
{
    public class Snake
    {
        private readonly ConsoleColor _headcolor;
        private readonly ConsoleColor _bodycolor;
        #region Одиночка
        private static Snake obj;
        public static Snake CreateSnake()
        {
            if (obj == null) obj=new Snake(10, 5, ConsoleColor.Yellow, ConsoleColor.Blue);
            return obj;
        }
        #endregion
        private Snake(int initialX,
                     int initialY,
                     ConsoleColor headColor,
                     ConsoleColor bodeColor,
                     int bodyLenght = 3)
        {
            _headcolor = headColor;
            _bodycolor = bodeColor;

            Head = new Pixel(initialX, initialY, _headcolor);

            for (int i = bodyLenght; i >= 0; i--)
            {
                Body.Enqueue(new Pixel(Head.Y - i - 1, initialY, _bodycolor));
            }
            Drow();
        }
        public Pixel Head { get; private set; }
        public Queue<Pixel> Body { get; } = new Queue<Pixel>();
        public void Move(Direction direction, bool eat = false)
        {
            Clear();

            Body.Enqueue(new Pixel(Head.Y, Head.X, _bodycolor));

            if (!eat)
                Body.Dequeue();

            Head = direction switch
            {
                Direction.Right => new Pixel(Head.Y + 1, Head.X, _headcolor),
                Direction.Left => new Pixel(Head.Y - 1, Head.X, _headcolor),
                Direction.Up => new Pixel(Head.Y, Head.X - 1, _headcolor),
                Direction.Down => new Pixel(Head.Y, Head.X + 1, _headcolor),
                _ => Head
            };
            Drow();
        }

        public void Drow()
        {
            Head.Drow();
            foreach (Pixel pixel in Body)
            {
                pixel.Drow();
            }
        }
        public void Clear()
        {
            Head.Clear();
            foreach (Pixel pixel in Body)
            {
                pixel.Clear();
            }
        }
    }
}
