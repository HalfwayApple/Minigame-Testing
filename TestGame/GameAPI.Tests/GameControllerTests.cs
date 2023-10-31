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
        public async Task Equip_ReturnsBadRequest_WithOutOfRangeIndex()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/equip?index=9999"); // Assuming 9999 is out of range

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
        public async Task Attack_ReturnsBadRequest_WithoutBattleInitialization()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/attack");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            // Log
            LogResponse(response);
        }

        [Fact]
        public async Task Defend_ReturnsOk_WithGameState()
        {
            // Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            await client.GetAsync("/game/battle");

            // Act
            var response = await client.GetAsync("/game/defend");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Log
            LogResponse(response);
        }

        [Fact]
        public async Task Defend_ReturnsBadRequest_WithoutBattleInitialization()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/defend");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            // Log
            LogResponse(response);
        }

        [Fact]
        public async Task Dodge_ReturnsOk_WithGameState()
        {
            // Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            await client.GetAsync("/game/battle"); // starting a battle

            // Act
            var response = await client.GetAsync("/game/dodge");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Log
            LogResponse(response);
        }

        [Fact]
        public async Task Dodge_ReturnsBadRequest_WithoutBattleInitialization()
        {
            //Arrange
            using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/game/dodge");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

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
            _log.WriteLine($"http status : {response.StatusCode}");
            var content = await response.Content.ReadAsStringAsync();
            _log.WriteLine($"objekt som svar: {content}");
        }

        /*
        Motivering till varför vi inte kan testa mer (vad vi kan säga till Robert när vi redovisar)
        Huvud funktionerna testas men utöver det testas även hyptetiska scenarion som inte kan uppstå i spelet.
        T.ex. att köpa/sälja ett item som inte finns i shopen eller att sälja ett item som inte finns i inventory.
        Detta är inte möjligt att testa då det inte är möjligt att skicka in felaktiga värden till dessa funktioner.
        Vi testar även alla negativa scenarion där vi får fel status kod tillbaka för alla scenarion.
        utifrån ett metods perspektiv så testar vi alla scenarion som kan uppstå.
         */
    }
}
