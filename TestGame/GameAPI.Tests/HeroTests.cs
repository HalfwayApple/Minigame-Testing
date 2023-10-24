namespace GameAPI.Tests
{
    public class HeroTests
    {
        //använd _log för att logga i test explorer
        #region Constructor
        private readonly ITestOutputHelper _log;

        public HeroTests(ITestOutputHelper output)
        {
            _log = output;
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
            Hero hero = new Hero(1, "TestHero");

            // Act
            int maxHp = hero.CalcMaxHp(level);

            //Log
            _log.WriteLine($"Hero baseHp: {hero.MaxHP} Hero level: {level} Hero Hp: {maxHp}");

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
            Hero hero = new Hero(1, "TestHero");

            //Act
            int maxMana = hero.CalcMaxMana(level);

            //Log
            _log.WriteLine($"Level: {level} Mp: {maxMana} baseMp: {hero.MaxMana}");

            //Assert
            Assert.Equal(expectedMana, maxMana);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        public void CalcArmorValue_WithVariousArmors_ShouldReturnUpdatedArmorValues(int armorVal)
        {
            Hero hero = new Hero(1, "TestHero");
            Armor testArmor = new Armor
            {
                ArmorValue = armorVal
            };
            hero.EquipArmor(testArmor);

            // Act
            int armorValue = hero.CalcArmorValue();

            //Log
            _log.WriteLine($"Armor value med armor: {hero.ArmorValue} ArmorValue utan armor: {armorValue}");

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
            Hero hero = new Hero(1, "TestHero");
            hero.Xp = heroxp;

            // Act
            int level = hero.CalcLevel();

            //Log
            _log.WriteLine($"xp du får in: {heroxp} vilket level du är/blir: {level}");

            // Assert
            Assert.Equal(expectedLevel, level);
        }

        [Theory]
        // xp, expectedLevel, expectedMaxHp, expectedMaxMana, expectedAttackPower
        [InlineData(0, 1, 10, 5, 1)]
        [InlineData(5, 1, 10, 5, 1)]
        [InlineData(10, 1, 10, 5, 1)] // vid 10 xp är man fortfarande lvl 1, så denna är för lvl 1
        [InlineData(20, 2, 13, 7, 2)]
        [InlineData(100, 10, 37, 23, 10)]
        public void SetStats_GivenXp_ShouldSetCorrectStats(int xp, int expectedLevel, int expectedMaxHp, int expectedMaxMana, int expectedAttackPower)
        {
            // Arrange
            Hero hero = new Hero(1, "TestHero");
            hero.Xp = xp;

            // Act
            hero.SetStats();

            //Log
            _log.WriteLine($"level: {hero.Level} max hp: {hero.MaxHP} max mana: {hero.MaxMana} attackpower: {hero.AttackPower}");

            // Assert
            Assert.Equal(expectedLevel, hero.Level);
            Assert.Equal(expectedMaxHp, hero.MaxHP);
            Assert.Equal(expectedMaxMana, hero.MaxMana);
            Assert.Equal(expectedMaxHp, hero.CurrentHP);
            Assert.Equal(expectedMaxMana, hero.CurrentMana);
            Assert.Equal(expectedAttackPower, hero.AttackPower);
        }

        [Fact]
        public void CalcNormalDamage_WithWeaponEquipped_ShouldReturnCombinedDamage()
        {
            // Arrange
            Hero hero = new Hero(1, "TestHero");
            Weapon SulfuronHammer = new Weapon { AttackPower = 5 };
            hero.EquipWeapon(SulfuronHammer);
            int expectedDamage = hero.AttackPower + SulfuronHammer.AttackPower;

            // Act
            int damage = hero.CalcNormalDamage();

            //Log
            _log.WriteLine($"Basedamage: {hero.AttackPower} AttackPower med vapen som har 5 AP:{damage}");

            // Assert
            Assert.Equal(expectedDamage, damage);
        }

        [Fact]
        public void CalcNormalDamage_WithoutWeaponEquipped_ShouldReturnHeroAttackPower()
        {
            // Arrange
            Hero hero = new Hero(1, "TestHero");
            int expectedDamage = hero.AttackPower;

            // Act
            int damage = hero.CalcNormalDamage();

            //Log
            _log.WriteLine($"Basedamage: {hero.AttackPower} AttackPower utan vapen:{damage}");

            // Assert
            Assert.Equal(expectedDamage, damage);
        }

        //Positivt test scenario
        [Fact]
        public void LevelUpCheck_WhenXpIsEnough_ShouldLevelUp()
        {
            // Arrange
            Hero hero = new Hero(1, "TestHero");

            // Act: ökar xp så hero borde lvla upp (10 xp för att lvla upp)
            hero.Xp = 20; //Behövs 20 xp för lvl 2 (19 räcker inte)
            var oldLevel = hero.Level;
            hero.LevelUpCheck();

            //Log
            _log.WriteLine($"Ny level för hero: {hero.Level} Gammal level för hero: {oldLevel}");

            // Assert
            Assert.True(hero.Level > oldLevel, "Du lvlade inte upp");
        }

        //Negativt test scenario
        [Fact]
        public void LevelUpCheck_WhenXpIsNotEnough_ShouldNotLevelUp()
        {
            // Arrange
            Hero hero = new Hero(1, "TestHero");

            // Act öka inte xp, borde inte lvla upp
            var oldLevel = hero.Level;
            hero.LevelUpCheck();

            //Log
            _log.WriteLine($"Ny level för hero: {hero.Level} Gammal level för hero: {oldLevel}");

            // Assert
            Assert.Equal(oldLevel, hero.Level);
        }

        [Theory]
        // prevWeaponName, newWeaponName, expectedEquippedWeaponName, ExpectedBagCount
        [InlineData(null, "Sword", "Sword", 0)]
        [InlineData("Axe", "Sword", "Sword", 1)]
        [InlineData("Spear", "Bow", "Bow", 1)]
        public void EquipWeapon_ShouldHandleEquippedWeaponsCorrectly(string? prevWeaponName, string newWeaponName, string expectedEquippedWeaponName, int expectedBagCount)
        {
            // Arrange
            Hero hero = new Hero(1, "TestHero");

            if (prevWeaponName != null)
            {
                hero.EquipWeapon(new Weapon { Name = prevWeaponName });
            }

            Weapon newWeapon = new Weapon { Name = newWeaponName };
            // Act
            hero.EquipWeapon(newWeapon);

            //Log
            _log.WriteLine($"hero weapon: {hero.EquippedWeapon?.Name}");

            // Assert
            Assert.Equal(expectedEquippedWeaponName, hero.EquippedWeapon?.Name);
            Assert.Equal(expectedBagCount, hero.EquipmentInBag.Count);

            if (expectedBagCount > 0)
            {
                //Log
                _log.WriteLine($"antal vapen i bag: {hero.EquipmentInBag.Count}");
                Assert.Contains(hero.EquipmentInBag, equipment => equipment is Weapon w && w.Name == prevWeaponName);
            }
        }
    }
}