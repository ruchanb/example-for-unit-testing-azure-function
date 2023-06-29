using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using System.Security.Claims;

namespace FunctionUnitTest.Mock
{
    public sealed class MockHttpRequestData : HttpRequestData
    {
        private readonly FunctionContext Context;
        public MockHttpRequestData(FunctionContext context) : base(context)
        {
            this.Context = context;
        }

        public override HttpResponseData CreateResponse()
        {
            return new MockHttpResponseData(Context);
        }

        public override Stream Body { get; }
        public override HttpHeadersCollection Headers { get; }
        public override IReadOnlyCollection<IHttpCookie> Cookies { get; }
        public override Uri Url { get; }
        public override IEnumerable<ClaimsIdentity> Identities { get; }
        public override string Method { get; }
    }
}
