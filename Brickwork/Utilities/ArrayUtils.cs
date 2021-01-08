using Brickwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brickwork.Utilities
{
    static class ArrayUtils
    {
        private const char Separator = ' ';
        private const string Column = "|";
        private const char Dash = '-';
        private const string Space = " ";
        private const string DoubleSpace = "  ";

        public static BrickLayer ReadArray()
        {
            Dimension dimension = ReadDimension();
            Validator.ValidateDimention(dimension);

            int[,] layer = new int[dimension.Rows, dimension.Columns];

            for (int row = 0; row < dimension.Rows; row++)
            {
                int[] input = ReadLineAndConvertToIntArray();
                for (int column = 0; column < dimension.Columns; column++)
                {
                    layer[row, column] = input[column];
                }
            }
            BrickLayer brickLayer = new BrickLayer(layer);
            Validator.ValidateArray(brickLayer);

            return brickLayer;
        }

        public static Dimension ReadDimension()
        {
            return new Dimension(ReadLineAndConvertToIntArray());
        }

        private static int[] ReadLineAndConvertToIntArray()
        {
            return Console.ReadLine().Trim().Split(Separator).Select(int.Parse).ToArray();
        }

        //Method that prints multidimensional arrays on the console.
        //Will use it to print the second layer of the wall
        public static void PrintArray(BrickLayer brickLayer)
        {
            Dimension dimension = new Dimension(brickLayer);
            PrintHeader(dimension);
            for (int r = 0; r < dimension.Rows; r++)
            {
                Console.Write(Column);
                for (int c = 0; c < dimension.Columns; c++)
                {
                    PrintLine(brickLayer, dimension, new Coordinates(r, c));
                }
                Console.WriteLine();
                for (int col = 0; col < dimension.Columns; col++)
                {
                    PrintSeparationLine(brickLayer, dimension, new Coordinates(r, col));
                }
                if (!IsLastRow(r, dimension))
                {
                    Console.WriteLine();
                }
            }
            PrintFooter(dimension);
        }

        private static void PrintSeparationLine(BrickLayer brickLayer, Dimension dimension, Coordinates coordinates)
        {
            string brickSeparator = IsBetweenBricksVertically(brickLayer, coordinates) ? "-----" : "     ";
            if (!IsLastRow(coordinates.Row, dimension))
            {
                Console.Write(string.Format("{0}", brickSeparator));
            }
        }

        private static void PrintLine(BrickLayer brickLayer, Dimension dimension, Coordinates coordinates)
        {
            string padding = brickLayer.Get(coordinates) < 10 ? DoubleSpace : Space;
            string brickSeparator = IsBetweenBricksHorizontally(brickLayer, coordinates) ? Column : Space;
            string lastColumn = IsLastColumn(coordinates.Column, dimension) ? Column : Space;
            Console.Write(string.Format("{0}{1}{2}{3}", padding, brickLayer.Get(coordinates), brickSeparator, lastColumn));
        }

        private static bool IsBetweenBricksHorizontally(BrickLayer brickLayer, Coordinates coordinates)
        {
            if (IsLastColumn(coordinates.Column, new Dimension(brickLayer)))
            {
                return false;
            }
            return brickLayer.Get(coordinates) != brickLayer.Get(coordinates.Row, coordinates.Column + 1);
        }

        private static bool IsBetweenBricksVertically(BrickLayer brickLayer, Coordinates coordinates)
        {
            if (IsLastRow(coordinates.Row, new Dimension(brickLayer)))
            {
                return false;
            }
            return brickLayer.Get(coordinates) != brickLayer.Get(coordinates.Row + 1, coordinates.Column);
        }

        private static void PrintFooter(Dimension dimension)
        {
            Console.WriteLine(new string(Dash, CalculateFrameSize(dimension)));
        }

        private static void PrintHeader(Dimension dimension)
        {
            Console.WriteLine(new string(Dash, CalculateFrameSize(dimension)));
        }

        private static bool IsLastColumn(int column, Dimension dimension)
        {
            return column == dimension.Columns - 1;
        }

        private static bool IsLastRow(int row, Dimension dimension)
        {
            return row == dimension.Rows - 1;
        }

        private static int CalculateFrameSize(Dimension dimension)
        {
            return dimension.Columns * 5;
        }
    }
}
