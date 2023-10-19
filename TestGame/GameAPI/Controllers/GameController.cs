using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private GameManager gameManager;

        public GameController()
        {
            gameManager = new GameManager();
        }

        [HttpGet("state")]
        public IActionResult GetGameState()
        {
            var gameState = gameManager.GetGameState();
            return Ok(gameState);
        }
    }
}