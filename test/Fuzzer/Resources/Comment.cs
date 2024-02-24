using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace Fuzzer.Resources;

public sealed class Comment : Identifiable<int>
{
    [Attr]
    public string Text { get; set; } = null!;

    [Attr]
    public DateTime CreatedAt { get; set; }

    [Attr]
    public int NumStars { get; set; }

    [HasOne]
    public WebAccount? Author { get; set; }

    [HasOne]
    public BlogPost Parent { get; set; } = null!;
}
