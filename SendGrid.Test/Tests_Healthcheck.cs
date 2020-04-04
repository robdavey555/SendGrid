using Microsoft.AspNetCore.Mvc.Testing;
using SendGrid.Service;
using System.Threading.Tasks;
using Xunit;

namespace SendGrid.Test
{
    public class Tests_HealthCheck
    : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public Tests_HealthCheck(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact(DisplayName = "HealthCheck")]
        public async Task HealthCheck()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("health");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}