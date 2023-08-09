using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using GraphQL;
using api;

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

            var graphQLClient = GraphApiConnection.Client(req);
            var graphQLResponse = await graphQLClient.SendQueryAsync<PeopleResponse>(movieRequest);

            foreach (var item in graphQLResponse.Data.people.items)
                Console.WriteLine("{0}: {1}", item.id, item.Name);

            var awaitable = await Task.Run<object>(() => new { text = "Hello world, I'm text served from a c# back end!" });

            return new JsonResult(awaitable);
        }
    }

    internal class PeopleResponse
    {
        public People people {get; set;}
    }

    public class People
    {
        public List<Person> items {get; set;}
    }

    public class Person
    {
        public string id {get; set;}
        public string Name {get; set;}
    }
}