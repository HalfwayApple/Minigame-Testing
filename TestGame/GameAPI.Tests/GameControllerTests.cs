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
            LogResponse(response);
        }

        [Fact]
        public async Task Equip_ReturnsOk_WithGameState()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/equip?index=0");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //Log
            LogResponse(response);
        }

        [Fact]
        public async Task Battle_ReturnsOk_WithGameState()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/battle");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //Log
            LogResponse(response);
        }

        [Fact]
        public async Task Attack_ReturnsOk_WithGameState()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // startar en Battle (måste göras för att Attack ska fungera)
            await client.GetAsync("/game/battle");

            // Act Attack efter att den startat Battle
            var response = await client.GetAsync("/game/attack");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _log.WriteLine($"status kod: {response.StatusCode}");

            // Log
            LogResponse(response);
        }

        private async void LogResponse(HttpResponseMessage response)
        {
            _log.WriteLine($"http status (borde va ok): {response.StatusCode}");
            var content = await response.Content.ReadAsStringAsync();
            _log.WriteLine($"objekt som svar: {content}");
        }
    }
}
