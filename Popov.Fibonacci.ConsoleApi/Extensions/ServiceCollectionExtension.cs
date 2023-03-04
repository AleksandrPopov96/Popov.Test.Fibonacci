using EasyNetQ.AutoSubscribe;
using EasyNetQ;
using Popov.Fibonacci.ConsoleApi.Services;
using Popov.Fibonacci.ConsoleApi.Models;
using Popov.Fibonacci.WebApi.Client;
using Refit;

namespace Popov.Fibonacci.ConsoleApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterQueueServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var connectionString = configuration.GetConnectionString("RabbitMQ");
            services.RegisterEasyNetQ(connectionString, r =>
            {
                r.EnableConsoleLogger();
                r.EnableSystemTextJson();
            });

            services.AddSingleton<IAutoSubscriberMessageDispatcher, MicrosoftMessageDispatcher>();

            services.AddSingleton(sp =>
            {
                var subscriber = new AutoSubscriber(sp.GetRequiredService<IBus>(), "consumer")
                {
                    AutoSubscriberMessageDispatcher = sp.GetRequiredService<IAutoSubscriberMessageDispatcher>(),
                    ConfigureSubscriptionConfiguration = x =>
                    {
                        x.WithSingleActiveConsumer();
                        x.WithAutoDelete();
                    }
                };
                return subscriber;
            });

            services.AddScoped<FibonacciNumberConsumer>();

            return services;
        }

        public static IServiceCollection RegisterApiClient(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var apiSettings = configuration.GetSection("WebApi")
                .Get<ApiSettings>();

            services.AddRefitClient<IApiClient>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(apiSettings.BasePath);
                    c.Timeout = TimeSpan.FromMinutes(apiSettings.TimeoutInMinutes);
                });

            return services;
        }
    }
}
