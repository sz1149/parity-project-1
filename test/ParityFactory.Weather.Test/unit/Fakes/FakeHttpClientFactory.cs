using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ParityFactory.Weather.Test.unit.Fakes
{
    public class FakeHttpClientFactory : IHttpClientFactory
    {
        public string ExpectedResponse { get; set; }
        
        public HttpStatusCode? ExpectedResponseCode { get; set; }

        public HttpClient CreateClient(string name)
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = ExpectedResponseCode ?? HttpStatusCode.OK,
                Content = new StringContent(ExpectedResponse, Encoding.UTF8,"application/json")
            });
            return new HttpClient(fakeHttpMessageHandler);
        }
    }
}