using EasyNetQ.AutoSubscribe;
using Popov.Fibomacci.Domain;
using Popov.Fibomacci.Domain.Absttract;
using Popov.Fibonacci.Abstract;
using Popov.Fibonacci.WebApi.Client;

namespace Popov.Fibonacci.ConsoleApi.Services
{
    public class FibonacciNumberConsumer : IConsumeAsync<FibonacciNumberDto>
    {
        private IFibonacciService CalcService { get; }
        private IApiClient ApiClient { get; }

        public FibonacciNumberConsumer(IFibonacciService calcService, IApiClient apiClient)
        {
            CalcService = calcService;
            ApiClient = apiClient;
        }

        public async Task ConsumeAsync(FibonacciNumberDto message, CancellationToken cancellationToken = default)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            try
            {
                Console.WriteLine($"Current - {message}");
                var next = CalcService.GetNext(message);
                Console.WriteLine($"Next - {next}");

                // todo задержка для удобства просмотра логов, убрать при необходимости
                await Task.Delay(100, cancellationToken);

                await ApiClient.CalcNextFibonacciAsync(next, cancellationToken);
            }
            catch (OverflowException)
            {
                MessageHelper.WriteOverflowMessage(message);
            }
        }
    }
}
