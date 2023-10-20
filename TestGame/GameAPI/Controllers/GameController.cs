using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameManager _gameManager;

        public GameController(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        [HttpGet("state")]
        public IActionResult GetGameState()
        {
            var gameState = _gameManager.GetGameState();
            return Ok(gameState);
        }

        [HttpGet("equip")]
        public IActionResult Equip(int index)
        {
            var gameState = _gameManager.Equip(index);
            return Ok(gameState);
        }
    }
}
