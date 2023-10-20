namespace GameAPI.Tests
{
    public class HeroTests
    {
        [Fact]
        public void LevelUpCheck_WhenXpIsEnough_ShouldLevelUp()
        {
            // Arrange
            Hero hero = new Hero(1, "TestHero");

            // Act: ökar xp så hero borde lvla upp (10 xp för att lvla upp)
            hero.Xp = 10;
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