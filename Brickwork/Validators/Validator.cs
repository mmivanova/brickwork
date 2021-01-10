using Brickwork.Data;
using System;
using System.Collections.Generic;

namespace Brickwork.Validators
{
    // Class that validates the user input
    // given specific conditions
    class Validator
    {
        // Throws exceptions if the user input 
        // for brick layer and dimension is invalid
        public static void ValidateArray(BrickLayer brickLayer, Dimension inputDimension)
        {
            if (!IsValidArray(brickLayer))
            {
                throw new Exception("Invalid input. Each number should be presented exactly twice.");
            }
            if (!UserInputMatchesActualSize(brickLayer, inputDimension))
            {
                throw new Exception("Invalid input. Incorrect number of rows or columns.");
            }
        }

        private static bool UserInputMatchesActualSize(BrickLayer brickLayer, Dimension inputDimension)
        {
            return brickLayer.Dimension.Rows == inputDimension.Rows &&
                   brickLayer.Dimension.Columns == inputDimension.Columns;
        }

        // A valid array will be one in which
        // no brick takes more than two places in the layer
        private static bool IsValidArray(BrickLayer brickLayer)
        {
            if (ArrayHasSizeTwoByTwo(brickLayer))
            {
                return true;
            }
            else if (ArrayHasOneSideEqualToTwo(brickLayer))
            {
                return IsValidArrayWithOneDimensionOfTwo(brickLayer);
            }
            else
            {
                return IsValidArrayWithDimensionsMoreThanTwo(brickLayer);
            }
        }

        private static bool ArrayHasOneSideEqualToTwo(BrickLayer brickLayer)
        {
            return brickLayer.GetRows() <= 2 || brickLayer.GetColumns() <= 2;
        }

        private static bool ArrayHasSizeTwoByTwo(BrickLayer brickLayer)
        {
            return brickLayer.GetRows() <= 2 && brickLayer.GetColumns() <= 2;
        }

        // If one of the dimensions equals 2,
        // the method checks only the other one 
        // if there are bricks taking more than two places
        private static bool IsValidArrayWithOneDimensionOfTwo(BrickLayer brickLayer)
        {
            Dictionary<int, int> brickValuesWithCount = new Dictionary<int, int>();
            for (int r = 0; r < brickLayer.GetRows(); r++)
            {
                for (int c = 0; c < brickLayer.GetColumns(); c++)
                {
                    Coordinates coordinates = new Coordinates(r, c);
                    if (brickValuesWithCount.ContainsKey(brickLayer.Get(coordinates)))
                    {
                        brickValuesWithCount[brickLayer.Get(coordinates)]++;
                        if (brickValuesWithCount[brickLayer.Get(coordinates)] == 3)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        brickValuesWithCount.Add(brickLayer.Get(coordinates), 1);
                    }
                }
            }
            return true;
        }

        // Goes through every row and column and
        // checks for bricks taking more than two places
        private static bool IsValidArrayWithDimensionsMoreThanTwo(BrickLayer brickLayer)
        {
            for (int r = 0; r < brickLayer.GetRows() - 2; r++)
            {
                for (int c = 0; c < brickLayer.GetColumns() - 2; c++)
                {
                    Coordinates coordinates = new Coordinates(r, c);
                    bool containsThreeIdenticalVerticalValues = brickLayer.Get(coordinates) == brickLayer.Get(r + 1, c) &&
                                                                  brickLayer.Get(r + 1, c) == brickLayer.Get(r + 2, c);
                    bool containsThreeIdenticalHorizontalValues = brickLayer.Get(r, c) == brickLayer.Get(r, c + 1) &&
                                                                   brickLayer.Get(r, c + 1) == brickLayer.Get(r, c + 2);
                    if (containsThreeIdenticalHorizontalValues || containsThreeIdenticalVerticalValues)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // If the dimension is invalid,
        // an exception is thrown
        public static void ValidateDimension(Dimension dimension)
        {
            if (!dimension.IsValid())
            {
                throw new Exception("Invalid input: The numbers must be even and less than 100.");
            }
        }
    }
}
