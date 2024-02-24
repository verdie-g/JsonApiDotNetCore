using System.ComponentModel.Design;
using Fuzzer;
using Fuzzer.Resources;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Errors;
using JsonApiDotNetCore.Middleware;
using JsonApiDotNetCore.Queries.Parsing;
using JsonApiDotNetCore.QueryStrings;
using JsonApiDotNetCore.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging.Abstractions;

#pragma warning disable AV1210

SharpFuzz.Fuzzer.OutOfProcess.Run(input =>
{
    QueryCollection queryCollection;
    try
    {
        queryCollection = new QueryCollection(QueryHelpers.ParseQuery(input));
    }
    catch
    {
        return;
    }

    IJsonApiOptions options = new JsonApiOptions();

    FakeRequestQueryStringAccessor queryStringAccessor = new(queryCollection);

    IResourceGraph resourceGraph = new ResourceGraphBuilder(options, NullLoggerFactory.Instance)
        .Add<WebAccount, int>()
        .Add<BlogPost, int>()
        .Add<Comment, int>()
        .Build();

    var request = new JsonApiRequest
    {
        PrimaryResourceType = resourceGraph.GetResourceType(typeof(BlogPost)),
        IsCollection = true
    };

    var resourceFactory = new ResourceFactory(new ServiceContainer());

    IncludeParser includeParser = new(options);
    IncludeQueryStringParameterReader includeReader = new(includeParser, request, resourceGraph);

    QueryStringParameterScopeParser filterScopeParser = new();
    FilterParser filterValueParser = new(resourceFactory);
    FilterQueryStringParameterReader filterReader = new(filterScopeParser, filterValueParser, request, resourceGraph, options);

    QueryStringParameterScopeParser sortScopeParser = new();
    SortParser sortValueParser = new();
    SortQueryStringParameterReader sortReader = new(sortScopeParser, sortValueParser, request, resourceGraph);

    SparseFieldTypeParser sparseFieldSetScopeParser = new(resourceGraph);
    SparseFieldSetParser sparseFieldSetValueParser = new();
    SparseFieldSetQueryStringParameterReader sparseFieldSetReader = new(sparseFieldSetScopeParser, sparseFieldSetValueParser, request, resourceGraph);

    PaginationParser paginationParser = new();
    PaginationQueryStringParameterReader paginationReader = new(paginationParser, request, resourceGraph, options);

    IQueryStringParameterReader[] readers =
    [
        includeReader,
        filterReader,
        sortReader,
        sparseFieldSetReader,
        paginationReader
    ];

    QueryStringReader queryStringReader = new(options, queryStringAccessor, readers, NullLoggerFactory.Instance);

    try
    {
     queryStringReader.ReadAll(null);
    }
    catch (InvalidQueryStringParameterException)
    {
    }
});
