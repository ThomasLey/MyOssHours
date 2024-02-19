using FluentAssertions;
using MyOssHours.Backend.Domain.Exceptions;
using NetArchTest.Rules;
using Xunit;

namespace MyOssHours.Backend.Architectural.Tests
{
    public class ProjectDependencyTests
    {
        public static string DomainNamespace = typeof(AssemblyReference).Namespace;
        public static string ApplicationNamespace = typeof(Application.AssemblyReference).Namespace;
        public static string InfrastructureNamespace = typeof(Infrastructure.AssemblyReference).Namespace;
        public static string PresentationNamespace = typeof(Presentation.AssemblyReference).Namespace;
        public static string WebNamespace = typeof(REST.AssemblyReference).Namespace;

        [Fact]
        public void Domain_Should_Not_HaveDependencyToOtherProjects()
        {
            // Arrange
            var assembly = typeof(AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Should_Not_HaveDependencyToOtherProjects()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Infrastructure_Should_Not_HaveDependencyToOtherProjects()
        {
            // Arrange
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                PresentationNamespace,
                WebNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Presentation_Should_Not_HaveDependencyToOtherProjects()
        {
            // Arrange
            var assembly = typeof(Presentation.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                InfrastructureNamespace,
                WebNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

    }
}
