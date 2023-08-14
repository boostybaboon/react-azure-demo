using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ReactAzureDemoApi.Function;

public static class message
{
    [FunctionName("message")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        var awaitable = await Task.Run<object>(() => new {text="Hello world, I'm text served from a c# back end!"});
        return new JsonResult(awaitable);
    }
}
