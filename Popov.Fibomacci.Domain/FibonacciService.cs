using Popov.Fibomacci.Domain.Absttract;
using Popov.Fibonacci.Abstract;

namespace Popov.Fibomacci.Domain
{
    public class FibonacciService : IFibonacciService
    {
        public FibonacciNumberDto GetNext(FibonacciNumberDto current)
        {
            if (current is null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            return current with
            {
                Value = CalcNext(current.Index, current.Value),
                Index = current.Index + 1,
            };                        
        }


        private static long CalcNext(int index, long currentValue)
        {
            if (index == 0 || index == 1)
                return 1;

            checked
            {
                var result = Math.Round(currentValue * (1 + Math.Sqrt(5)) / 2.0);
                return (long)result;
            }          
        }
    }
}
