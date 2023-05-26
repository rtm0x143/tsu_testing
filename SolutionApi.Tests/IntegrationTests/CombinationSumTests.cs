using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SolutionApi.Controllers;
using SolutionApi.Tests.TestCaseSources;

namespace SolutionApi.Tests.IntegrationTests;

[TestFixture(Description = $"Tests for POST ~/{Route} endpoint")]
public class CombinationSumTests
{
    private const string Route = "CombinationSum";

    private readonly WebApplicationFactory<Program> _factory;

    public CombinationSumTests()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [Test(Description = "Checks behaviour if valid data are provided")]
    [TestCaseSource(typeof(CombinationSumSources), nameof(CombinationSumSources.ValidTestCaseSource))]
    public async Task GivenValidData_WhenPostEndpointCalled_ThenReturnCorrectResult(
        CombinationSumSources.Source args)
    {
        var sut = _factory.CreateClient();

        var message = await sut.PostAsJsonAsync(Route, new CallCombinationSumCommand(args.Candidates, args.Target));
        message.EnsureSuccessStatusCode();

        var combinations = await message.Content.ReadFromJsonAsync<int[][]>();
        AssertHelper.AssertSumCombinationsEquivalent(args.ExpectedResult, combinations);
    }

    [Test(Description = $"Test with invalid {nameof(args.Candidates)} collection length")]
    [TestCaseSource(typeof(CombinationSumSources), nameof(CombinationSumSources.InvalidCandidatesLengthTestCaseSource))]
    public async Task GivenInvalidCandidatesLength_WhenPostEndpointCalled_ThenReturnsBadRequest(
        CombinationSumSources.Source args)
    {
        var sut = _factory.CreateClient();

        var message = await sut.PostAsJsonAsync(Route, new CallCombinationSumCommand(args.Candidates, args.Target));
        Assert.That(message.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test(Description = $"Test with invalid {nameof(args.Candidates)} values")]
    [TestCaseSource(typeof(CombinationSumSources), nameof(CombinationSumSources.InvalidCandidatesValuesTestCaseSource))]
    public async Task GivenInvalidCandidatesValues_WhenPostEndpointCalled_ThenReturnsBadRequest(
        CombinationSumSources.Source args)
    {
        var sut = _factory.CreateClient();

        var message = await sut.PostAsJsonAsync(Route, new CallCombinationSumCommand(args.Candidates, args.Target));
        Assert.That(message.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test(Description = "Cases with not distinct candidates values")]
    [TestCaseSource(typeof(CombinationSumSources),
        nameof(CombinationSumSources.NotDistinctCandidatesValuesTestCaseSource))]
    public async Task GivenNotDistinctCandidatesValues_WhenPostEndpointCalled_ThenReturnsBadRequest(
        CombinationSumSources.Source args)
    {
        var sut = _factory.CreateClient();

        var message = await sut.PostAsJsonAsync(Route, new CallCombinationSumCommand(args.Candidates, args.Target));
        Assert.That(message.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test(Description = $"Test with invalid {nameof(args.Target)} values")]
    [TestCaseSource(typeof(CombinationSumSources), nameof(CombinationSumSources.InvalidTargetValueTestCaseSource))]
    public async Task GivenInvalidTargetValue_WhenPostEndpointCalled_ThenReturnsBadRequest(
        CombinationSumSources.Source args)
    {
        var sut = _factory.CreateClient();

        var message = await sut.PostAsJsonAsync(Route, new CallCombinationSumCommand(args.Candidates, args.Target));
        Assert.That(message.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}