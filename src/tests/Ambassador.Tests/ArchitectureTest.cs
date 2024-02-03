using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Ambassador.Tests;

public class ArchitectureTest
{
    private const string DomainNamespace = "Ambassador.Domain";

    [Fact]
    // [InlineData(typeof(Ambassador.AssemblyReference).Assembly)]
    public void Should_have_project_named_AmbassadorDomain()
    {
        // Arrange
        // var assembly = typeof(Ambassador.Domain.AssemblyReference).Assembly;
        var entryAssembly = Assembly.GetEntryAssembly();
        Assembly.
        //Act
        var existingAssemblies = Types
                .InAssembly(assembly)
                .Should()
                .HaveNameMatching(DomainNamespace)
                .GetResult();

        //Assert
        existingAssemblies.IsSuccessful.Should().BeTrue();
    }   
}