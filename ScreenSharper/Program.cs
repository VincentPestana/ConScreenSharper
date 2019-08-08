using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using TrueColorConsole;

namespace ScreenSharper
{
    class Program
    {
        private static ConsoleOut _cout;

        private static CursorPosition _curPos;

        private static string _characters;

        static void Main(string[] args)
        {
            // VT Console check
            if (!VTConsole.IsSupported)
            {
                Console.WriteLine("VT Console not supported");
                Console.ReadLine();
            }

            _cout = new ConsoleOut(1000);
            _curPos = new CursorPosition();

            _characters = "<>";

            //TestingOnly();

            StartingScene();


            /////////
            ///
            RandomColorsForever();

        }

        private static void RandomColorsForever()
        {
            VTConsole.CursorSetBlinking(false);
            VTConsole.CursorSetVisibility(true);
            VTConsole.SetColorBackground(Color.Black);
            VTConsole.SetColorForeground(Color.White);

            Console.Clear();
            Console.SetCursorPosition(5, 8);
            VTConsole.Write("Hi there", Color.FromArgb(0, 255, 0));
            Console.SetCursorPosition(0, 0);

            int r = 0;
            int g = 0;
            int b = 0;

            //for (var column = 0; column < Console.BufferWidth; column++)
            while (!Console.KeyAvailable)
            {
                // Set column to random and row to 0
                var randomColumn = new Random().Next(0, Console.BufferWidth);
                Console.SetCursorPosition(randomColumn, 0);

                for (var row = 0; row < Console.WindowHeight - 1; row++)
                {
                    // Always move the cursor down
                    VTConsole.CursorMoveDown();

                    var percentageChance = new Random().Next(100);

                    //if (percentageChance == 50)
                    //{
                    //    VTConsole.CursorPosition(new Random().Next(1, Console.WindowHeight), new Random().Next(1, Console.WindowWidth - 1));
                    //    var datetimeString = DateTime.Now.ToString("hh:mm:ss");
                    //    VTConsole.Write(datetimeString, Color.Black);
                    //    VTConsole.CursorMoveLeft(datetimeString.Length);
                    //}

                    // Randomly print a new block colour
                    if (percentageChance < 40)
                    {
                        var random = new Random();
                        r = random.Next(255);
                        g = random.Next(255);
                        b = random.Next(255);

                        // Values will only ever be slightly over 255
                        if (r > 255)
                            r -= 255;
                        if (g > 255)
                            g -= 255;
                        if (b > 255)
                            b -= 255;

                        // Vibrant colours only
                        //bool vibrantColours = true;
                        //if (vibrantColours)
                        //{
                        //    // Make the lowest value = 0
                        //    if (r < g && r < b)
                        //        r = 0;
                        //    else if (g < r && g < b)
                        //        g = 0;
                        //    else if (b < g && b < r)
                        //        b = 0;
                        //}

                        //Debug.WriteLine($"r:{r} g:{g} b:{b}");

                        // vertical strip dark at the top
                        VTConsole.Write(" ", Color.Black, Color.FromArgb(Convert.ToInt32(r / 1.5), Convert.ToInt32(g / 1.5), Convert.ToInt32(b / 1.5)));
                        VTConsole.CursorMoveDown();
                        VTConsole.CursorMoveLeft();
                        VTConsole.Write(" ", Color.Black, Color.FromArgb(Convert.ToInt32(r / 1.2), Convert.ToInt32(g / 1.2), Convert.ToInt32(b / 1.2)));
                        VTConsole.CursorMoveDown();
                        VTConsole.CursorMoveLeft();
                        VTConsole.Write(" ", Color.Black, Color.FromArgb(r, g, b));


                        VTConsole.CursorMoveLeft();
                    }
                    else
                    {
                        VTConsole.Write(" ", Color.Black, Color.Black);
                        VTConsole.CursorMoveDown();
                        VTConsole.CursorMoveLeft();
                        VTConsole.Write(" ", Color.Black, Color.Black);
                        VTConsole.CursorMoveDown();
                        VTConsole.CursorMoveLeft();
                        VTConsole.Write(" ", Color.Black, Color.Black);


                        VTConsole.CursorMoveLeft();
                    }

                    //Thread.Sleep(5);
                }
            }

        }

        private static string GetRandomCharacter()
        {
            return _characters[new Random().Next(0, _characters.Length)].ToString();
        }

        private static void TestingOnly()
        {
            _cout.TypeRollFromLeft("What does this code even do?");


            Console.ReadLine();
        }

        private static void StartingScene()
        {
            // T- Start removing text randomly from the screen (set cursor, print blank)
            // T- 
            SaveCursorPosition();
            Console.SetCursorPosition(6, 4);
            _cout.PrintLine(" I, the Imposter ");
        }

        private static void SaveCursorPosition()
        {
            _curPos.Left = Console.CursorLeft;
            _curPos.Top = Console.CursorTop;
        }

        private static void RevertCursorPostion()
        {
            Console.CursorLeft = _curPos.Left;
            Console.CursorTop = _curPos.Top;
        }
    }
}
