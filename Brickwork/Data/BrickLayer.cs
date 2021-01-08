using System;
using System.Collections.Generic;
using System.Text;

namespace Brickwork.Data
{
    class BrickLayer
    {
        //wrapper of multidimensional array
        //facade pattern
        //abstraction of the idea of multidimensional array

        private int[,] Layer { get; }

        // remove setter in order to prevent it being changed in other classes
        public Dimension Dimension { get; }

        public BrickLayer(int[,] layer)
        {
            Layer = layer;
            Dimension = new Dimension(this);
        }

        public int Get(Coordinates coordinates)
        {
            return Layer[coordinates.Row, coordinates.Column];
        }

        public int Get(int row, int column)
        {
            return Layer[row, column];
        }

        public void Set(Coordinates coordinates, int value)
        {
            Layer[coordinates.Row, coordinates.Column] = value;
        }

        public void Set(int row, int column, int value)
        {
            Layer[row, column] = value;
        }

        //heigth
        public int GetRows()
        {
            return Layer.GetLength(0);
        }

        //width
        public int GetColumns()
        {
            return Layer.GetLength(1);
        }


    }
}
