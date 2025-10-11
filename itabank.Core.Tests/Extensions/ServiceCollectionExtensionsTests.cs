using AutoMapper;
using FluentAssertions;
using itabank.Core.Repositories.Interfaces;
using itabank.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace itabank.Core.Extensions.Tests;

public class ServiceCollectionExtensionsTests
{
    [Test]
    public void OK()
    {
        var services = new ServiceCollection();

        services.AddItaBank();

        var provider = services.BuildServiceProvider();

        provider.GetRequiredService<IAccountRepo>().Should().NotBeNull();
        provider.GetRequiredService<IAccountService>().Should().NotBeNull();
        provider.GetRequiredService<IMapper>().Should().NotBeNull();
        provider.GetRequiredService<ITransactionService>().Should().NotBeNull();
    }
}