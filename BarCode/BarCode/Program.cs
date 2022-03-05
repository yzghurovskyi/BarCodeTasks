using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace BarCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var encoder = new BarCodeEncoder();
            var input = GetUserInput("Please, input digits for conversion:");

            while (string.IsNullOrWhiteSpace(input) || !input.ToCharArray().ToList().TrueForAll(char.IsDigit))
                input = GetUserInput("Please, input only digit characters for conversion:");

            var parsedDigits = input.Select(digit => int.Parse(digit.ToString())).ToList();

            var bmp = encoder.Encode(parsedDigits);

            var path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "barcode.png");
            bmp.Save(path);
            Console.WriteLine($"Barcode was saved to {path}");

            var image = Image.FromFile(path);

            var toRead = new Bitmap(image);
            var digits = encoder.Decode(bmp);
        }


        private static string GetUserInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine()?.Trim();
        }
    }
}
