using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameAPI.Data.Characters;
using GameAPI.Data.Items.Equipment;

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
