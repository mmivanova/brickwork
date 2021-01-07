using Brickwork.Utilities;

namespace Brickwork
{
    class Program
    {
        static void Main()
        {
            int[,] previousLayer = ArrayUtils.ReadArray();
            int[,] nextLayer = BricklayingAlgorithm.GenerateNextLayer(previousLayer);
            ArrayUtils.PrintArray(nextLayer);
        }
    }
}
