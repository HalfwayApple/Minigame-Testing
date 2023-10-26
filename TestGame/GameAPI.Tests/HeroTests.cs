namespace GameAPI.Tests
{
    public class HeroTests
    {
        //anv�nd _log f�r att logga i test explorer
        #region Constructor
        private readonly ITestOutputHelper _log;
		private Hero _hero;

		public HeroTests(ITestOutputHelper output)
        {
            _log = output;
			_hero = new Hero(1, "Test Hero");
		}
        #endregion

        [Theory]
        [InlineData(1, 10)]  // 7 baseHp + 3*1 = 10 : level 1 blir maxHp 10
        [InlineData(2, 13)]  // 7 baseHp + 3*2 = 13
        [InlineData(3, 16)]  // 7 baseHp + 3*3 = 16
        [InlineData(10, 37)] // 7 baseHp + 3*10 = 37
        public void CalcMaxHp_AtVariousLevels_ShouldReturnCorrectHp(int level, int expectedHp)
        {
            // Arrange
            // Act
            int maxHp = _hero.CalcMaxHp(level);

            //Log
            _log.WriteLine($"Hero baseHp: {_hero.MaxHP} Hero level: {level} Hero Hp: {maxHp}");

            // Assert
            Assert.Equal(expectedHp, maxHp);
        }

        [Theory]
        [InlineData(1, 5)] // 3 baseMana + 2*1 = 5 //level 1 blir baseMana 5
        [InlineData(2, 7)] // 3 baseMana + 2*2 = 7
        [InlineData(10, 23)] // 3 baseMana + 2*10 = 23
        public void CalcMaxMana_AtVariousLevels_ShouldReturnCorrectMp(int level, int expectedMana)
        {
            //Arrange
            //Act
            int maxMana = _hero.CalcMaxMana(level);

            //Log
            _log.WriteLine($"Level: {level} Mp: {maxMana} baseMp: {_hero.MaxMana}");

            //Assert
            Assert.Equal(expectedMana, maxMana);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        public void CalcArmorValue_WithVariousArmors_ShouldReturnUpdatedArmorValues(int armorVal)
        {
			//Arrange
			Armor testArmor = new Armor
            {
                ArmorValue = armorVal
            };
            _hero.EquipArmor(testArmor);

            // Act
            int armorValue = _hero.CalcArmorValue();

            //Log
            _log.WriteLine($"Armor value med armor: {_hero.ArmorValue} ArmorValue utan armor: {armorValue}");

            // Assert
            Assert.Equal(testArmor.ArmorValue, armorValue);
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(20, 2)]
        [InlineData(100, 10)]
        public void CalcLevel_XpLessThan10_ShouldReturnLevelOne(int heroxp, int expectedLevel)
        {
            // Arrange
            _hero.Xp = heroxp;

            // Act
            int level = _hero.CalcLevel();

            //Log
            _log.WriteLine($"xp du f�r in: {heroxp} vilket level du �r/blir: {level}");

            // Assert
            Assert.Equal(expectedLevel, level);
        }

        [Theory]
        // xp, expectedLevel, expectedMaxHp, expectedMaxMana, expectedAttackPower
        [InlineData(0, 1, 10, 5, 1)]
        [InlineData(5, 1, 10, 5, 1)]
        [InlineData(10, 1, 10, 5, 1)] // vid 10 xp �r man fortfarande lvl 1, s� denna �r f�r lvl 1
        [InlineData(20, 2, 13, 7, 2)]
        [InlineData(100, 10, 37, 23, 10)]
        public void SetStats_GivenXp_ShouldSetCorrectStats(int xp, int expectedLevel, int expectedMaxHp, int expectedMaxMana, int expectedAttackPower)
        {
            // Arrange
            _hero.Xp = xp;

            // Act
            _hero.SetStats();

            //Log
            _log.WriteLine($"level: {_hero.Level} max hp: {_hero.MaxHP} max mana: {_hero.MaxMana} attackpower: {_hero.AttackPower}");

            // Assert
            Assert.Equal(expectedLevel, _hero.Level);
            Assert.Equal(expectedMaxHp, _hero.MaxHP);
            Assert.Equal(expectedMaxMana, _hero.MaxMana);
            Assert.Equal(expectedMaxHp, _hero.CurrentHP);
            Assert.Equal(expectedMaxMana, _hero.CurrentMana);
            Assert.Equal(expectedAttackPower, _hero.AttackPower);
        }

        [Fact]
        public void CalcNormalDamage_WithWeaponEquipped_ShouldReturnCombinedDamage()
        {
            // Arrange
            Weapon SulfuronHammer = new Weapon { AttackPower = 5 };
            _hero.EquipWeapon(SulfuronHammer);
            int expectedDamage = _hero.Level + SulfuronHammer.AttackPower;

            // Act
            int damage = _hero.CalcNormalDamage();

            //Log
            _log.WriteLine($"Basedamage: {_hero.Level} AttackPower med vapen som har 5 AP:{damage}");

            // Assert
            Assert.Equal(expectedDamage, damage);
        }

        [Fact]
        public void CalcNormalDamage_WithoutWeaponEquipped_ShouldReturnHeroAttackPower()
        {
            // Arrange
            int expectedDamage = _hero.AttackPower;

            // Act
            int damage = _hero.CalcNormalDamage();

            //Log
            _log.WriteLine($"Basedamage: {_hero.AttackPower} AttackPower utan vapen:{damage}");

            // Assert
            Assert.Equal(expectedDamage, damage);
        }

        //Positivt test scenario
        [Fact]
        public void LevelUpCheck_WhenXpIsEnough_ShouldLevelUp()
        {
            // Arrange
            // Act: �kar xp s� _hero borde lvla upp (10 xp f�r att lvla upp)
            _hero.Xp = 20; //Beh�vs 20 xp f�r lvl 2 (19 r�cker inte)
            var oldLevel = _hero.Level;
            _hero.LevelUpCheck();

            //Log
            _log.WriteLine($"Ny level f�r _hero: {_hero.Level} Gammal level f�r _hero: {oldLevel}");

            // Assert
            Assert.True(_hero.Level > oldLevel, "Du lvlade inte upp");
        }

        //Negativt test scenario
        [Fact]
        public void LevelUpCheck_WhenXpIsNotEnough_ShouldNotLevelUp()
        {
            // Arrange
            // Act �ka inte xp, borde inte lvla upp
            var oldLevel = _hero.Level;
            _hero.LevelUpCheck();

            //Log
            _log.WriteLine($"Ny level f�r _hero: {_hero.Level} Gammal level f�r _hero: {oldLevel}");

            // Assert
            Assert.Equal(oldLevel, _hero.Level);
        }

        [Fact]
        public void EquipWeapon_ShouldEquipWeapon_WhenNoWeaponEquipped()
        {
            // Arrange
            _hero.EquippedWeapon = null;
            Weapon newWeapon = new Weapon { Name = "TestWeapon" };
            _hero.EquipmentInBag.Add(newWeapon);
            int amountInBagAtStart = _hero.EquipmentInBag.Count();

            // Act
            _hero.EquipWeapon(newWeapon);

            //Log
            _log.WriteLine($"_hero weapon: {_hero.EquippedWeapon?.Name}");

            // Assert
            Assert.Equal(_hero.EquippedWeapon?.Name, newWeapon.Name);
            Assert.Equal(amountInBagAtStart-1, _hero.EquipmentInBag.Count);
        }

        [Fact]
        public void EquipWeapon_ShouldSwapWeapons_WhenWeaponEquipped()
        {
            // Arrange
            Weapon equippedWeapon = new Weapon { Name = "InitiallyEquippedWeapon" };
            Weapon weaponToBeEquipped = new Weapon { Name = "WeaponInitiallyInBag" };
            _hero.EquippedWeapon = equippedWeapon;
            _hero.EquipmentInBag.Add(weaponToBeEquipped);
            int amountInBagAtStart = _hero.EquipmentInBag.Count();

            // Act
            _hero.EquipWeapon(weaponToBeEquipped);

            //Log
            _log.WriteLine($"_hero weapon: {_hero.EquippedWeapon?.Name}");

            // Assert
            Assert.Equal(weaponToBeEquipped, _hero.EquippedWeapon);
            Assert.Equal(amountInBagAtStart, _hero.EquipmentInBag.Count);
            Assert.Contains(_hero.EquipmentInBag, equipment => equipment is Weapon w && w.Name == equippedWeapon.Name);
            Assert.DoesNotContain(_hero.EquipmentInBag, equipment => equipment is Weapon w && w.Name == weaponToBeEquipped.Name);
        }

        [Fact]
        public void EquipArmor_ShouldEquipArmor_WhenNoArmorEquipped()
        {
            // Arrange
            _hero.EquippedWeapon = null;
            Armor newArmor = new Armor { Name = "TestArmor" };
            _hero.EquipmentInBag.Add(newArmor);
            int amountInBagAtStart = _hero.EquipmentInBag.Count();

            // Act
            _hero.EquipArmor(newArmor);

            //Log
            _log.WriteLine($"_hero armor: {_hero.EquippedArmor?.Name}");

            // Assert
            Assert.Equal(_hero.EquippedArmor, newArmor);
            Assert.Equal(amountInBagAtStart - 1, _hero.EquipmentInBag.Count);
        }

        [Fact]
        public void EquipArmor_ShouldSwapArmors_WhenArmorEquipped()
        {
            // Arrange
            Armor equippedArmor = new Armor { Name = "InitiallyEquippedArmor" };
            Armor armorToBeEquipped = new Armor { Name = "ArmorInitiallyInBag" };
            _hero.EquippedArmor = equippedArmor;
            _hero.EquipmentInBag.Add(armorToBeEquipped);
            int amountInBagAtStart = _hero.EquipmentInBag.Count();

            // Act
            _hero.EquipArmor(armorToBeEquipped);

            //Log
            _log.WriteLine($"_hero weapon: {_hero.EquippedWeapon?.Name}");

            // Assert
            Assert.Equal(armorToBeEquipped, _hero.EquippedArmor);
            Assert.Equal(amountInBagAtStart, _hero.EquipmentInBag.Count);
            Assert.Contains(_hero.EquipmentInBag, equipment => equipment is Armor a && a.Name == equippedArmor.Name);
            Assert.DoesNotContain(_hero.EquipmentInBag, equipment => equipment is Armor a && a.Name == armorToBeEquipped.Name);
        }

		[Fact]
		public void EquipWeapon_WhenWeaponIsNull_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _hero.EquipWeapon(null));
		}

		[Fact]
		public void EquipArmor_WhenArmorIsNull_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _hero.EquipArmor(null));
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		public void CalcMaxHp_WhenLevelIsNegativeOrZero_ThrowsArgumentOutOfRangeException(int level)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => _hero.CalcMaxHp(level));
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		public void CalcMaxMana_WhenLevelIsNegativeOrZero_ThrowsArgumentOutOfRangeException(int level)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => _hero.CalcMaxMana(level));
		}

		[Fact]
		public void CalcLevel_WhenXpIsLessThanTen_ReturnsOne()
		{
			_hero.Xp = 5;
			var result = _hero.CalcLevel();
			Assert.Equal(1, result);
		}
	}
}