using GameAPI.Data.Items.Equipment;

namespace GameAPI.Data.Characters
{
    public class Enemy : Character, ICloneable
    {
        public Enemy()
        {
            // does not work
            CurrentHP = MaxHP;
            CurrentMana = MaxMana;
        }
        public int XpValue { get; set; }
        public List<Equipment> LootTable { get; set; } = new List<Equipment>();
        override public int CalcNormalDamage()
        {
            int damage = AttackPower;
            return damage;
        }

		public object Clone()
		{
            return MemberwiseClone();
		}

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
