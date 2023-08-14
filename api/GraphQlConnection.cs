using System;
using System.Net.Http;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

public static class GraphQlConnection
{
    private static Lazy<GraphQLHttpClient> lazyClient = new Lazy<GraphQLHttpClient>(InitializeGraphQlClient);
    public static GraphQLHttpClient GraphQlClient => lazyClient.Value;

    private static GraphQLHttpClient InitializeGraphQlClient()
    {
        string ep = Environment.GetEnvironmentVariable("dataApiEndpoint");

        var graphQLHttpClientOptions = new GraphQLHttpClientOptions
        {
            EndPoint = new Uri(ep)
        };

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("apiKey", Environment.GetEnvironmentVariable("dataApiKey"));

        var graphQLClient = new GraphQLHttpClient(graphQLHttpClientOptions, new SystemTextJsonSerializer(), httpClient);

        return graphQLClient;
    }
}