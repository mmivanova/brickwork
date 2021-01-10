using Brickwork.Data;
using System;

namespace Brickwork.Business
{
    // This class is responsible for finding the correct BrickPattern of a given TwoByTwoArea
    class BrickPatternFinder
    {
        public BrickPattern Find(TwoByTwoArea twoByTwoArea)
        {
            if (twoByTwoArea.BothBricksAreHorizontal())
            {
                return BrickPattern.TwoHorizontalBricks;
            }
            else if (twoByTwoArea.BothBricksAreVertical())
            {
                return BrickPattern.TwoVerticalBricks;
            }
            else if (twoByTwoArea.ContainsOnlyOneVerticalBrick())
            {
                return BrickPattern.OneVerticalBrick;
            }
            else if (twoByTwoArea.ContainsPartsOfFourDifferentBricks())
            {
                return BrickPattern.FourDifferentBricks;
            }
            else
            {
                return BrickPattern.UnknownPattern;
            }
        }
    }
}
