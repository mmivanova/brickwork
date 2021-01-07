namespace Brickwork.Data
{
    class Dimension
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public Dimension(int[] array)
        {
            Rows = array[0];
            Columns = array[1];
        }

        public bool IsValid()
        {
            return (Rows % 2 == 0 && Rows <= 100) && 
                   (Columns % 2 == 0 && Columns <= 100);
        }
    }
}
