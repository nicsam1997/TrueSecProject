using TrueSecProject.Models;
using TrueSecProject.Exceptions;
using Xunit;

namespace TrueSecProject.UnitTests;

public class ExternalReferenceTests
{
    [Fact]
    public void Constructor_WithValidInputs_CreatesInstance()
    {
        // Arrange
        var sourceName = "test_source";
        var description = "A test description.";

        // Act
        var externalReference = new ExternalReference(sourceName, description: description);

        // Assert
        Assert.NotNull(externalReference);
        Assert.Equal(sourceName, externalReference.SourceName);
        Assert.Equal(description, externalReference.Description);
    }

    [Fact]
    public void Constructor_WithAllValidInputs_AssignsPropertiesCorrectly()
    {
        // Arrange
        var sourceName = "test_source";
        var description = "A test description.";
        var url = "http://example.com";
        var externalId = "test-123";
        var hashes = new Hashes();

        // Act
        var externalReference = new ExternalReference(sourceName, hashes, description, url, externalId);

        // Assert
        Assert.Equal(sourceName, externalReference.SourceName);
        Assert.Equal(description, externalReference.Description);
        Assert.Equal(url, externalReference.Url);
        Assert.Equal(externalId, externalReference.ExternalId);
        Assert.Equal(hashes, externalReference.Hashes);
    }

    [Fact]
    public void Constructor_MissingDescriptionUrlAndExternalId_ThrowsInvalidStructureException()
    {
        // Arrange
        var sourceName = "test_source";

        // Act & Assert
        var exception = Assert.Throws<InvalidStructureException>(() => new ExternalReference(sourceName));
        Assert.Equal("In an external_reference, in addition to source_name, at least one of description, url, or external_id must be provided.", exception.Message);
    }

    [Theory]
    [InlineData("CVE-2021-12345")]
    [InlineData("CVE-1999-0001")]
    public void Constructor_WithCveSourceNameAndValidExternalId_CreatesInstance(string validCveId)
    {
        // Arrange
        var sourceName = "cve";

        // Act
        var externalReference = new ExternalReference(sourceName, externalId: validCveId);

        // Assert
        Assert.NotNull(externalReference);
        Assert.Equal(sourceName, externalReference.SourceName);
        Assert.Equal(validCveId, externalReference.ExternalId);
    }

    [Fact]
    public void Constructor_WithCveSourceNameCaseInsensitiveAndValidExternalId_CreatesInstance()
    {
        // Arrange
        var sourceName = "CVE";
        var validCveId = "CVE-2023-45678";

        // Act
        var externalReference = new ExternalReference(sourceName, externalId: validCveId);

        // Assert
        Assert.NotNull(externalReference);
        Assert.Equal(sourceName, externalReference.SourceName);
        Assert.Equal(validCveId, externalReference.ExternalId);
    }

    [Theory]
    [InlineData("invalid-cve")]
    [InlineData("CVE-123-4567")]
    [InlineData("CVE-2021-123")]
    public void Constructor_WithCveSourceNameAndInvalidExternalId_ThrowsInvalidStructureException(string invalidCveId)
    {
        // Arrange
        var sourceName = "cve";

        // Act & Assert
        var exception = Assert.Throws<InvalidStructureException>(() => new ExternalReference(sourceName, externalId: invalidCveId));
        Assert.Equal("In an external_reference,, when source_name is 'cve', the external_id must be a valid CVE identifier  matching the format 'CVE-YYYY-NNNN...'.", exception.Message);
    }

    [Fact]
    public void Constructor_WithCveSourceNameAndMissingExternalId_ThrowsInvalidStructureException()
    {
        // Arrange
        var sourceName = "cve";

        // Act & Assert
        var exception = Assert.Throws<InvalidStructureException>(() => new ExternalReference(sourceName, description: "A description without an ID"));
        Assert.Equal("In an external_reference,, when source_name is 'cve', the external_id must be a valid CVE identifier  matching the format 'CVE-YYYY-NNNN...'.", exception.Message);
    }
}