using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BarCode
{
    public class BarCodeEncoder
    {
        private const int ModuleLength = 8;
        private const int WideModuleRelation = 3;
        private const int ModuleRelation = 2;

        private const int WideBarsCount = 2;
        private const int NarrowBarsCount = 3;
        private const int BarHeight = 200;

        private int _currentPosition;

        public Bitmap Encode(List<int> digits)
        {
            var controlDigit = ControlDigit(digits);
            Console.WriteLine($"Control digit: {controlDigit}");

            var bmp = new Bitmap(CountLength(digits.Count + 1), BarHeight);
            using var gr = Graphics.FromImage(bmp);

            _currentPosition = 0;
            PrintStartStop(gr);
            PrintSeparation(gr);

            foreach (var digit in digits)
            {
                PrintDigit(gr, digit);
                PrintSeparation(gr);
            }

            PrintDigit(gr, controlDigit);
            PrintSeparation(gr);
            PrintStartStop(gr);

            return bmp;
        }

        public (List<int> digits, int controlDigit) Decode(Bitmap bitmap)
        {
            var colors = GetBitmapColorMatrix(bitmap);
            var digits = new List<int>();

            var colorsDividedOnModule = colors.Where((x, i) => i % ModuleLength == 0).ToList();

            var skippedStartStop = colorsDividedOnModule.Skip(WideModuleRelation + 5).SkipLast(WideModuleRelation + 5).ToList();
            var bytes = new bool[5];
            var bytesPointer = 0;
            for (var i = 0; i < skippedStartStop.Count; i++)
            {
                var firstColor = skippedStartStop[i];
                if (i + 1 == skippedStartStop.Count)
                    break;

                if (skippedStartStop[i + 1].Equals(firstColor))
                {
                    bytes[bytesPointer] = true;
                    i++;
                }
                else
                    bytes[bytesPointer] = false;

                bytesPointer++;

                if (bytesPointer == 5)
                {
                    bytesPointer = 0;
                    digits.Add(GetDigit(bytes));
                    i++;
                }
            }

            var controlDigit = digits.Last();

            digits.RemoveAt(digits.LastIndexOf(controlDigit));

            if (ControlDigit(digits) != controlDigit)
                throw new ArgumentException("Control digit is wrong, please try again");

            return (digits, controlDigit);
        }

        private void PrintStartStop(Graphics graphics)
        {
            var width = ModuleLength * WideModuleRelation;
            PrintBar(graphics, Brushes.Black, width);

            width = ModuleLength;
            PrintBar(graphics, Brushes.White, width);
            PrintBar(graphics, Brushes.Black, width);
            PrintBar(graphics, Brushes.White, width);
            PrintBar(graphics, Brushes.Black, width);
        }

        private void PrintDigit(Graphics graphics, int digit)
        {
            var bytes = GetBytes(digit);

            for (var i = 0; i < bytes.Length; i++)
            {
                var brush = Brushes.Black;
                if (i % 2 == 1)
                    brush = Brushes.White;

                var width = ModuleLength;
                if (bytes[i])
                    width *= ModuleRelation;

                PrintBar(graphics, brush, width);
            }
        }

        private void PrintSeparation(Graphics graphics) => PrintBar(graphics, Brushes.White, ModuleLength);

        private int CountLength(int digitsCount)
        {
            const int digitLength = WideBarsCount * ModuleLength * ModuleRelation + NarrowBarsCount * ModuleLength;
            const int startStopLength = (4 * ModuleLength + ModuleLength * WideModuleRelation) * 2;
            var separationLength = (digitsCount + 1) * ModuleLength;

            return digitLength * digitsCount + startStopLength + separationLength;
        }

        private bool[] GetBytes(int digit) => digit switch
        {
            0 => new[] { false, false, true, true, false },
            1 => new[] { true, false, false, false, true },
            2 => new[] { false, true, false, false, true },
            3 => new[] { true, true, false, false, false },
            4 => new[] { false, false, true, false, true },
            5 => new[] { true, false, true, false, false },
            6 => new[] { false, true, true, false, false },
            7 => new[] { false, false, false, true, true },
            8 => new[] { true, false, false, true, false },
            9 => new[] { false, true, false, true, false },
            _ => throw new ArgumentException("Can not print number")
        };

        private int GetDigit(IEnumerable<bool> bytes)
        {
            if (bytes.SequenceEqual(new List<bool> { false, false, true, true, false }))
                return 0;
            if (bytes.SequenceEqual(new List<bool> { true, false, false, false, true }))
                return 1;
            if (bytes.SequenceEqual(new List<bool> { false, true, false, false, true }))
                return 2;
            if (bytes.SequenceEqual(new List<bool> { true, true, false, false, false }))
                return 3;
            if (bytes.SequenceEqual(new List<bool> { false, false, true, false, true }))
                return 4;
            if (bytes.SequenceEqual(new List<bool> { true, false, true, false, false }))
                return 5;
            if (bytes.SequenceEqual(new List<bool> { false, true, true, false, false }))
                return 6;
            if (bytes.SequenceEqual(new List<bool> { false, false, false, true, true }))
                return 7;
            if (bytes.SequenceEqual(new List<bool> { true, false, false, true, false }))
                return 8;
            if (bytes.SequenceEqual(new List<bool> { false, true, false, true, false }))
                return 9;

            throw new ArgumentException("Can not define digit");
        }

        private int ControlDigit(List<int> digits)
        {
            var copy = digits.ToList();
            var counter = 0;
            var sum = 0;
            copy.Reverse();
            foreach (var digit in copy)
            {
                if (counter % 2 == 0)
                    sum += digit * 3;
                else
                    sum += digit;

                counter++;
            }

            return sum % 10 == 0 ? 0 : Math.Abs(sum % 10 - 10);
        }

        private void PrintBar(Graphics graphics, Brush brush, int width)
        {
            graphics.FillRectangle(brush, new Rectangle(_currentPosition, 0, width, BarHeight));
            _currentPosition += width;
        }

        private Color[] GetBitmapColorMatrix(Bitmap bitmap)
        {
            var colors = new Color[bitmap.Width];
            for (var i = 0; i < bitmap.Width; i++)
            {
                var pixel = bitmap.GetPixel(i, 0);
                if (pixel.Name == "ff000000")
                    colors[i] = Color.Black;
                else
                    colors[i] = Color.White;
            }

            return colors;
        }
    }
}
