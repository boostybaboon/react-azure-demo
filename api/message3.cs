using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using GraphQL;

namespace ReactAzureDemoApi.Function;

public static class message3
{
    private static readonly GraphQLRequest peopleRequest = new(){
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

    [FunctionName("message3")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        //try
        //{
            var graphQLResponse = await GraphQlConnection.GraphQlClient.SendQueryAsync<PeopleResponse>(peopleRequest);

            return new JsonResult(graphQLResponse);
        //}
        //catch (Exception ex)
        //{
        //    log.LogError($"Error doing graphql stuff: {ex.Message}");
        //
        //    var awaitable = await Task.Run<object>(() =>
        //        new { text = ex.Message });
        //
        //    return new JsonResult(awaitable);
        //}
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
