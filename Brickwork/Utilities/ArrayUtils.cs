using Brickwork.Data;
using Brickwork.Validators;
using System;
using System.Linq;

namespace Brickwork.Utilities
{
    // Utility class containing helper functions 
    // regarding arrays
    static class ArrayUtils
    {
        
        private const char Separator = ' ';
        private const string Column = "|";
        private const char Dash = '-';
        private const string Space = " ";
        private const string DoubleSpace = "  ";
        private const string FiveDashes = "-----";
        private const string FiveSpaces = "     ";

        // Reads user input, validates it and
        // converts it into BrickLayer
        public static BrickLayer ReadArray()
        {
            Dimension dimension = ReadDimension();
            Validator.ValidateDimension(dimension);

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
            Validator.ValidateArray(brickLayer, dimension);

            return brickLayer;
        }

        // returns new dimension with values fro the user input
        public static Dimension ReadDimension()
        {
            return new Dimension(ReadLineAndConvertToIntArray());
        }

        // converts the user input into an array of integers
        private static int[] ReadLineAndConvertToIntArray()
        {
            return Console.ReadLine().Trim().Split(Separator).Select(int.Parse).ToArray();
        }

        // Method that prints multidimensional arrays on the console.
        public static void PrintArray(BrickLayer brickLayer)
        {
            Dimension dimension = new Dimension(brickLayer);
            PrintHeader(dimension);
            for (int r = 0; r < dimension.Rows; r++)
            {
                Console.Write(Column);
                for (int c = 0; c < dimension.Columns; c++)
                {
                    PrintRow(brickLayer, dimension, new Coordinates(r, c));
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

        // Given the coordinates, it checks if the separation line
        // between the bricks passes through one or two bricks 
        // and it prints different characters based on this condition
        private static void PrintSeparationLine(BrickLayer brickLayer, Dimension dimension, Coordinates coordinates)
        {
            string brickSeparator = IsBetweenOneVerticalBrick(brickLayer, coordinates) ? FiveDashes : FiveSpaces;
            if (!IsLastRow(coordinates.Row, dimension))
            {
                Console.Write(string.Format("{0}", brickSeparator));
            }
        }

        // Prints a single row of the brick layer
        private static void PrintRow(BrickLayer brickLayer, Dimension dimension, Coordinates coordinates)
        {
            string padding = brickLayer.Get(coordinates) < 10 ? DoubleSpace : Space;
            string brickSeparator = IsBetweenTwoHorizontalBricks(brickLayer, coordinates) ? Column : Space;
            string lastColumn = IsLastColumn(coordinates.Column, dimension) ? Column : Space;
            Console.Write(string.Format("{0}{1}{2}{3}", padding, brickLayer.Get(coordinates), brickSeparator, lastColumn));
        }

        // Returns true if the separation line passes through 
        // two horizontally placed brick
        private static bool IsBetweenTwoHorizontalBricks(BrickLayer brickLayer, Coordinates coordinates)
        {
            if (IsLastColumn(coordinates.Column, new Dimension(brickLayer)))
            {
                return false;
            }
            return brickLayer.Get(coordinates) != brickLayer.Get(coordinates.Row, coordinates.Column + 1);
        }

        // Returns true if the separation line passes through 
        // a vertically placed brick
        private static bool IsBetweenOneVerticalBrick(BrickLayer brickLayer, Coordinates coordinates)
        {
            if (IsLastRow(coordinates.Row, new Dimension(brickLayer)))
            {
                return false;
            }
            return brickLayer.Get(coordinates) != brickLayer.Get(coordinates.Row + 1, coordinates.Column);
        }

        // Prints the first line of the brick layer
        private static void PrintHeader(Dimension dimension)
        {
            Console.WriteLine(new string(Dash, CalculateFrameSize(dimension)));
        }

        // Prints the bottom line of the brick layer
        private static void PrintFooter(Dimension dimension)
        {
            Console.WriteLine(new string(Dash, CalculateFrameSize(dimension)));
        }
        
        // Returns true if the column is the last one
        private static bool IsLastColumn(int column, Dimension dimension)
        {
            return column == dimension.Columns - 1;
        }

        // Returns true if the row is the last one
        private static bool IsLastRow(int row, Dimension dimension)
        {
            return row == dimension.Rows - 1;
        }

        // Calculates how long should the top and bottom
        // lines of the array be
        private static int CalculateFrameSize(Dimension dimension)
        {
            return dimension.Columns * 5;
        }
    }
}
