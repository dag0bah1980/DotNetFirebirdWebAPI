using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace JSONFirebirdWebServiceTest.HTTPResponses
{

    public class TextHeaderResponses : IHttpActionResult
    {
        public string Message { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public TextHeaderResponses(string message, HttpRequestMessage request)
        {
            if (message == null)
            {
                throw new ArgumentNullException("value");
            }

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            Message = message;
            Request = request;
        }

        

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(Message),
                RequestMessage = Request
            };
            return Task.FromResult(response);
        }

        public HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            response.Content = new StringContent(Message); // Put the message in the response body (text/plain content).
            response.RequestMessage = Request;
            return response;
        }
    }

    public static class ApiControllerExtensions
    {
        public static TextHeaderResponses NotFound(this ApiController controller, string message)
        {
            return new TextHeaderResponses(message, controller.Request);
        }
    }
}

