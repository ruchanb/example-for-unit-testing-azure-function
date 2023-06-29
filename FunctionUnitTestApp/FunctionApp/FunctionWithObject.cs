using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FunctionApp.Model;

namespace FunctionApp
{
    public class FunctionWithObject
    {
        private readonly ILogger _logger;

        public FunctionWithObject(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FunctionWithString>();
        }
        [Function("FunctionWithObject")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse();

            await response.WriteAsJsonAsync(new DummyModel
            {
                Name = "Azure",
                Position = 1
            });

            return response;
        }
    }
}
