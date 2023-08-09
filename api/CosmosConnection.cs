using System;
using System.Net.Http;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Http;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace api;

public static class GraphApiConnection
{
    private static GraphQLHttpClient instance = null;

    public static GraphQLHttpClient Client(HttpRequest req)
    {
        if (instance is not null)
            return instance;

        instance = InitializeThing(req);
        return instance;
    }

    private static GraphQLHttpClient InitializeThing(HttpRequest req)
    {
        string ep = $"{req.Scheme}://{req.Host}{req.PathBase}/data-api/graphql";
    
        var graphQLHttpClientOptions = new GraphQLHttpClientOptions
        {
            EndPoint = new Uri(ep)
        };

        var httpClient = new HttpClient();

        var graphQLClient = new GraphQLHttpClient(graphQLHttpClientOptions, new SystemTextJsonSerializer(), httpClient);

        return graphQLClient;
    }
}
