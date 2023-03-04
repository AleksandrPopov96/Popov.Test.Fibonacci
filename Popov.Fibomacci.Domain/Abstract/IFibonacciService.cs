using Popov.Fibonacci.Abstract;

namespace Popov.Fibomacci.Domain.Absttract
{
    public interface IFibonacciService
    {
        FibonacciNumberDto GetNext(FibonacciNumberDto current);
    }
}
