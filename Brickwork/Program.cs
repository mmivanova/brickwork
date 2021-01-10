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
