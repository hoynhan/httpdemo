using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace MySessionIntergrationTest
{
    public class SessionIntergrationTest: IClassFixture<WebApplicationFactory<HttpDemo.Program>>
    {
        private readonly HttpClient _factory;

        public SessionIntergrationTest(WebApplicationFactory<HttpDemo.Program> factory)
        {
            this._factory = factory.CreateClient();
        }

        [Fact]
        public async Task Call_TestGetSession_Return_Ok_Async()
        {
            var response = await _factory.GetAsync("/Test/TestGetSession");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Call_Set_And_GetSessionValueAsync_Return_Ok_Async()
        {
            string randomValue = Guid.NewGuid().ToString();
            await _factory.GetAsync($"/Test/SetSessionValue?key=TEST-Key&value={randomValue}");

            var response = await _factory.GetAsync("/Test/GetSessionValue?key=TEST-Key");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(randomValue, responseString);
        }
    }
}
