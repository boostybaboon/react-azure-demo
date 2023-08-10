using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using GraphQL;
using System.Net.Http;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
namespace ReactAzureDemoApi.Function
{
    public static class message3
    {
        [FunctionName("message3")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var movieRequest = new GraphQLRequest
            {
                Query = @"
                    {
                        people {
                            items {
                                id
                                Name
                            }
                        }
                    }
                "
            };

            try
            {
                string ep = $"{req.Scheme}://{req.Host}{req.PathBase}/data-api/graphql";

                log.LogInformation($"end point: {ep}");
                log.LogInformation($"from: Scheme {req.Scheme}, Host {req.Host}, PathBase {req.PathBase}");

                var graphQLHttpClientOptions = new GraphQLHttpClientOptions
                {
                    EndPoint = new Uri(ep)
                };

                log.LogInformation($"made client options");

                var httpClient = new HttpClient();

                log.LogInformation($"made httpClient");

                var graphQLClient = new GraphQLHttpClient(graphQLHttpClientOptions, new SystemTextJsonSerializer(), httpClient);

                log.LogInformation($"made graphQLClient");

                //var graphQLClient = GraphApiConnection.Client(req);

                var graphQLResponse = await graphQLClient.SendQueryAsync<PeopleResponse>(movieRequest);

                log.LogInformation($"got graphQLResponse");

                foreach (var item in graphQLResponse.Data.people.items)
                    log.LogInformation("{0}: {1}", item.id, item.Name);

                return new JsonResult(graphQLResponse);
            }
            catch (Exception ex)
            {
                log.LogError($"Error doing graphql stuff: {ex.Message}");
                
                var awaitable = await Task.Run<object>(() => new { text = ex.Message });

                return new JsonResult(awaitable);
            }

        }
    }

    internal class PeopleResponse
    {
        public People people { get; set; }
    }

    public class People
    {
        public List<Person> items { get; set; }
    }

    public class Person
    {
        public string id { get; set; }
        public string Name { get; set; }
    }
}
