using System;
namespace змея
{
    public readonly struct Pixel
    {
        private const char PixelChar = '█';

        public Pixel(int y, int x, ConsoleColor color, int PixelSize = 3) : this()
        {
            X = x;
            Y = y;

            Color = color;
            pixelSize = PixelSize;
        }
        public int X { get; }
        public int Y { get; }
        public ConsoleColor Color { get; }
        public int pixelSize { get; }

        public void Drow()
        {
            Console.ForegroundColor = Color;
            for (int y = 0; y < pixelSize; y++)
            {
                for (int x = 0; x < pixelSize; x++)

                {
                    Console.SetCursorPosition(Y * pixelSize + y, X * pixelSize + x);
                    Console.Write(PixelChar);
                }
            }
        }
        public void Clear()
        {
            for (int y = 0; y < pixelSize; y++)
            {
                for (int x = 0; x < pixelSize; x++)
                {
                    Console.SetCursorPosition(Y * pixelSize + y, X * pixelSize + x);
                    Console.Write(" ");
                    //System.Threading.Thread.Sleep(500);
                }
            }
        }
    }
}
