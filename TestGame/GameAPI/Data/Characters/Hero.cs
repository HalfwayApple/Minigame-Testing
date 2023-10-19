using GameAPI.Data.Items.Equipment;
using GameAPI.Data.Items.Equipment.Armor;
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

        public int Xp { get; set; } = 0;
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
        internal int CalcMaxHp(int level)
        {
            int baseHp = 7;
            int leveledHp = 3 * level;
            return baseHp + leveledHp;
        }
        internal int CalcMaxMana(int level)
        {
            int baseHp = 3;
            int leveledHp = 2 * level;
            return baseHp + leveledHp;
        }
        public int CalcArmorValue()
        {
            if (EquippedArmor != null)
            {
                return EquippedArmor.ArmorValue;
            }
            else { return 0; }
        }
        public int CalcLevel()
        {
            int level = Xp / 10;
            if (level < 1)
            {
                level = 1;
            }
            return level;
        }
        public void SetStats()
        {
            Level = CalcLevel();
            MaxHP = CalcMaxHp(Level);
            MaxMana = CalcMaxMana(Level);
            CurrentHP = MaxHP;
            CurrentMana = MaxMana;
            AttackPower = Level;
        }
        public void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                EquipmentInBag.Add(EquippedWeapon);
            }
            EquippedWeapon = weapon;
        }
        public void EquipArmor(Armor armor)
        {
            if (EquippedArmor != null)
            {
                EquipmentInBag.Add(EquippedArmor);
            }
            EquippedArmor = armor;
        }
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
