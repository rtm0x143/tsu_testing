using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Chrome;
using SolutionApi.Tests.TestCaseSources;

namespace SolutionApi.Tests.UITests.SolutionPage;

public class SolutionPageTests : IDisposable
{
    private ChromeDriver _chromeDriver;
    private string _pageUrl;
    private SolutionPage _page = default!;

    public SolutionPageTests()
    {
        _chromeDriver = new ChromeDriver();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _pageUrl = configuration["SolutionApiUrl"]
                   ?? throw new ArgumentException("Couldn't find 'SolutionApiUrl' in configuration");
    }

    public void Dispose()
    {
        _chromeDriver.Quit();
        _chromeDriver.Dispose();
    }

    [SetUp]
    public void SetUp()
    {
        _chromeDriver.Navigate().GoToUrl(_pageUrl);
        _page = new SolutionPage(_chromeDriver);
    }

    [Test(Description = "Checks title")]
    public void PageContainsCorrectTitle()
    {
        Assert.That(_chromeDriver.Title, Is.EqualTo("Combination Sum Algorithm"));
    }

    [TestCase("0")]
    [TestCase("500")]
    [TestCase("")]
    public void GivenIncorrectTargetInput_WhenExecutePressed_ThenTargetInputErrorShown(string target)
    {
        _page.Target.SendKeys("0");
        _page.CandidatesSeq.SendKeys("3,4");
        _page.Execute.Click();
        
        Assert.That(string.IsNullOrWhiteSpace(_page.TargetError.Text), Is.False);
    }

    [TestCase("3,4,")]
    [TestCase("")]
    [TestCase("0,1")]
    [TestCase("100,500")]
    public void GivenIncorrectCandidatesInput_WhenExecutePressed_ThenCandidatesInputErrorShown(string candidatesSeq)
    {
        _page.Target.SendKeys("3");
        _page.CandidatesSeq.SendKeys(candidatesSeq);
        _page.Execute.Click();
        
        Assert.That(string.IsNullOrWhiteSpace(_page.CandidatesSeqError.Text), Is.False);
    }

    [TestCaseSource(typeof(CombinationSumSources), nameof(CombinationSumSources.ValidTestCaseSource))]
    public void GivenCorrectInput_WhenExecutePressed_ThenResultShown(CombinationSumSources.Source source)
    {
        _page.Target.SendKeys(source.Target.ToString());
        _page.CandidatesSeq.SendKeys(string.Join(',', source.Candidates));
        _page.Execute.Click();

        Assert.That(string.IsNullOrWhiteSpace(_page.Result.Text), Is.False);
    }
}