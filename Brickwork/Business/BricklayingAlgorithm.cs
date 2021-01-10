using Brickwork.Data;
using System;

namespace Brickwork.Business
{
    // Logic behind the generation of a new 
    // brick layer
    class BricklayingAlgorithm
    {
        // Generates the next layer of bricks by dividing the 
        // layer on two by two areas 
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

        // With the given coordinates a new two by two area is generated 
        // and a brick pattern is being found. Then two new bricks
        // are generated and are set on the new layer
        public static void LayBricksOnTwoByTwoArea(BrickLayer brickLayer, Coordinates coordinates)
        {
            TwoByTwoArea twoByTwoArea = new TwoByTwoArea(brickLayer, coordinates);
            if (twoByTwoArea.BothBricksAreHorizontal())
            {
                PlaceTwoVerticalBricks(twoByTwoArea);
            }
            else if (twoByTwoArea.BothBricksAreVertical())
            {
                PlaceTwoHorizontalBricks(twoByTwoArea);
            }
            else if (twoByTwoArea.ContainsOnlyOneVerticalBrick())
            {
                PlaceTwoHorizontalBricks(twoByTwoArea);
            }
            else if (twoByTwoArea.ContainsPartsOfFourDifferentBricks())
            {
                PlaceTwoVerticalBricks(twoByTwoArea);
            }
            else
            {
                // If no pattern is found it throws an exeption for missing solution
                throw new Exception("-1; There is no solution to your problem.");
            }
            PlaceBricksOnTwoByTwoAreaOnNextLayer(brickLayer, coordinates, twoByTwoArea);

        }

        // If on two by two area both or one of the bricks are vertical
        // two horizontal ones are placed above them
        private static void PlaceTwoHorizontalBricks(TwoByTwoArea twoByTwoArea)
        {
            bool secondBrickIsVertical = twoByTwoArea.OnlySecondBrickIsVertical();
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

        // If on two by two area both of the bricks are horizontal
        // or there are four different bricks
        // two vertical ones are placed above
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

        // Sets the values to the two by two area 
        // on the next layer
        public static void PlaceBricksOnTwoByTwoAreaOnNextLayer(BrickLayer brickLayer, Coordinates coordinates, TwoByTwoArea twoByTwoArea)
        {
            brickLayer.Set(coordinates, twoByTwoArea.UpLeftBrick);
            brickLayer.Set(coordinates.Row, coordinates.Column + 1, twoByTwoArea.UpRightBrick);
            brickLayer.Set(coordinates.Row + 1, coordinates.Column, twoByTwoArea.DownLeftBrick);
            brickLayer.Set(coordinates.Row + 1, coordinates.Column + 1, twoByTwoArea.DownRightBrick);
        }
    }
}
