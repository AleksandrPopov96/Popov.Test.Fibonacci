using Popov.Fibonacci.Abstract;
using Refit;

namespace Popov.Fibonacci.WebApi.Client
{

    public interface IApiClient
    {
        [Post("/api/Fibonacci/CalcNext")]
        Task CalcNextFibonacciAsync(FibonacciNumberDto message, CancellationToken cancellationToken = default);
    }
}