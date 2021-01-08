using Brickwork.Data;
using System;

namespace Brickwork
{
    class BricklayingAlgorithm
    {
        public static BrickLayer GenerateNextLayer(BrickLayer previousLayer)
        {
            BrickLayer nextLayer = previousLayer;
            for (int r = 0; r < nextLayer.GetRows(); r += 2)
            {
                for (int c = 0; c < nextLayer.GetColumns(); c += 2)
                {
                    LayBricksOnTwoByTwoArea(nextLayer, new Coordinates(r, c));
                }
            }
            return nextLayer;
        }

        public static void LayBricksOnTwoByTwoArea(BrickLayer brickLayer, Coordinates coordinates)
        {
            TwoByTwoArea twoByTwoArea = new TwoByTwoArea(brickLayer, coordinates);
            if (twoByTwoArea.IsTwoHorizontalBricks())
            {
                PlaceTwoVerticalBricks(twoByTwoArea);
            }
            else if (twoByTwoArea.IsTwoVerticalBricks())
            {
                PlaceTwoHorizontalBricks(twoByTwoArea);
            }
            else if (twoByTwoArea.ContainsOneVerticalBrick())
            {
                PlaceTwoHorizontalBricks(twoByTwoArea);
            }
            else if (twoByTwoArea.ContainsPartsOfFourDifferentBricks())
            {
                PlaceTwoVerticalBricks(twoByTwoArea);
            }
            else
            {
                throw new Exception("-1; There is no solution to your problem.");
            }
            PlaceBricksOnTwoByTwoAreaOnNextLayer(brickLayer, coordinates, twoByTwoArea);

        }

        private static void PlaceTwoHorizontalBricks(TwoByTwoArea twoByTwoArea)
        {
            bool secondBrickIsVertical = twoByTwoArea.SecondBrickIsVertical();
            if (secondBrickIsVertical)
            {
                twoByTwoArea.DownLeftBrick = twoByTwoArea.DownRightBrick;
                twoByTwoArea.UpRightBrick = twoByTwoArea.UpLeftBrick;
            }
            else
            {
                twoByTwoArea.UpRightBrick = twoByTwoArea.DownLeftBrick;
                twoByTwoArea.DownLeftBrick = twoByTwoArea.DownRightBrick;
            }
        }

        private static void PlaceTwoVerticalBricks(TwoByTwoArea twoByTwoArea)
        {
            bool fourDifferentParts = twoByTwoArea.ContainsPartsOfFourDifferentBricks();
            if (fourDifferentParts)
            {
                twoByTwoArea.DownLeftBrick = twoByTwoArea.UpLeftBrick;
                twoByTwoArea.UpRightBrick = twoByTwoArea.DownRightBrick;
            }
            else
            {
                twoByTwoArea.DownLeftBrick = twoByTwoArea.UpRightBrick;
                twoByTwoArea.UpRightBrick = twoByTwoArea.DownRightBrick;
            }
        }

        public static void PlaceBricksOnTwoByTwoAreaOnNextLayer(BrickLayer brickLayer, Coordinates coordinates, TwoByTwoArea twoByTwoArea)
        {
            brickLayer.Set(coordinates, twoByTwoArea.UpLeftBrick);
            brickLayer.Set(coordinates.Row, coordinates.Column + 1, twoByTwoArea.UpRightBrick);
            brickLayer.Set(coordinates.Row + 1, coordinates.Column, twoByTwoArea.DownLeftBrick);
            brickLayer.Set(coordinates.Row + 1, coordinates.Column + 1, twoByTwoArea.DownRightBrick);
        }
    }
}
