using Brickwork.Data;
using System;
using System.Collections.Generic;

namespace Brickwork.Utilities
{
    class Validator
    {
        public static void ValidateArray(BrickLayer brickLayer)
        {
            if (!IsValidArray(brickLayer))
            {
                throw new Exception("Invalid input. Each number should be presented exactly twice.");
            }
        }

        private static bool IsValidArray(BrickLayer brickLayer)
        {
            bool IsValidFlag = true;
            if (brickLayer.GetRows() <= 2 && brickLayer.GetColumns() <= 2)
            {
                goto exit;
            }
            else if (brickLayer.GetRows() <= 2 || brickLayer.GetColumns() <= 2)
            {
                IsValidFlag = IsValidArrayWithOneDimensionOfTwo(brickLayer);
                goto exit;
            }
            else
            {
                IsValidFlag = IsValidArrayWithDimensionsMoreThanTwo(brickLayer);
                goto exit;
            }
        exit:
            return IsValidFlag;
        }

        private static bool IsValidArrayWithOneDimensionOfTwo(BrickLayer brickLayer)
        {
            bool IsValidFlag = true;
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
                            IsValidFlag = false;
                            goto exit;
                        }
                    }
                    else
                    {
                        brickValuesWithCount.Add(brickLayer.Get(coordinates), 1);
                    }
                }
            }
        exit:
            return IsValidFlag;
        }

        private static bool IsValidArrayWithDimensionsMoreThanTwo(BrickLayer brickLayer)
        {
            bool IsValidFlag = true;
            for (int r = 0; r < brickLayer.GetRows() - 2; r++)
            {
                for (int c = 0; c < brickLayer.GetColumns() - 2; c++)
                {
                    Coordinates coordinates = new Coordinates(r, c);
                    bool containsThreeIdenticalVerticalIndexes = brickLayer.Get(coordinates) == brickLayer.Get(r + 1, c) &&
                                                                  brickLayer.Get(r + 1, c) == brickLayer.Get(r + 2, c);
                    bool containsThreeIdenticalHorizontalIndexes = brickLayer.Get(r, c) == brickLayer.Get(r, c + 1) &&
                                                                   brickLayer.Get(r, c + 1) == brickLayer.Get(r, c + 2);
                    if (containsThreeIdenticalHorizontalIndexes || containsThreeIdenticalVerticalIndexes)
                    {
                        IsValidFlag = false;
                        goto exit;
                    }
                }
            }
            exit:
            return IsValidFlag;
        }

        public static void ValidateDimention(Dimension dimension)
        {
            if (!dimension.IsValid())
            {
                throw new Exception("Invalid input: The numbers must be even and less than 100.");
            }
        }
    }
}
