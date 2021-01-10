namespace Brickwork.Data
{
    // Represents an area of two by two values 
    // of a multidimensional array
    class TwoByTwoArea
    {
        public int UpLeftBrick { get; set; }
        public int UpRightBrick { get; set; }
        public int DownLeftBrick { get; set; }
        public int DownRightBrick { get; set; }
        public TwoByTwoArea(BrickLayer brickLayer, Coordinates coordinates)
        {   
            UpLeftBrick = brickLayer.Get(coordinates);
            UpRightBrick = brickLayer.Get(coordinates.Row, coordinates.Column + 1);
            DownLeftBrick = brickLayer.Get(coordinates.Row + 1, coordinates.Column);
            DownRightBrick = brickLayer.Get(coordinates.Row + 1, coordinates.Column + 1);
        }
        
        // Second brick will be vertical if both values 
        // on the right side of the area are the same
        public bool OnlySecondBrickIsVertical() => UpRightBrick.Equals(DownRightBrick) &&
                                              UpLeftBrick != DownLeftBrick;

        // First brick will be vertical if both values 
        // on the left side of the area are the same
        public bool OnlyFirstBrickIsVertical() => UpLeftBrick.Equals(DownLeftBrick) &&
                                              UpRightBrick != DownRightBrick;
        
        // Both bricks will be vertical if 
        // two by two values are equal on both 
        // left and right side of the area
        public bool BothBricksAreVertical() => UpLeftBrick.Equals(DownLeftBrick) && UpRightBrick.Equals(DownRightBrick);

        // Both bricks will be vertical if 
        // two by two values are equal on both 
        // upper and lower side of the area
        public bool BothBricksAreHorizontal() => UpLeftBrick.Equals(UpRightBrick) && DownLeftBrick.Equals(DownRightBrick);

        // Checks if either the left or the right brick is vertical
        public bool ContainsOnlyOneVerticalBrick() => OnlyFirstBrickIsVertical() || OnlySecondBrickIsVertical();

        // Checks whether the four values 
        // differ from each other  
        public bool ContainsPartsOfFourDifferentBricks()
        {
            return UpLeftBrick != UpRightBrick &&
                UpRightBrick != DownLeftBrick &&
                DownLeftBrick != DownRightBrick;
        }
    }
}
