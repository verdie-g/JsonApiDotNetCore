using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace Fuzzer.Resources;

public sealed class BlogPost : Identifiable<int>
{
    [Attr]
    public string Caption { get; set; } = null!;

    [Attr]
    public string Url { get; set; } = null!;

    [HasOne]
    public WebAccount? Author { get; set; }

    [HasOne]
    public WebAccount? Reviewer { get; set; }

    [HasMany]
    public ISet<Comment> Comments { get; set; } = new HashSet<Comment>();
}
