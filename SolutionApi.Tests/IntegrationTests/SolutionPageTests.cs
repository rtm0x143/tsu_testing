using Microsoft.AspNetCore.Mvc.Testing;

namespace SolutionApi.Tests.IntegrationTests;

public class SolutionPageTests
{
    private readonly WebApplicationFactory<Program> _factory;

    public SolutionPageTests()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [TestCase("Solution/Index")]
    [TestCase("Solution")]
    [TestCase("/")]
    public async Task WhenGetEndpointCalled_ThenReturnSuccessHtmlContentType(string url)
    {
        var httpClient = _factory.CreateClient();

        var message = await httpClient.GetAsync(url);
        message.EnsureSuccessStatusCode();

        Assert.That(message.Content.Headers.ContentType?.ToString(), Is.EqualTo("text/html; charset=utf-8"));
    }
}