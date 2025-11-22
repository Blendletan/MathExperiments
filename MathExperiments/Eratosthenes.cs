namespace MathExperiments
{
    public static class Eratosthenes
    {
        public static int[] MoebiusMu(int max)
        {
            var isPrime = new bool[max + 1];
            var output = new int[max + 1];
            for (int i = 2; i <= max; i++)
            {
                isPrime[i] = true;
                output[i] = 1;
            }
            for (int prime = 2; prime <= max; prime++)
            {
                if (isPrime[prime] == false)
                {
                    continue;
                }
                for (int composite = prime; composite <= max; composite += prime)
                {
                    if (composite != prime)
                    {
                        isPrime[composite] = false;
                    }
                    output[composite] *= -prime;
                }
            }
            for (int i = 2; i <= max; i++)
            {
                if (Math.Abs(output[i]) != i)
                {
                    output[i] = 0;
                }
                else
                {
                    output[i] = output[i] / i;
                }
            }
            return output;
        }
        public static Primes GetPrimes(int max)
        {
            var isPrime = GetPrimesAsBoolArray(max);
            return new Primes(isPrime);
        }
        public static bool[] GetPrimesAsBoolArray(int max)
        {
            var isPrime = new bool[max + 1];
            int maxHeight = (int)Math.Sqrt(max);
            for (int i = 2; i <= max; i++)
            {
                isPrime[i] = true;
            }
            for (int prime = 2; prime <= maxHeight; prime++)
            {
                if (isPrime[prime] == false)
                {
                    continue;
                }
                for (int composite = prime * prime; composite <= max; composite += prime)
                {
                    isPrime[composite] = false;
                }
            }
            return isPrime;
        }
    }
    public class Primes
    {
        public int MaxPrimeSize { get; }
        public List<int> PrimeList { get; }
        public Primes(bool[] isPrime)
        {
            MaxPrimeSize = isPrime.Length;
            PrimeList = new List<int>();
            for (int i = 0; i < MaxPrimeSize; i++)
            {
                if (isPrime[i])
                {
                    PrimeList.Add(i);
                }
            }
        }
    }
    public static class PrimeGaps
    {
        public static double[] GetPrimeGaps(int max)
        {
            var p = Eratosthenes.GetPrimes(max).PrimeList;
            var doubleP = p.Select(x => (double)x).ToList();
            return DiscreteDifferences<double>.GetDifferences(doubleP).ToArray();
        }
    }
}
