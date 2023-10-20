using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace GameAPI.Tests
{
    public class GameControllerTests
    {
        #region Constructor
        private readonly ITestOutputHelper _log;

        public GameControllerTests(ITestOutputHelper output)
        {
            _log = output;
        }
        #endregion

        [Fact]
        public async Task GetGameState_ReturnsOk_WithGameState()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/state");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //Log
            _log.WriteLine($"Response HTTP Status Code: {response.StatusCode}");
            var content = await response.Content.ReadAsStringAsync();
            _log.WriteLine($"Response Content: {content}");
        }
    }
}
