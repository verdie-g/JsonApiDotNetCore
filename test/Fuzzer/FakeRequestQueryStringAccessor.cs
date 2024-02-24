using JsonApiDotNetCore.QueryStrings;
using Microsoft.AspNetCore.Http;

namespace Fuzzer;

internal sealed class FakeRequestQueryStringAccessor(IQueryCollection query) : IRequestQueryStringAccessor
{
    public IQueryCollection Query { get; } = query;
}
