namespace GameAPI.Tests
{
    public class GameManagerTests
    {
        #region Constructor
        private readonly ITestOutputHelper _log;
        private GameManager gameManager;

        public GameManagerTests(ITestOutputHelper output)
        {
            _log = output;
            gameManager = new GameManager();
        }
        #endregion

        [Fact]
        public void CanEquip_InTown_ShouldReturnTrue()
        {
            //Arrange
            gameManager.GetGameState().Location = new Town("Town");

            //Act
            var result = gameManager.CanEquip();

            //Assert
            _log.WriteLine($"kan equippa i town? {result}");
            Assert.True(result);
        }

        [Fact]
        public void CanEquip_NotInTown_ShouldReturnFalse()
        {
            gameManager.GetGameState().Location = new Battle("Battle", new Enemy());

            var result = gameManager.CanEquip();

            _log.WriteLine($"Kan equippa utanför town? {result}");
            Assert.False(result);
        }

        [Fact]
        public void EquipWeapon_ShouldEquipToHero()
        {
            var weapon = new Weapon();
            gameManager.GetGameState().Location = new Town("Town");
            gameManager.GetGameState().Hero.EquipmentInBag.Add(weapon);

            gameManager.Equip(0);

            _log.WriteLine($"Hero equipped wepon: {gameManager.GetGameState().Hero.EquippedWeapon}");
            Assert.NotNull(gameManager.GetGameState().Hero.EquippedWeapon);
        }

        [Fact]
        public void EquipArmor_ShouldEquipToHero()
        {
            var armor = new Armor() { ArmorValue = 10 };
            gameManager.GetGameState().Location = new Town("Town");
            gameManager.GetGameState().Hero.EquipmentInBag.Add(armor);
            gameManager.Equip(gameManager.GetGameState().Hero.EquipmentInBag.Count() - 1);

            _log.WriteLine($"Hero equip armor samt armor val: {gameManager.GetGameState().Hero.EquippedArmor}");
            Assert.NotNull(gameManager.GetGameState().Hero.EquippedArmor);
        }

        [Fact]
        public void EquipArmor_ShouldEquipToHero2()
        {
            //Testar equippa armor med equiparmor metoden vilket fungerar utmärkt tillskillnad från den ovanför
            var armor = new Armor() { ArmorValue = 10 };
            gameManager.GetGameState().Location = new Town("Town");
            gameManager.GetGameState().Hero.EquipmentInBag.Add(armor);
            gameManager.EquipArmor(armor);

            _log.WriteLine($"Hero equip armor samt armor val: {gameManager.GetGameState().Hero.EquippedArmor}");
            Assert.NotNull(gameManager.GetGameState().Hero.EquippedArmor);
        }

        [Fact]
        public void StartFight_ShouldChangeLocationToBattle()
        {
            gameManager.StartFight();

            _log.WriteLine($"Location efter startad battle: {gameManager.GetGameState().Location}");
            Assert.IsType<Battle>(gameManager.GetGameState().Location);
        }

        [Fact]
        public void Attack_ShouldInvokeHeroAttack()
        {
            var enemyMock = new Mock<Enemy>();
            gameManager.GetGameState().Location = new Battle("Battle", enemyMock.Object);

            gameManager.Attack();

            _log.WriteLine($"Enemy hp efter attack: {enemyMock.Object.CurrentHP}");
            Assert.NotEqual(enemyMock.Object.MaxHP, enemyMock.Object.CurrentHP);
        }

        [Fact]
        public void EnemyTurn_WhenEnemyHasHP_ShouldInvokeEnemyAttack()
        {
            //var heroMock = new Mock<Hero>(); försökte mocka innan och skicka som objekt men fungerade inte
            var heroMock = new Hero(1, "Testhero");
            var enemy = new Enemy { CurrentHP = 100, AttackPower = 5 };
            gameManager.GetGameState().Hero = heroMock;
            gameManager.GetGameState().Location = new Battle("Battle", enemy);

            gameManager.EnemyTurn(enemy);

            _log.WriteLine($"Hero hp efter attack: {heroMock.CurrentHP} hp innan attack: {heroMock.MaxHP}");
            Assert.NotEqual(heroMock.MaxHP, heroMock.CurrentHP);
        }

        [Fact]
        public void EnemyTurn_WhenEnemyHasNoHP_ShouldEndBattle()
        {
            var enemy = new Enemy { CurrentHP = 0 };
            gameManager.GetGameState().Location = new Battle("Battle", enemy);

            gameManager.EnemyTurn(enemy);

            _log.WriteLine($"location efter fiende är död (borde va town?): {gameManager.GetGameState().Location}");
            Assert.IsType<Town>(gameManager.GetGameState().Location);
        }


    }
}
