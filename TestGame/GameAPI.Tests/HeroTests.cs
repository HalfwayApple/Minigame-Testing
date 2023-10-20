using GameAPI.Data.Items.Equipment.Armors;
using GameAPI.Data.Items.Equipment.Weapons;
using Xunit.Abstractions;

namespace GameAPI.Tests
{
    public class HeroTests
    {
        #region Constructor
        private readonly ITestOutputHelper _log;

        public HeroTests(ITestOutputHelper output)
        {
            _log = output;
        }
        #endregion

        [Theory]
        [InlineData(1, 10)]  // 7 baseHp + 1*3 = 10 : level 1 blir maxHp 10
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
        [InlineData(1, 5)] // 3 baseMana + 1*2 = 5 //level 1 blir baseMana 5
        [InlineData(2, 7)] // 3 baseMana + 1*2 = 5
        [InlineData(10, 23)] // 3 baseMana + 1*2 = 5
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
            _log.WriteLine($"Armor value without armor: {hero.ArmorValue} ArmorValue with armor: {armorValue}");

            // Assert
            Assert.Equal(testArmor.ArmorValue, armorValue);
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

        [Fact]
        public void LevelUpCheck_WhenXpIsEnough_ShouldLevelUp()
        {
            // Arrange
            Hero hero = new Hero(1, "TestHero");

            // Act: ökar xp så hero borde lvla upp (10 xp för att lvla upp)
            hero.Xp = 20; //Behövs 20 xp för lvl 2
            var oldLevel = hero.Level;
            hero.LevelUpCheck();

            //Log
            _log.WriteLine($"Ny level för hero: {hero.Level} Gammal level för hero: {oldLevel}");

            // Assert
            Assert.True(hero.Level > oldLevel, "Du lvlade inte upp");
        }

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

    }
}