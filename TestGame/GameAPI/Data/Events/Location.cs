using System.Text.Json.Serialization;

namespace GameAPI.Data.Events
{
	[JsonDerivedType(typeof(Battle))]
	[JsonDerivedType(typeof(Town))]
	public class Location
	{
		public string Name { get; set; }

		public Location(string name)
		{
			Name = name;
		}
	}
}
