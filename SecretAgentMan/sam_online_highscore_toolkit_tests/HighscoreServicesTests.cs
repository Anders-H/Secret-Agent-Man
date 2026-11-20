using System.Net;
using sam_online_highscore_toolkit;

namespace sam_online_highscore_toolkit_tests;

[TestClass]
public sealed class HighscoreServicesTests
{
    private sealed class TestSettings : ISettings
    {
        public string BaseUrl => "https://example.test/";
        public string Password => "a&b";
    }

    private sealed class Handler(Func<HttpRequestMessage, Task<HttpResponseMessage>> respond) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => respond(request);
    }

    private static HttpResponseMessage Response(string data) =>
        new(HttpStatusCode.OK) { Content = new StringContent(data) };

    [TestMethod]
    public async Task ParsesScoresAndDatesWithoutContactingServer()
    {
        using var client = new HttpClient(new Handler(_ => Task.FromResult(Response("100|2026-11-20|abc;50|2026-11-19|def;"))));
        var scores = await new HighscoreServices(new TestSettings(), client).GetGlobalHighscores();
        Assert.HasCount(2, scores);
        Assert.AreEqual(100, scores[0].Score);
        Assert.AreEqual("abc", scores[0].PlayerName);
        Assert.AreEqual(new DateOnly(2026, 11, 20), scores[0].Date);
        Assert.AreEqual(2, scores[1].Position);
    }

    [TestMethod]
    public async Task SaveIsAcknowledgedBeforeRefreshStartsAndQueryValuesAreEncoded()
    {
        var acknowledged = new TaskCompletionSource<HttpResponseMessage>(TaskCreationOptions.RunContinuationsAsynchronously);
        var requests = new List<string>();

        using var client = new HttpClient(new Handler(request =>
        {
            var url = request.RequestUri!.AbsoluteUri;
            requests.Add(url);
            return requests.Count == 1 ? acknowledged.Task : Task.FromResult(Response("42|2026-11-20|abc"));
        }));

        var saving = new HighscoreServices(new TestSettings(), client).SaveGlobalHighscoreEntry(42, "a&b");
        Assert.HasCount(1, requests);
        Assert.IsFalse(saving.IsCompleted);
        StringAssert.Contains(requests[0], "password=a%26b");
        StringAssert.Contains(requests[0], "user=a%26b");
        acknowledged.SetResult(Response("ok"));
        var result = await saving;
        Assert.HasCount(2, requests);
        StringAssert.Contains(requests[1], "gethighscore.php");
        Assert.AreEqual(42, result[0].Score);
    }

    [TestMethod]
    [DataRow("broken")]
    [DataRow("100||abc")]
    [DataRow("100|2026-99-01|abc")]
    [DataRow("100|2026-11-20|")]
    public async Task InvalidDataReturnsFailureMessage(string data)
    {
        using var client = new HttpClient(new Handler(_ => Task.FromResult(Response(data))));
        var result = await new HighscoreServices(new TestSettings(), client).GetGlobalHighscores();
        Assert.AreEqual("failed to download global highscore list", string.Concat(result.Select(x => x.PlayerName)).TrimEnd());
    }

    [TestMethod]
    public async Task NetworkFailureIsHandledForBothReadAndSave()
    {
        using var client = new HttpClient(new Handler(_ => throw new HttpRequestException("offline")));
        var service = new HighscoreServices(new TestSettings(), client);
        var read = await service.GetGlobalHighscores();
        var save = await service.SaveGlobalHighscoreEntry(42, "abc");
        StringAssert.StartsWith(string.Concat(read.Select(x => x.PlayerName)), "failed to download");
        StringAssert.StartsWith(string.Concat(save.Select(x => x.PlayerName)), "failed to save");
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("a")]
    [DataRow("ab")]
    [DataRow("abc")]
    [DataRow("abcd")]
    public void SubtitlePreservesTextAndPadsLastName(string message)
    {
        var result = GlobalHighscoreList.CreateSubtitleMessage(message);
        Assert.AreEqual(message, string.Concat(result.Select(x => x.PlayerName)).TrimEnd());
        Assert.IsTrue(result.All(x => x.PlayerName.Length == 3));
    }
}
