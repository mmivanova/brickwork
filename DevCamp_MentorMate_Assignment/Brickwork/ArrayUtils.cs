using System;
using System.Linq;

namespace Brickwork
{
    static class ArrayUtils
    {
        private const char Separator = ' ';

        public static int[,] ReadArray()
        {
            Dimension dimension = ValidateDimention();

            int[,] brickLayer = new int[dimension.Rows, dimension.Columns];

            for (int row = 0; row < dimension.Rows; row++)
            {
                int[] input = ReadLineAndConvertToIntArray();
                for (int column = 0; column < dimension.Columns; column++)
                {
                    brickLayer[row, column] = input[column];
                }
            }
            if (dimension.Rows > 2 && dimension.Columns>2)
            {
                return ValidateArray(brickLayer);
            }
            return brickLayer;
        }

        public static int[,] ValidateArray(int[,] array)
        {
            if (IsValidArray(array))
            {
                return array;
            }
            else
            {
                do
                {
                    array = ReadArray();
                } while (!IsValidArray(array));
                return array;
            }
        }

        public static bool IsValidArray(int[,] array)
        {
            bool IsValidFlag = true;
            for (int r = 0; r < array.GetLength(0) - 2; r++)
            {
                int col = 0;
                if (array[r, col] == array[r + 1, col] 
                    && array[r + 1, col] == array[r + 2, col] 
                    || IsValidFlag == false)
                { 
                    IsValidFlag = false;
                    break;
                }
                for (int c = 0; c < array.GetLength(1) - 2; c++)
                {
                    if (array[r, c] == array[r, c + 1] && array[r, c + 1] == array[r, c + 2])
                    {
                        IsValidFlag = false;
                        break;
                    }
                }
            }
            return IsValidFlag;
        }

        public static Dimension ValidateDimention()
        {
            Dimension dimension;
            do
            {
                dimension = ReadDimension();
            } while (!dimension.IsValidNumber(dimension.Rows) || !dimension.IsValidNumber(dimension.Columns));
            return dimension;
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
        //Will use it the print the second layer of the wall
        public static void PrintArray(int[,] input)
        {
            int rows = input.GetLength(0);
            int columns = input.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(string.Format("| {0} ", input[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
        }

    }
}
