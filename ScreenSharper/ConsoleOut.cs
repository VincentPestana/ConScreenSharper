using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ScreenSharper
{
    class ConsoleOut
    {
        private int _delay;

        public ConsoleOut(int standardDelay)
        {
            _delay = standardDelay;
        }

        public void PrintLine(string line)
        {
            Console.WriteLine(line);
            Thread.Sleep(_delay);
        }

        public void TypeText(string text)
        {
            foreach (var singleChar in text.ToCharArray())
            {
                Console.Write(singleChar);
                Thread.Sleep(_delay / 5);
            }
        }

        public void TypeBackwards(string text)
        {
            // TODO
        }

        public void TypeRollFromLeft(string text)
        {
            for (int i = text.Length - 1; i >= 0; i--)
            {
                Console.Write(text.Substring(i));
                Thread.Sleep(_delay / 5);
                if (i != 0)
                    Console.SetCursorPosition(0, Console.CursorTop);
            }
        }

        public void Wait(int sleepTime)
        {
            Thread.Sleep(sleepTime);
        }

        internal void NL()
        {
            Console.WriteLine();
        }
    }
}
