namespace GameAPI.Tests
{
    public class CharacterTests
    {
        #region Constructor
        private readonly ITestOutputHelper _log;

        public CharacterTests(ITestOutputHelper output)
        {
            _log = output;
        }
        #endregion
        [Fact]
        public void Attack_ShouldReduceEnemyCurrentHPByCorrectAmount()
        {
            // Arrange
            var attackerMock = new Mock<Character>();
            var enemyMock = new Mock<Character>();

            attackerMock.Setup(m => m.CalcNormalDamage()).Returns(40); //40 ap
            attackerMock.Object.AttackPower = 40;
            enemyMock.Object.ArmorValue = 10;
            enemyMock.Object.CurrentHP = 100;

            // Act
            attackerMock.Object.Attack(enemyMock.Object);

            //Log
            _log.WriteLine($"din attackpower till en början: {attackerMock.Object.AttackPower}");
            _log.WriteLine($"fiendes armorvalue: {enemyMock.Object.ArmorValue}");
            _log.WriteLine($"fiendes currenthp: 100");
            _log.WriteLine($"hur mycket damage du gör på fiende: {attackerMock.Object.CalcNormalDamage()}");
            _log.WriteLine($"fiendes hp efter du attackerat: {enemyMock.Object.CurrentHP} 100 + 10armor - 40 damage = 70");

            // Assert
            Assert.Equal(70, enemyMock.Object.CurrentHP); // 100 - (40 - 10) = 70 fiende borde ha 70hp
        }
    }
}
