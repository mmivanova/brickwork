using System;

namespace Brickwork
{
    class BricklayingAlgorithm
    {
        public static int[,] GenerateNextLayer(int[,] previousLayer)
        {
            int[,] nextLayer = previousLayer;
            for (int r = 0; r < nextLayer.GetLength(0); r += 2)
            {
                for (int c = 0; c < nextLayer.GetLength(1); c += 2)
                {
                    LayBricksOnTwoByTwoArea(nextLayer, r, c);
                }
            }
            return nextLayer;
        }

        private static int[] GetValuesFromTwoByTwoArea(int[,] layer, int r, int c)
        {
            return new int[]{ layer[r, c],
                              layer[r, c + 1],
                              layer[r + 1, c],
                              layer[r + 1, c + 1]};
        }

        public static void LayBricksOnTwoByTwoArea(int[,] layer, int row, int column)
        {
            int[] values = GetValuesFromTwoByTwoArea(layer, row, column);
            if (IsTwoHorizontalBricks(values))
            {
                PlaceTwoVerticalBricks(layer, row, column, values);
            }
            else if (IsTwoVerticalBricks(values))
            {
                PlaceTwoHorizontalBticks(layer, row, column, values);
            }
            else if (ContainsOneVerticalBrick(values))
            {
                PlaceTwoHorizontalBticks(layer, row, column, values);
            }
            else if (ContainsPartsOfFourDifferentBricks(values))
            {
                PlaceTwoVerticalBricks(layer, row, column, values);
            }
            else
            {
                throw new Exception("-1; There is no solution to your problem.");
            }
        }

        private static bool ContainsPartsOfFourDifferentBricks(int[] values)
        {
            if (values[0] != values[1] &&
                values[1] != values[2] &&
                values[2] != values[3])
            {
                return true;
            }
            return false;
        }

        private static void PlaceTwoHorizontalBticks(int[,] layer, int row, int column, int[] values)
        {
            bool secondBrickIsVertical = values[1].Equals(values[3]) && values[0] != values[2];
            if (secondBrickIsVertical)
            {
                layer[row + 1, column] = values[1];
                layer[row, column + 1] = values[0];
            }
            else
            {
                layer[row, column + 1] = values[2];
                layer[row + 1, column] = values[3];
            }
        }

        private static void PlaceTwoVerticalBricks(int[,] layer, int row, int column, int[] values)
        {
            bool fourDifferentParts = ContainsPartsOfFourDifferentBricks(values);
            if (fourDifferentParts)
            {
                layer[row + 1, column] = values[0];
                layer[row, column + 1] = values[3];
            }
            else
            {
                layer[row + 1, column] = values[1];
                layer[row, column + 1] = values[3];
            }
        }

        public static bool ContainsOneVerticalBrick(int[] values)
        {
            bool firstBrickIsVertical = values[0].Equals(values[2]) && values[1] != values[3];
            bool secondBrickIsVertical = values[1].Equals(values[3]) && values[0] != values[2];
            bool hasOneVerticalBrick = firstBrickIsVertical || secondBrickIsVertical;
            
            return hasOneVerticalBrick;
        }

        private static bool IsTwoVerticalBricks(int[] values)
        {
            return values[0].Equals(values[2]) && values[1].Equals(values[3]);
        }

        private static bool IsTwoHorizontalBricks(int[] values)
        {
            return values[0].Equals(values[1]) && values[2].Equals(values[3]);
        }
    }
}
