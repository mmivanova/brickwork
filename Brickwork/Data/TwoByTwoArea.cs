namespace Brickwork.Data
{
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

        public bool SecondBrickIsVertical() => UpRightBrick.Equals(DownRightBrick) &&
                                              UpLeftBrick != DownLeftBrick;

        public bool FirstBrickIsVertical() => UpLeftBrick.Equals(DownLeftBrick) &&
                                              UpRightBrick != DownRightBrick;

        public bool IsTwoVerticalBricks() => UpLeftBrick.Equals(DownLeftBrick) && UpRightBrick.Equals(DownRightBrick);

        public bool IsTwoHorizontalBricks() => UpLeftBrick.Equals(UpRightBrick) && DownLeftBrick.Equals(DownRightBrick);

        public bool ContainsOneVerticalBrick() => FirstBrickIsVertical() || SecondBrickIsVertical();

        public bool ContainsPartsOfFourDifferentBricks()
        {
            return UpLeftBrick != UpRightBrick &&
                UpRightBrick != DownLeftBrick &&
                DownLeftBrick != DownRightBrick;
        }

    }
}
