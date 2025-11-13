using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MathExperiments
{
    public static class DiscreteDifferences<T> where T: ISignedNumber<T>
    {
        public static List<T> GetDifferences(List<T> input)
        {
            var output = new List<T>();
            int length = input.Count;
            for (int i = 1; i < length; i++)
            {
                output.Add(input[i] - input[i - 1]);
            }
            return output;
        }
    }
}
