namespace Brickwork.Data
{
    class TwoByTwoArea
    {
        public int FirstRowFirstBrickValue { get; set; }
        public int FirstRowSecondBrickValue { get; set; }
        public int SecondRowFirstBrickValue { get; set; }
        public int SecondRowSecondBrickValue { get; set; }

        public TwoByTwoArea(int[,] layer)
        {
            FirstRowFirstBrickValue = layer[0, 0];
            FirstRowSecondBrickValue = layer[0, 1];
            SecondRowFirstBrickValue = layer[1, 0];
            SecondRowSecondBrickValue = layer[1, 1];
        }


    }
}
