using Azure.Core.Serialization;
using FunctionUnitTest.Mock;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FunctionApp.Model;
using FunctionApp;

namespace FunctionUnitTest
{
    public class FunctionWithStringTest
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly Mock<FunctionContext> functionContext;
        public FunctionWithStringTest()
        {
            loggerFactory = new NullLoggerFactory();
            functionContext = new Mock<FunctionContext>();
        }
        private string GetBodyMessageFromResponse(HttpResponseData response)
        {
            string body = string.Empty;
            using (var reader = new StreamReader(response.Body))
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                body = reader.ReadToEnd();
            }
            return body;
        }

        #region " test "
        [Fact]
        public void Run_return_ok_with_message()
        {
            // Setup
            MockHttpRequestData requestData = new MockHttpRequestData(functionContext.Object);
            var function = new FunctionWithString(loggerFactory);

            // Execute
            var response = function.Run(requestData);

            // Validate
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            Assert.Equal(expectedStatusCode, response.StatusCode);

            var expectedmessage = "Welcome to Azure Functions!";
            var receivedMessage = GetBodyMessageFromResponse(response);
            Assert.Equal(expectedmessage, receivedMessage);
        }
        #endregion " test "
    }
}