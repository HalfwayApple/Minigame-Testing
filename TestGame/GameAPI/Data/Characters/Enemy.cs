using GameAPI.Data.Items.Equipment;

namespace GameAPI.Data.Characters
{
    public class Enemy : Character, ICloneable
    {
        public Enemy()
        {
            // does not work, fix later
            CurrentHP = MaxHP;
            CurrentMana = MaxMana;
        }
        public int XpValue { get; set; }
        public List<Equipment> LootTable { get; set; } = new List<Equipment>();

        /// <summary>
        /// Creates a clone of the enemy object
        /// </summary>
        /// <returns>Clone of the object</returns>
		public object Clone()
		{
            return MemberwiseClone();
		}

        /// <summary>
        /// Checks for Equipment in enemy loot table, picks one at random, and returns it (or null for empty loot tables)
        /// </summary>
        /// <returns>Equipment or null</returns>
		public Equipment? DropEquipment()
        {
            if (LootTable == null || LootTable.Count == 0)
            {
                return null;
            }
            Random rng = new Random();
            int randomNumber = rng.Next(0, LootTable.Count);
            Equipment loot = LootTable[randomNumber];

            return loot;
        }
    }
}
