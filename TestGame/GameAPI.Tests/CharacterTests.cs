namespace GameAPI.Tests
{
    public class TestCharacter : Character
    {
        //på grund av att Character är abstract så kan vi inte skapa en instans av den, därför skapar vi en ny klass som ärver från Character
        //för att skapa upp  metoderna som vi kan testa
        private int? _testDamage;
        public void TestCalcNormalDamage(int value)
        {
            _testDamage = value;
        }

        public virtual int CalcNormalDamage()
        {
            return _testDamage ?? base.CalcNormalDamage();
        }
    }

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
            var attacker = new TestCharacter();
            var enemy = new TestCharacter();

            attacker.TestCalcNormalDamage(40);
            attacker.AttackPower = 40;
            enemy.ArmorValue = 10;
            enemy.CurrentHP = 100;

            // Act
            attacker.Attack(enemy);

            //Log
            _log.WriteLine($"din attackpower till en början: {attacker.AttackPower}");
            _log.WriteLine($"fiendes armorvalue: {enemy.ArmorValue}");
            _log.WriteLine($"fiendes currenthp: 100");
            _log.WriteLine($"hur mycket damage du gör på fiende: {attacker.CalcNormalDamage()}");
            _log.WriteLine($"fiendes hp efter du attackerat: {enemy.CurrentHP} MATHS: 100 + 10armor - 40 damage = 70");

            // Assert
            Assert.Equal(70, enemy.CurrentHP); // 100 - (40 - 10) = 70 fiende borde ha 70hp
        }

    }
}
