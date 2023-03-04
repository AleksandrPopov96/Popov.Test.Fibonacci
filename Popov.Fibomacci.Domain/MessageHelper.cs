using Popov.Fibonacci.Abstract;

namespace Popov.Fibomacci.Domain
{
    public static class MessageHelper
    {
        public static void WriteOverflowMessage(FibonacciNumberDto current)
        {
            if (current is null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            Console.WriteLine($"Calculation {current.CalcId} overflowed on {current.Index + 1} index");
        }
    }
}
