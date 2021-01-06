namespace Brickwork
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

        public bool IsValidNumber(int number)
        {
            return number % 2 == 0 && number <= 100;
        }
    }
}
