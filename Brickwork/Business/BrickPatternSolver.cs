using Brickwork.Data;
using System;

namespace Brickwork.Business
{
    // This class is responsible for "solving" a TwoByTwoArea with a given BrickPattern
    class BrickPatternSolver
    {
        // Calls the correct solver depending on the BrickPattern
        // If none is found then an Exception is thrown
        public TwoByTwoArea Solve(TwoByTwoArea twoByTwoArea, BrickPattern brickPattern)
        {
            switch (brickPattern)
            {
                case BrickPattern.TwoVerticalBricks:
                case BrickPattern.OneVerticalBrick:
                    PlaceTwoHorizontalBricks(twoByTwoArea);
                    break;
                case BrickPattern.TwoHorizontalBricks:
                case BrickPattern.FourDifferentBricks:
                    PlaceTwoVerticalBricks(twoByTwoArea);
                    break;
                case BrickPattern.UnknownPattern:
                    throw new Exception("-1; There is no solution to your problem.");
            }

            return twoByTwoArea;
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
    }
}