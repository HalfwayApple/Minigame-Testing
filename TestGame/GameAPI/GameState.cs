using GameAPI.Data.Characters;
using GameAPI.Data.Items.Equipment.Armors;
using GameAPI.Data.Items.Equipment.Weapons;

namespace GameAPI
{
    public class GameState
    {
        public Hero Hero { get; set; }
        public List<Enemy> EnemyList { get; set; } = new List<Enemy>();
        public GameState() 
        {
            // Create hero
            Hero = new(1, "Daniel");

            // Create some loot
            Weapon sword = new()
            {
                Name = "Sword",
                AttackPower = 2
            };
            Armor breastplate = new()
            {
                Name = "Breastplate",
                ArmorValue = 1
            };

            // Create an enemy
            Enemy enemy1 = new()
            {
                Id = 2,
                Name = "Slime",
                Description = "Japanese first enemy",
                Level = 1,
                AttackPower = 1,
                MaxHP = 5,
                MaxMana = 5,
                ArmorValue = 0,
                XpValue = 5,
            };
            enemy1.LootTable.Add(sword);
            enemy1.LootTable.Add(breastplate);

            Enemy enemy2 = new()
            {
                Id = 33,
                Name = "Rat",
                Description = "European first enemy",
                Level = 1,
                AttackPower = 2,
                MaxHP = 3,
                MaxMana = 3,
                ArmorValue = 0,
                XpValue = 5,
            };
            enemy2.LootTable.Add(breastplate);

            // Add enemy to enemylist
            EnemyList.Add(enemy1);
            EnemyList.Add(enemy2);

            Hero.EquipWeapon(sword);
        }
    }
}
