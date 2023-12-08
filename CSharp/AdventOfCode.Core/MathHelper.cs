namespace AdventOfCode.Core
{
    public class MathHelper
    {
        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static long LCM(long a, long b)
        {
            return (a / GCD(a, b)) * b;
        }

        public static long LCM(List<long> numbers)
        {
            long result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                result = LCM(result, numbers[i]);
            }
            return result;
        }
    }
}
