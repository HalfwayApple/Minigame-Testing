using GameAPI.Data.Characters;

namespace GameAPI.Data.Events
{
	public class Town : Location
	{
		public Town(string name, Hero hero) : base(name, hero)
		{
		}
	}
}
