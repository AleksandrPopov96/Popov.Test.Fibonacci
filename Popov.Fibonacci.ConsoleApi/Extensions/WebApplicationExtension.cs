using Popov.Fibonacci.Abstract;
using Popov.Fibonacci.WebApi.Client;

namespace Popov.Fibonacci.ConsoleApi.Extensions
{
    public static class WebApplicationExtension
    {
        public static async Task RunCalculationsAsync(this WebApplication app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var calculationCount = app.Configuration.GetValue<int>("CalculationCount");

            var client = app.Services.GetRequiredService<IApiClient>();

            var tasks = new List<Task>(calculationCount);

            for (int i = 0; i < calculationCount; i++)
            {
                var initialFibonacciNumber = new FibonacciNumberDto
                {
                    CalcId = Guid.NewGuid()
                };

                var currentTask = client.CalcNextFibonacciAsync(initialFibonacciNumber);
                tasks.Add(currentTask);
            }

            await Task.WhenAll(tasks);

            Console.WriteLine($"{calculationCount} Fibonacci calculations is started");
        }
    }
}
