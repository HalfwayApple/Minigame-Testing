using GameAPI.Data.Characters;

namespace GameAPI.Data.Events
{
	internal class Battle : Location
    {
        public Enemy Enemy { get; set; }
        public Battle(string name, Enemy enemy) : base(name)
        {
            Name = name;
            Enemy = enemy;
        }
    }
}
