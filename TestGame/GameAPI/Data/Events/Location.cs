using GameAPI.Data.Characters;

namespace GameAPI.Data.Events
{
	public class Location
	{
		public string Name { get; set; }
		public Hero Hero { get; set; }

		public Location(string name, Hero hero)
		{
			Name = name;
			Hero = hero;
		}
	}
}
