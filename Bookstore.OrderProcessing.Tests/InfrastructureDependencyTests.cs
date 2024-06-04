using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Xunit.Abstractions;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Bookstore.OrderProcessing.Tests;

public class InfrastructureDependencyTests
{
    private readonly ITestOutputHelper _outputHelper;

    public InfrastructureDependencyTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    private static readonly Architecture Architecture
        = new ArchLoader()
        .LoadAssemblies(typeof(OrderProcessing.AssemblyInfo).Assembly)
        .Build();

    [Fact]
    public void DomainTypesShouldNotReferenceInfrastructure()
    {
        var domainTypes = Types().That()
            .ResideInNamespace("Bookstore.OrderProcessing.Domain.*", useRegularExpressions: true)
            .As("OrderProcessing Domain Types");

        var infrastructureTypes = Types().That()
            .ResideInNamespace("Bookstore.OrderProcessing.Infrastructure.*", useRegularExpressions: true)
            .As("OrderProcessing Infrastructure Types");

        var rule = domainTypes.Should().NotDependOnAny(infrastructureTypes);

        rule.Check(Architecture);
    }

    //TODO: create tests for users module, make sure that use cases are not depending on infrastructure directly
    //they should be using interfaces
}
