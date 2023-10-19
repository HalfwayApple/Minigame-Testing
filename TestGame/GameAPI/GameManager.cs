namespace GameAPI
{
    public class GameManager
    {
        private GameState _state;
        public GameManager() 
        {
            _state = new GameState();
        }
        public GameState GetGameState()
        {
            return _state;
        }
    }
}
