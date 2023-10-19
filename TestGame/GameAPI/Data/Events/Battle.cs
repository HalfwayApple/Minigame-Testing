using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameAPI.Data.Characters;
using GameAPI.Data.Items.Equipment;

namespace GameAPI.Data.Events
{
    internal class Battle
    {
        Hero Hero { get; set; }
        Enemy Enemy { get; set; }
        public Battle(Hero hero, Enemy enemy)
        {
            Hero = hero;
            Enemy = enemy;
        }

        internal void StartBattle()
        {
            while (Enemy.CurrentHP > 0)
            {
                Console.WriteLine("Fight!");
                Console.WriteLine("Your HP: " + Hero.CurrentHP);
                Console.WriteLine("Monster HP: " + Enemy.CurrentHP);
                Console.ReadLine();
                Hero.Attack(Enemy);
                if (Enemy.CurrentHP > 0)
                {
                    Enemy.Attack(Hero);
                }
                if (Hero.CurrentHP <= 0)
                {
                    Console.WriteLine("You died!");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
            Console.WriteLine("You win!");
            EndBattle();
            Console.WriteLine("You get a " + Hero.EquipmentInBag[Hero.EquipmentInBag.Count-1].Name);
            Console.ReadLine();
            return;
        }
        internal void EndBattle()
        {
            Equipment? loot = Enemy.DropEquipment();
            if (loot != null)
            {
                Hero.EquipmentInBag.Add(loot);
            }
            Hero.Xp += Enemy.XpValue;
            Hero.LevelUpCheck();
        }
    }
}
