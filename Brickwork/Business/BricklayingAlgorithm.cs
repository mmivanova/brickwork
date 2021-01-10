using Brickwork.Data;
using System;

namespace Brickwork.Business
{
    // Logic behind the generation of a new 
    // brick layer
    class BricklayingAlgorithm
    {
        private static readonly BrickPatternFinder brickPatternFinder = new BrickPatternFinder();
        private static readonly BrickPatternSolver brickPatternSolver = new BrickPatternSolver();

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

            BrickPattern brickPattern = brickPatternFinder.Find(twoByTwoArea);
            TwoByTwoArea solvedTwoByTwoArea = brickPatternSolver.Solve(twoByTwoArea, brickPattern);

            PlaceBricksOnTwoByTwoAreaOnNextLayer(brickLayer, coordinates, solvedTwoByTwoArea);
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
