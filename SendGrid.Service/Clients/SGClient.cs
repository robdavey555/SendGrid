using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid.Service.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SendGrid.Service.Clients
{
    public class SGClient : ISendGridClient
    {
        private SendGridClient _myClient;
        private readonly IOptions<SendGridSettings> _sgConfig;
        private readonly HttpClient _httpClient;

        public string UrlPath { get; set; }
        public string Version { get; set; }
        public string MediaType { get; set; }

        public SGClient(HttpClient httpClient, IOptions<SendGridSettings> sgConfig)
        {
            _sgConfig = sgConfig;
            _httpClient = httpClient;
            _myClient = new SendGridClient(_httpClient, _sgConfig.Value.Key);
        }

        public AuthenticationHeaderValue AddAuthorization(KeyValuePair<string, string> header)
        {
            return _myClient.AddAuthorization(header);
        }

        public Task<Response> MakeRequest(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            return _myClient.MakeRequest(request, cancellationToken);
        }

        public Task<Response> RequestAsync(SendGridClient.Method method, string requestBody = null, string queryParams = null, string urlPath = null, CancellationToken cancellationToken = default)
        {
            return _myClient.RequestAsync(method, requestBody, queryParams, urlPath, cancellationToken);
        }

        public Task<Response> SendEmailAsync(SendGridMessage msg, CancellationToken cancellationToken = default)
        {
            return _myClient.SendEmailAsync(msg, cancellationToken);
        }
    }
}