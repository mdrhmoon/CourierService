using FluentAssertions;
using NetArchTest.Rules;

namespace Ambassador.Tests;

public class ArchitectureTest
{
    private const string DomainNamespace = "Ambassador.Domain";
    private const string ApplicationNamespace = "Ambassador.Application";
    private const string InfrastructureNamespace = "Ambassador.Infrastructure";
    private const string ApiNamespace = "Ambassador.API";
    private const string TestNamespace = "Ambassador.Tests";

    [Fact]
    public void Domain_Should_Not_Have_DependencyOnOtherProject()
    {
        // Arrange
        var assembly = typeof(Domain.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            ApiNamespace,
            TestNamespace
        };

        //Act
        var existingAssemblies = 
            Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

        //Assert
        existingAssemblies.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Have_DependencyOnDomainProject()
    {
        // Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;

        //Act
        var existingAssemblies = 
            Types
                .InAssembly(assembly)
                .That()
                .ResideInNamespace("Ambassador")
                .Should()
                .HaveDependencyOn(DomainNamespace)
                .GetResult();

        //Assert
        existingAssemblies.IsSuccessful.Should().BeTrue();
    }

    // [Fact]
    // public void Application_Should_Have_DependencyOnDomainProject()
    // {
    //     // Arrange
    //     var assembly = typeof(Ambassador.Application.AssemblyReference).Assembly;
    //     var projects = new[] { DomainNamespace };

    //     //Act
    //     var existingAssemblies = 
    //         Types
    //             .InAssembly(assembly)
    //             .Should()
    //             .HaveDependencyOnAll(projects)
    //             .GetResult();

    //     //Assert
    //     existingAssemblies.IsSuccessful.Should().BeTrue();
    // }
}
