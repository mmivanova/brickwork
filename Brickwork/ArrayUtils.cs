using System;
using System.Collections.Generic;
using System.Linq;

namespace Brickwork
{
    static class ArrayUtils
    {
        private const char Separator = ' ';

        public static int[,] ReadArray()
        {
            Dimension dimension = ReadDimension();
            ValidateDimention(dimension);

            int[,] brickLayer = new int[dimension.Rows, dimension.Columns];

            for (int row = 0; row < dimension.Rows; row++)
            {
                int[] input = ReadLineAndConvertToIntArray();
                for (int column = 0; column < dimension.Columns; column++)
                {
                    brickLayer[row, column] = input[column];
                }
            }

            ValidateArray(brickLayer);
            return brickLayer;
        }

        public static void ValidateArray(int[,] array)
        {
            if (!IsValidArray(array))
            {
                throw new Exception("Invalid input. Each number should be presented exactly twice.");
            }
        }

        public static bool IsValidArray(int[,] array)
        {
            bool IsValidFlag = true;
            if (array.GetLength(0) <= 2 && array.GetLength(1) <= 2)
            {
                goto exit;
            }
            else if (array.GetLength(0) <= 2 || array.GetLength(1) <= 2)
            {
                IsValidFlag = IsValidArrayWithOneDimensionOfTwo(array);
                goto exit;
            }
            else
            {
                for (int r = 0; r < array.GetLength(0) - 2; r++)
                {
                    for (int c = 0; c < array.GetLength(1) - 2; c++)
                    {
                        bool containsThreeIdenticalVerticalIndexes = array[r, c] == array[r + 1, c] &&
                                                                      array[r + 1, c] == array[r + 2, c];
                        bool containsThreeIdenticalHorizontalIndexes = array[r, c] == array[r, c + 1] &&
                                                                       array[r, c + 1] == array[r, c + 2];
                        if (containsThreeIdenticalHorizontalIndexes || containsThreeIdenticalVerticalIndexes)
                        {
                            IsValidFlag = false;
                            goto exit;
                        }
                    }
                }
            }
        exit:
            return IsValidFlag;
        }

        private static bool IsValidArrayWithOneDimensionOfTwo(int[,] array)
        {
            bool IsValidFlag = true;
            Dictionary<int, int> brickValuesWithCount = new Dictionary<int, int>();
            for (int r = 0; r < array.GetLength(0); r++)
            {
                for (int c = 0; c < array.GetLength(1); c++)
                {
                    if (brickValuesWithCount.ContainsKey(array[r, c]))
                    {
                        brickValuesWithCount[array[r, c]]++;
                    }
                    else
                    {
                        brickValuesWithCount.Add(array[r, c], 1);
                    }
                }
            }
            foreach (var value in brickValuesWithCount)
            {
                if (value.Value >= 3)
                {
                    IsValidFlag = false;
                }
            }
            return IsValidFlag;
        }

        public static void ValidateDimention(Dimension dimension)
        {
            if (!dimension.IsValid())
            {
                throw new Exception("Invalid input: The numbers must be even and less than 100.");
            }
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
