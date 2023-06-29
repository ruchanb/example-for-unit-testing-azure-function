using Azure.Core.Serialization;
using FunctionApp.Model;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FunctionUnitTest.Mock;
using FunctionApp;
using System.Net;

namespace FunctionUnitTest
{

    public class FunctionWithObjectTest
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly Mock<FunctionContext> functionContext;
        public FunctionWithObjectTest()
        {
            IOptions<WorkerOptions> workerOptions = Options.Create<WorkerOptions>(new WorkerOptions() { Serializer = new JsonObjectSerializer() });
            var service = new ServiceCollection();
            service.AddSingleton<IOptions<WorkerOptions>>(workerOptions);
            functionContext = new Mock<FunctionContext>();
            // HttpRequestData uses InstanceServices internally while using WriteAsJsonAsync
            // https://github.com/Azure/azure-functions-dotnet-worker/blob/main/src/DotNetWorker.Core/Http/HttpResponseDataExtensions.cs
            functionContext.Setup(m => m.InstanceServices).Returns(service.BuildServiceProvider());
            loggerFactory = new NullLoggerFactory();
        }
        private DummyModel GetBodyObjectFromResponse(HttpResponseData response)
        {
            string body = string.Empty;
            using (var reader = new StreamReader(response.Body))
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                body = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<DummyModel>(body);
        }

        #region " test "
        [Fact]
        public async Task Run_return_ok_with_object()
        {
            // Setup
            MockHttpRequestData requestData = new MockHttpRequestData(functionContext.Object);
            var function = new FunctionWithObject(loggerFactory);

            // Execute
            var response = await function.Run(requestData);

            // Validate
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            Assert.Equal(expectedStatusCode, response.StatusCode);

            DummyModel expectedObject = new DummyModel() { Name = "Azure", Position = 1 };
            var receivedObject = GetBodyObjectFromResponse(response);
            Assert.Equivalent(expectedObject, receivedObject);
        }
        #endregion " test "
    }
}
