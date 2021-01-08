namespace Brickwork.Data
{
    class Dimension
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public Dimension(int[] userInput)
        {
            Rows = userInput[0];
            Columns = userInput[1];
        }

        public Dimension(BrickLayer brickLayer)
        {
            Rows = brickLayer.GetRows();
            Columns = brickLayer.GetColumns();
        }

        public bool IsValid()
        {
            return (Rows % 2 == 0 && Rows < 100) && 
                   (Columns % 2 == 0 && Columns < 100);
        }
    }
}
