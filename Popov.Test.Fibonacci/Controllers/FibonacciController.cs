using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Popov.Fibomacci.Domain;
using Popov.Fibomacci.Domain.Absttract;
using Popov.Fibonacci.Abstract;

namespace Popov.Test.Fibonacci.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        private IFibonacciService FibonacciService { get; }
        private IBus Bus { get; }

        public FibonacciController(IFibonacciService fibonacciService, IBus bus)
        {
            FibonacciService = fibonacciService;
            Bus = bus;
        }

        /// <summary>
        /// Рассчитывает следующее число Фибоначчи
        /// </summary>
        /// <param name="current">Текущее число Фибоначчи</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpPost("CalcNext")]
        public async Task CalcNextAsync(FibonacciNumberDto current, CancellationToken cancellationToken = default)
        {
            if (current is null)
            {
                throw new ArgumentNullException(nameof(current));
            }

            try
            {
                Console.WriteLine($"Current - {current}");
                var next = FibonacciService.GetNext(current);
                Console.WriteLine($"Next - {next}");

                await Bus.PubSub.PublishAsync(next, cancellationToken);
            }
            catch (OverflowException)
            {
                MessageHelper.WriteOverflowMessage(current);
            }
            
        }
    }
}
