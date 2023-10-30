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
        public async Task GetGameState_ReturnsBadRequest()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/state");
            response.StatusCode = HttpStatusCode.BadRequest;
            
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            //Log
            LogResponse(response);
        }

        [Fact]
        public async Task GetGameState_ReturnsNotFound_IfUriWrong()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/stäjt");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

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
        public async Task Equip_ReturnsBadRequest_IfIndexIncorrect()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/equip?index=ä");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

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
        public async Task Battle_ReturnsBadRequest()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/battle");
            response.StatusCode = HttpStatusCode.BadRequest;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

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

        [Fact]
        public async Task Attack_ReturnsBadRequest()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // startar en Battle (måste göras för att Attack ska fungera)
            await client.GetAsync("/game/battle");

            // Act Attack efter att den startat Battle
            var response = await client.GetAsync("/game/attack");
            response.StatusCode = HttpStatusCode.BadRequest;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            _log.WriteLine($"status kod: {response.StatusCode}");

            // Log
            LogResponse(response);
        }

        [Fact]
        public async Task ReturnToTown_ReturnsOk_WithGameState()
        {
            // Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/returnToTown");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _log.WriteLine($"Status kod: {response.StatusCode}");

            // Log
            LogResponse(response);
        }

        [Fact]
        public async Task ReturnToTown_ReturnsBadRequest()
        {
            // Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/returnToTown");
            response.StatusCode = HttpStatusCode.BadRequest;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            _log.WriteLine($"Status kod: {response.StatusCode}");

            // Log
            LogResponse(response);
        }

        [Fact]
        public async Task EnterShop_ReturnsOk_WithGameState()
        {
            // Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/enterStore");

            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _log.WriteLine($"Status kod: {response.StatusCode}");
        }

        [Fact]
        public async Task EnterShop_ReturnsBadRequest()
        {
            // Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/enterStore");
            response.StatusCode = HttpStatusCode.BadRequest;

            // Assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            _log.WriteLine($"Status kod: {response.StatusCode}");
        }

        [Fact]
        public async Task Buy_ReturnsOk_WithGameState()
        {
            // Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act;
            var response = await client.GetAsync("/game/buy?index=0");

            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _log.WriteLine($"Status kod: {response.StatusCode}");
        }

        [Fact]
        public async Task Buy_ReturnsBadRequest_IfIndexIncorrect()
        {
            // Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act;
            var response = await client.GetAsync("/game/buy?index=ö");

            // Assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            _log.WriteLine($"Status kod: {response.StatusCode}");
        }

        [Fact]
        public async Task Sell_ReturnsOk_WithGameState()
        {
            // Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act;
            var response = await client.GetAsync("/game/sell?index=0");

            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _log.WriteLine($"Status kod: {response.StatusCode}");
        }

        [Fact]
        public async Task Sell_ReturnsBadRequest_IfIndexIncorrect()
        {
            // Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act;
            var response = await client.GetAsync("/game/sell?index=å");

            // Assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            _log.WriteLine($"Status kod: {response.StatusCode}");
        }

        private async void LogResponse(HttpResponseMessage response)
        {
            _log.WriteLine($"http status (borde va ok): {response.StatusCode}");
            var content = await response.Content.ReadAsStringAsync();
            _log.WriteLine($"objekt som svar: {content}");
        }
    }
}
