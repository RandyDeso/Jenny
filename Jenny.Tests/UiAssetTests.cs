namespace Jenny.Tests;

public sealed class UiAssetTests
{
    [Fact]
    public void IndexHtml_ContainsChatbotShell()
    {
        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../Jenny.Web/wwwroot/index.html"));
        var content = File.ReadAllText(path);

        Assert.Contains("Jenny Travel Assistant", content);
        Assert.Contains("Chat with Jenny", content);
        Assert.Contains("chat-form", content);
        Assert.Contains("app.js", content);
    }
}
