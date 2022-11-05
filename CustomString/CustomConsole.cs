using System;

namespace CustomString
{
    public static class CustomConsole
    {
        public static CustomString ReadLine()
        {
            var text = new char[8];
            var counter = 0;
            while (true)
            {
                var character = (char)Console.Read();
                if (character == 13)
                    // 13 is Enter key ASCII code
                    break;

                text[counter] = character;
                counter++;
                if (counter < text.Length) continue;
                // We can replace this lines with Array.resize()
                var copyText = text;
                text = new char[counter + 8];
                copyText.CopyTo(text, 0);
            }

            return new CustomString(text, counter);
        }

        public static void WriteLine(CustomString text)
        {
            Console.WriteLine(text);
        }
    }
}