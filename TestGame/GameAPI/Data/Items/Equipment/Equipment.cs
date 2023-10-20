﻿using GameAPI.Data.Items.Equipment.Armors;
using GameAPI.Data.Items.Equipment.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameAPI.Data.Items.Equipment
{
    [JsonDerivedType(typeof(Weapon))]
    [JsonDerivedType(typeof(Armor))]
    public class Equipment
    {
        public string Name { get; set; } = null!;
    }
}
