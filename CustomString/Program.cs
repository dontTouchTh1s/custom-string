using System;

namespace CustomString
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter a text ...");
            var str = CustomConsole.ReadLine();
            Console.WriteLine("length: " + str.Lenght);
            str.ToUpper();
            Console.WriteLine("UpperCase: " + str);
            str.ToLower();
            Console.WriteLine("LoweCase: " + str);
            str.Replace('a', 'b');
            Console.WriteLine("Replaced char 'a' with char 'b': " + str);
            Console.WriteLine("Index of found char 's': " + str.Find('s'));
            CustomString newString = new[] { ' ', ',', 'r', 'e', 'z', 'a' };
            var addedString = str + newString;
            Console.WriteLine("Added ' , reza' to string: " + addedString);
            try
            {
                Console.WriteLine("Substring at index 4: " + addedString.Substring(4));
                Console.WriteLine("Substring at index 4 with 3 length: " + addedString.Substring(4, 3));
            }
            catch
            {
                Console.WriteLine("This error throws because length " +
                                  "of your input string is lower than " +
                                  "substring method parameter, you can change it programmatically," +
                                  "or insert a longer string.");
            }

            CustomString splitStr = new[] { 'r', 'e', 'z', 'a', ',', 'a', 'l', 'i', ',', 'j', 'a', 'v', 'a', 'd' };
            var newSplitStr = splitStr.Split(',');
            Console.WriteLine("Split string of " + splitStr + " by ',' character: ");
            foreach (var splitStrVar in newSplitStr)
                Console.WriteLine(splitStrVar);
            // Copy 'ali' to input string at index 4
            try
            {
                newSplitStr[1].CopyTo(0, ref str, 4, 3);
                Console.WriteLine("Copy 'ali' to input string at index 4: " + str);
            }
            catch
            {
                Console.WriteLine("This error throws because length " +
                                  "of your input string is lower than " +
                                  "CopyTo method destinationIndex parameter, you can change it programmatically," +
                                  "or insert a longer string.");
            }

            Console.WriteLine("Compare 'ali' to input string: " + str.CompareTo(
                new[] { 'a', 'l', 'i' }));
            Console.WriteLine("Compare input string to it self: " + str.CompareTo(
                str));
        }
    }
}