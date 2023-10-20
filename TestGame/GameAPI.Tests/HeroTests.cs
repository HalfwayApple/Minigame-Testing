using GameAPI.Data.Items.Equipment.Weapons;

namespace GameAPI.Tests
{
    public class HeroTests
    {

        [Theory]
        [InlineData(1, 10)]   // 7 baseHp + 1*3+7 = 10 : level 1 blir maxHp 10
        [InlineData(2, 13)]  // 7 baseHp + 3*2+7 = 13
        [InlineData(3, 16)]  // 7 baseHp + 3*3+7 = 16
        [InlineData(10, 37)] // 7 baseHp + 3*10+7 = 37
        public void CalcMaxHp_AtVariousLevels_ShouldReturnCorrectHp(int level, int expectedHp)
        {
            // Arrange
            Hero hero = new Hero(1, "TestHero");

            // Act
            int maxHp = hero.CalcMaxHp(level);

            // Assert
            Assert.Equal(expectedHp, maxHp);
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

            // Assert
            Assert.Equal(expectedDamage, damage);
        }

        [Fact]
        public void LevelUpCheck_WhenXpIsEnough_ShouldLevelUp()
        {
            // Arrange
            Hero hero = new Hero(1, "TestHero");

            // Act: ökar xp så hero borde lvla upp (10 xp för att lvla upp)
            hero.Xp = 11;
            var oldLevel = hero.Level;
            hero.LevelUpCheck();

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

            // Assert
            Assert.Equal(oldLevel, hero.Level);
        }

    }
}