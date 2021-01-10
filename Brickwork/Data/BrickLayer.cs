namespace Brickwork.Data
{
    // Wrapping and abstracting out the complexity of 
    // a multidimensional array
    class BrickLayer
    {
        // Remove setter in order to make it inaccessible
        private int[,] Layer { get; }

        // Remove setter in order to make it immutable
        public Dimension Dimension { get; }

        public BrickLayer(int[,] layer)
        {
            Layer = layer;
            Dimension = new Dimension(this);
        }

        // Returns the value behind the given coordinates
        public int Get(Coordinates coordinates)
        {
            return Layer[coordinates.Row, coordinates.Column];
        }

        // Overload of Get(coordinates)
        public int Get(int row, int column)
        {
            return Layer[row, column];
        }

        // Sets new value to the given coordinates
        public void Set(Coordinates coordinates, int value)
        {
            Layer[coordinates.Row, coordinates.Column] = value;
        }

        // Overload of Set(coordinates, value)
        public void Set(int row, int column, int value)
        {
            Layer[row, column] = value;
        }

        // Returns the heigth of the layer
        public int GetRows()
        {
            return Layer.GetLength(0);
        }

        // Returns the width of the layer
        public int GetColumns()
        {
            return Layer.GetLength(1);
        }
    }
}
