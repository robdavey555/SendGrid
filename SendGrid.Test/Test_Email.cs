using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SendGrid.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SendGrid.Test
{
    public class Test_Email
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public Test_Email(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact(DisplayName = "Email")]
        public async Task Email_BadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            Dictionary<string, string> Substitutions = new Dictionary<string, string>();
            Substitutions.Add("sub", "me");
            var request = new StringContent(JsonConvert.SerializeObject(Substitutions), Encoding.UTF8, "application/json");
            string email = "joeblogs@email.com";
            string templateid = "897890y8h9sdy78dyf";

            // Act
            var response = await client.PostAsync($"api/email/SendEmail?emailAddress={email}&templateid={templateid}", request);

            // Assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }
    }
}