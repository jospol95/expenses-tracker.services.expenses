using Expenses.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Expenses.Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddHealthChecks()
                .AddDbContextCheck<ExpensesDbContext>();
            return serviceCollection;
        }
    }
}