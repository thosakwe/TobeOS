using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS
{
    public class ConsoleStdin : Stdin
    {
        private bool open = true, has = false;
        int queue = 0;

        public override bool CanRead
        {
            get {
                if (!open) return false;
                if (has) return true;
                has = true;
                queue = ReadInternal();
                return has && open;
            }
        }

        public override void Dispose()
        {
            open = false;
        }

        public override int Read()
        {
            if (has)
            {
                has = false;
                return queue;
            }
            else
            {
                return ReadInternal();
            }
        }

        private int ReadInternal()
        {
            if (!open) return 0;

            var key = Console.ReadKey();
            bool ctrlZ = key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.Z;

            if (ctrlZ || key.KeyChar == 26)
            {
                open = has = false;
                return 0;
            }
            else if (key.KeyChar == -1)
            {
                var k = Console.ReadKey();

                if (k.Key == ConsoleKey.Z)
                {
                    open = false;
                    return 0;
                }
                else
                {
                    has = true;
                    return queue = k.KeyChar;
                }
            }
            else
            {
                has = true;
                return queue = key.KeyChar;
            }
        }

        public override bool ReadInto(byte[] b, int length)
        {
            has = false;

            for (int i = 0; i < length; i++)
            {
                b[i] = (byte)Console.Read();
            }

            return true;
        }

        public override ConsoleKeyInfo ReadKey()
        {
            has = false;
            return Console.ReadKey();
        }

        public override string ReadLine()
        {
            has = false;
            return Console.ReadLine();
        }
    }
}
