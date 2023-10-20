using GameAPI.Data.Items.Equipment;
using GameAPI.Data.Items.Equipment.Armors;
using GameAPI.Data.Items.Equipment.Weapons;

namespace GameAPI.Data.Characters
{
    public class Hero : Character
    {
        public Hero(int id, string name)
        {
            Id = id;
            Name = name;

            SetStats();
            ArmorValue = CalcArmorValue();
        }

        public int Xp { get; set; } = 10;
        public Weapon? EquippedWeapon { get; set; } = null;
        public Armor? EquippedArmor { get; set; } = null;
        public List<Equipment> EquipmentInBag { get; set; } = new List<Equipment>();
        override public int CalcNormalDamage()
        {
            if (EquippedWeapon != null)
            {
                int damage = AttackPower + EquippedWeapon.AttackPower;
                return damage;
            }
            else
            {
                int damage = AttackPower;
                return damage;
            }
        }

		#region Calculations
        /// <summary>
        /// Calculate Hero MaxHp based on level
        /// </summary>
        /// <param name="level"></param>
        /// <returns>Integer of Max Hp for specified level</returns>
		internal int CalcMaxHp(int level)
        {
            int baseHp = 7;
            int leveledHp = 3 * level;
            return baseHp + leveledHp;
        }
		/// <summary>
		/// Calculate Hero MaxMana based on level
		/// </summary>
		/// <param name="level"></param>
		/// <returns>Integer of Max Mana for specified level</returns>
		internal int CalcMaxMana(int level)
        {
            int baseMana = 3;
            int leveledMana = 2 * level;
            return baseMana + leveledMana;
        }
        /// <summary>
        /// Calculate armor value based on equipped armor
        /// </summary>
        /// <returns>Armor value as Int</returns>
        public int CalcArmorValue()
        {
            if (EquippedArmor != null)
            {
                return EquippedArmor.ArmorValue;
            }
            else { return 0; }
        }

        /// <summary>
        /// Calculate level based on xp/level formula
        /// </summary>
        /// <returns>Currene level</returns>
        public int CalcLevel()
        {
            int level = Xp / 10;
            if (level < 1)
            {
                level = 1;
            }
            return level;
        }

		#endregion

        /// <summary>
        /// Sets hero stats (Level, MaxHP, MaxMana, AttackPower according to currect formulas, and sets current hp/mana to max
        /// </summary>
		public void SetStats()
        {
            Level = CalcLevel();
            MaxHP = CalcMaxHp(Level);
            MaxMana = CalcMaxMana(Level);
            CurrentHP = MaxHP;
            CurrentMana = MaxMana;
            AttackPower = Level;
        }

        /// <summary>
        /// Equips input weapon. If another weapon is already equipped, put that one in the bag
        /// </summary>
        /// <param name="weapon"></param>
        public void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                EquipmentInBag.Add(EquippedWeapon);
            }
            EquippedWeapon = weapon;
            EquipmentInBag.Remove(weapon);
        }

		/// <summary>
		/// Equips input armor. If another armor is already equipped, put that one in the bag
		/// </summary>
		/// <param name="armor"></param>
		public void EquipArmor(Armor armor)
        {
            if (EquippedArmor != null)
            {
                EquipmentInBag.Add(EquippedArmor);
            }
            EquippedArmor = armor;
			EquipmentInBag.Remove(armor);
		}

        /// <summary>
        /// Uses CalcLevel() to check if Hero should be higher level than currently. If so, sets the new level and recalculates the stats using SetStats()
        /// </summary>
        public void LevelUpCheck()
        {
            int levelCheck = CalcLevel();
            if (levelCheck > Level) 
            {
                Console.WriteLine("Level up!");
                Level = levelCheck;
                SetStats();
            }
        }
    }
}
