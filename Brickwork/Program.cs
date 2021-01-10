using Brickwork.Business;
using Brickwork.Data;
using Brickwork.Utilities;

namespace Brickwork
{
    class Program
    {
        static void Main()
        {
            BrickLayer previousLayer = ArrayUtils.ReadArray();
            BrickLayer nextLayer = BricklayingAlgorithm.GenerateNextLayer(previousLayer);
            ArrayUtils.PrintArray(nextLayer);
        }
    }
}

/*
 Example valid input:
    4 8
    1 2 2 12 5 7 7 16
    1 10 10 12 5 15 15 16
    9 9 3 4 4 8 8 14
    11 11 3 13 13 6 6 14

 Example invalid input:
    2 4
    1 1 1 2
    3 3 3 2
*/