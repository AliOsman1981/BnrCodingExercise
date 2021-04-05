using BnrCodingExercise.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BnrCodingExercise.Tests
{
    internal static class TestHelpers
    {
        internal static DbContextOptions BuildDbOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDataContext>();
            builder.UseInMemoryDatabase("BndCodingExercise")
                .UseInternalServiceProvider(serviceProvider);
            return builder.Options;
        }
    }
}
