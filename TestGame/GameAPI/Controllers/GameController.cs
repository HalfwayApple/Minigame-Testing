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

        [HttpGet("returnToTown")]
		public IActionResult ReturnToTown()
		{
			var gameState = _gameManager.ReturnToTown();
			return Ok(gameState);
		}

		[HttpGet("enterStore")]
		public IActionResult EnterShop()
		{
			var gameState = _gameManager.EnterShop();
			return Ok(gameState);
		}


		[HttpGet("equip")]
        public IActionResult Equip(int index) // Index for which item in inventory is being equipped
        {
            var gameState = _gameManager.Equip(index);
            return Ok(gameState);
        }

		[HttpGet("battle")]
		public IActionResult Battle()
		{
			var gameState = _gameManager.StartFight();
			return Ok(gameState);
		}

		[HttpGet("attack")]
		public IActionResult Attack()
		{
			var gameState = _gameManager.Attack();
			return Ok(gameState);
		}

		[HttpGet("defend")]
		public IActionResult Defend()
		{
			var gameState = _gameManager.Defend();
			return Ok(gameState);
		}

		[HttpGet("dodge")]
		public IActionResult Dodge()
		{
			var gameState = _gameManager.Dodge();
			return Ok(gameState);
		}


		[HttpGet("buy")]
        public IActionResult Buy(int index)
        {
            var gameState = _gameManager.Buy(index);
            return Ok(gameState);
        }

		[HttpGet("sell")]
		public IActionResult Sell(int index)
		{
			var gameState = _gameManager.Sell(index);
			return Ok(gameState);
		}
	}
}
