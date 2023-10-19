using GameAPI.Data.Characters;

namespace GameAPI
{
    public class GameState
    {
        public Hero Hero { get; set; }
        public List<Enemy> EnemyList { get; set; } = new List<Enemy>();

    }
}
