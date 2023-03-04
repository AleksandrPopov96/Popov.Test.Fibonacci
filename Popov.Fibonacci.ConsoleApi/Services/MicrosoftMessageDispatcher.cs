using EasyNetQ.AutoSubscribe;

namespace Popov.Fibonacci.ConsoleApi.Services
{
    public class MicrosoftMessageDispatcher : IAutoSubscriberMessageDispatcher
    {
        private IServiceScopeFactory ScopeFactory { get; }

        public MicrosoftMessageDispatcher(IServiceScopeFactory scopeFactory)
        {
            ScopeFactory = scopeFactory;
        }

        public void Dispatch<TMessage, TConsumer>(TMessage message, CancellationToken cancellationToken = default)
            where TMessage : class
            where TConsumer : class, IConsume<TMessage>
        {
            using var scope = ScopeFactory.CreateScope();

            var consumer = scope.ServiceProvider.GetRequiredService<TConsumer>();
            consumer.Consume(message, cancellationToken);
        }

        public async Task DispatchAsync<TMessage, TConsumer>(TMessage message, CancellationToken cancellationToken = default)
            where TMessage : class
            where TConsumer : class, IConsumeAsync<TMessage>
        {
            using var scope = ScopeFactory.CreateScope();

            var consumer = scope.ServiceProvider.GetRequiredService<TConsumer>();
            await consumer.ConsumeAsync(message, cancellationToken);
        }
    }
}
