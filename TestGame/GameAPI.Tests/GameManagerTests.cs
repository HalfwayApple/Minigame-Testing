using GameAPI.Data.Characters;

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

		#region CanEquip
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
		public void CanEquip_LocationIsNull_ShouldThrowArgumentNullException()
		{
			gameManager.GetGameState().Location = null;

			Assert.Throws<ArgumentNullException>(() => gameManager.CanEquip());
		}
		#endregion
		#region Equip
		[Fact]
		public void Equip_EquippingWeapon_WhenUnarmed_ShouldRemoveWeaponFromBag()
		{
			Weapon weapon = new Weapon();
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedWeapon = null;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(weapon);

			gameManager.Equip(0);

			_log.WriteLine($"Hero equipped wepon: {gameManager.GetGameState().Hero.EquippedWeapon}");
			_log.WriteLine($"Hero bag: {gameManager.GetGameState().Hero.EquipmentInBag}");

			Assert.Empty(gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void Equip_EquippingWeapon_WhenArmed_ShouldRemoveWeaponFromBag()
		{
			Weapon initialWeapon = new Weapon();
			initialWeapon.Name = "InitialWeapon";
			Weapon newWeapon = new Weapon();
			newWeapon.Name = "NewWeapon";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedWeapon = initialWeapon;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newWeapon);

			gameManager.Equip(0);

			_log.WriteLine($"Hero equipped wepon: {gameManager.GetGameState().Hero.EquippedWeapon}");
			_log.WriteLine($"Hero bag: {gameManager.GetGameState().Hero.EquipmentInBag}");

			Assert.DoesNotContain(newWeapon, gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void Equip_EquippingWeapon_WhenUnarmed_ShouldEquipWeaponToHero()
		{
			Weapon weapon = new Weapon();
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedWeapon = null;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(weapon);

			gameManager.Equip(0);

			_log.WriteLine($"Hero equipped wepon: {gameManager.GetGameState().Hero.EquippedWeapon}");
			Assert.Equal(weapon, gameManager.GetGameState().Hero.EquippedWeapon);
		}
		[Fact]
		public void Equip_EquippingWeapon_WhenArmed_ShouldEquipWeaponToHero()
		{
			Weapon initialWeapon = new Weapon();
			initialWeapon.Name = "InitialWeapon";
			Weapon newWeapon = new Weapon();
			newWeapon.Name = "NewWeapon";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedWeapon = initialWeapon;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newWeapon);

			gameManager.Equip(0);

			_log.WriteLine($"Hero equipped wepon: {gameManager.GetGameState().Hero.EquippedWeapon}");
			Assert.Equal(newWeapon, gameManager.GetGameState().Hero.EquippedWeapon);
		}
		[Fact]
		public void Equip_EquippingWeapon_WhenArmed_ShouldPutInitialWeaponInBag()
		{
			Weapon initialWeapon = new Weapon();
			initialWeapon.Name = "InitialWeapon";
			Weapon newWeapon = new Weapon();
			newWeapon.Name = "NewWeapon";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedWeapon = initialWeapon;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newWeapon);

			gameManager.Equip(0);

			_log.WriteLine($"Hero equipped wepon: {gameManager.GetGameState().Hero.EquippedWeapon}");
			Assert.Contains(initialWeapon, gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void Equip_EquippingArmor_WhenUnarmored_ShouldRemoveArmorFromBag()
		{
			Armor armor = new Armor();
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedArmor = null;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(armor);

			gameManager.Equip(0);

			_log.WriteLine($"Hero equipped armor: {gameManager.GetGameState().Hero.EquippedArmor}");
			_log.WriteLine($"Hero bag: {gameManager.GetGameState().Hero.EquipmentInBag}");

			Assert.Empty(gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void Equip_EquippingArmor_WhenArmored_ShouldRemoveArmorFromBag()
		{
			Armor initialArmor = new Armor();
			initialArmor.Name = "InitialArmor";
			Armor newArmor = new Armor();
			newArmor.Name = "NewArmor";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedArmor = initialArmor;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newArmor);

			gameManager.Equip(0);

			_log.WriteLine($"Hero equipped Armor: {gameManager.GetGameState().Hero.EquippedArmor}");
			_log.WriteLine($"Hero bag: {gameManager.GetGameState().Hero.EquipmentInBag}");

			Assert.DoesNotContain(newArmor, gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void Equip_EquippingArmor_WhenUnarmored_ShouldEquipArmorToHero()
		{
			Armor armor = new Armor();
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedArmor = null;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(armor);

			gameManager.Equip(0);

			_log.WriteLine($"Hero equipped Armor: {gameManager.GetGameState().Hero.EquippedArmor}");
			Assert.Equal(armor, gameManager.GetGameState().Hero.EquippedArmor);
		}
		[Fact]
		public void Equip_EquippingArmor_WhenArmored_ShouldEquipArmorToHero()
		{
			Armor initialArmor = new Armor();
			initialArmor.Name = "InitialArmor";
			Armor newArmor = new Armor();
			newArmor.Name = "NewArmor";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedArmor = initialArmor;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newArmor);

			gameManager.Equip(0);

			_log.WriteLine($"Hero equipped Armor: {gameManager.GetGameState().Hero.EquippedArmor}");
			Assert.Equal(newArmor, gameManager.GetGameState().Hero.EquippedArmor);
		}
		[Fact]
		public void Equip_EquippingArmor_WhenArmored_ShouldPutInitialArmorInBag()
		{
			Armor initialArmor = new Armor();
			initialArmor.Name = "InitialArmor";
			Armor newArmor = new Armor();
			newArmor.Name = "NewArmor";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedArmor = initialArmor;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newArmor);

			gameManager.Equip(0);

			_log.WriteLine($"Hero equipped armor: {gameManager.GetGameState().Hero.EquippedArmor}");
			Assert.Contains(initialArmor, gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void Equip_IndexLessThanZero_ShouldThrowArgumentOutOfRangeException()
		{
			gameManager.GetGameState().Location = new Town("Town");
			Assert.Throws<ArgumentOutOfRangeException>(() => gameManager.Equip(-1));
		}
		[Fact]
		public void Equip_IndexMoreThanNumberOfItemsInBag_ShouldThrowArgumentOutOfRangeException()
		{
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(new Armor());

			Assert.Throws<ArgumentOutOfRangeException>(() => gameManager.Equip(5));
		}
		[Fact]
		public void Equip_EquipmentInBagIsNull_ShouldThrowArgumentNullException()
		{
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquipmentInBag = null;

			Assert.Throws<ArgumentNullException>(() => gameManager.Equip(0));
		}
		[Fact]
		public void Equip_ItemIsNotAcceptedDerivativeOfEquipment_ShouldThrowArgumentException()
		{
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(new Equipment());

			Assert.Throws<ArgumentException>(() => gameManager.Equip(0));
		}
		#endregion
		#region EquipWeapon
		[Fact]
		public void EquipWeapon_EquippingWeapon_WhenUnarmed_ShouldRemoveWeaponFromBag()
		{
			Weapon weapon = new Weapon();
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedWeapon = null;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(weapon);

			gameManager.EquipWeapon(weapon);

			_log.WriteLine($"Hero equipped wepon: {gameManager.GetGameState().Hero.EquippedWeapon}");
			_log.WriteLine($"Hero bag: {gameManager.GetGameState().Hero.EquipmentInBag}");

			Assert.Empty(gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void EquipWeapon_EquippingWeapon_WhenArmed_ShouldRemoveWeaponFromBag()
		{
			Weapon initialWeapon = new Weapon();
			initialWeapon.Name = "InitialWeapon";
			Weapon newWeapon = new Weapon();
			newWeapon.Name = "NewWeapon";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedWeapon = initialWeapon;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newWeapon);

			gameManager.EquipWeapon(newWeapon);

			_log.WriteLine($"Hero equipped wepon: {gameManager.GetGameState().Hero.EquippedWeapon}");
			_log.WriteLine($"Hero bag: {gameManager.GetGameState().Hero.EquipmentInBag}");

			Assert.DoesNotContain(newWeapon, gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void EquipWeapon_EquippingWeapon_WhenUnarmed_ShouldEquipWeaponToHero()
		{
			Weapon weapon = new Weapon();
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedWeapon = null;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(weapon);

			gameManager.EquipWeapon(weapon);

			_log.WriteLine($"Hero equipped wepon: {gameManager.GetGameState().Hero.EquippedWeapon}");
			Assert.Equal(weapon, gameManager.GetGameState().Hero.EquippedWeapon);
		}
		[Fact]
		public void EquipWeapon_EquippingWeapon_WhenArmed_ShouldEquipWeaponToHero()
		{
			Weapon initialWeapon = new Weapon();
			initialWeapon.Name = "InitialWeapon";
			Weapon newWeapon = new Weapon();
			newWeapon.Name = "NewWeapon";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedWeapon = initialWeapon;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newWeapon);

			gameManager.EquipWeapon(newWeapon);

			_log.WriteLine($"Hero equipped wepon: {gameManager.GetGameState().Hero.EquippedWeapon}");
			Assert.Equal(newWeapon, gameManager.GetGameState().Hero.EquippedWeapon);
		}
		[Fact]
		public void EquipWeapon_EquippingWeapon_WhenArmed_ShouldPutInitialWeaponInBag()
		{
			Weapon initialWeapon = new Weapon();
			initialWeapon.Name = "InitialWeapon";
			Weapon newWeapon = new Weapon();
			newWeapon.Name = "NewWeapon";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedWeapon = initialWeapon;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newWeapon);

			gameManager.EquipWeapon(newWeapon);

			_log.WriteLine($"Hero equipped wepon: {gameManager.GetGameState().Hero.EquippedWeapon}");
			Assert.Contains(initialWeapon, gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void EquipWeapon_ItemIsNotWeapon_ShouldThrowArgumentException()
		{
			gameManager.GetGameState().Location = new Town("Town");
			Weapon weapon = null;

			Assert.Throws<ArgumentNullException>(() => gameManager.EquipWeapon(weapon));
		}
		#endregion
		#region EquipArmor
		[Fact]
		public void EquipArmor_EquippingArmor_WhenUnarmored_ShouldRemoveArmorFromBag()
		{
			Armor armor = new Armor();
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedArmor = null;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(armor);

			gameManager.EquipArmor(armor);

			_log.WriteLine($"Hero equipped armor: {gameManager.GetGameState().Hero.EquippedArmor}");
			_log.WriteLine($"Hero bag: {gameManager.GetGameState().Hero.EquipmentInBag}");

			Assert.Empty(gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void EquipArmor_EquippingArmor_WhenArmored_ShouldRemoveArmorFromBag()
		{
			Armor initialArmor = new Armor();
			initialArmor.Name = "InitialArmor";
			Armor newArmor = new Armor();
			newArmor.Name = "NewArmor";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedArmor = initialArmor;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newArmor);

			gameManager.EquipArmor(newArmor);

			_log.WriteLine($"Hero equipped Armor: {gameManager.GetGameState().Hero.EquippedArmor}");
			_log.WriteLine($"Hero bag: {gameManager.GetGameState().Hero.EquipmentInBag}");

			Assert.DoesNotContain(newArmor, gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void EquipArmor_EquippingArmor_WhenUnarmored_ShouldEquipArmorToHero()
		{
			Armor armor = new Armor();
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedArmor = null;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(armor);

			gameManager.EquipArmor(armor);

			_log.WriteLine($"Hero equipped Armor: {gameManager.GetGameState().Hero.EquippedArmor}");
			Assert.Equal(armor, gameManager.GetGameState().Hero.EquippedArmor);
		}
		[Fact]
		public void EquipArmor_EquippingArmor_WhenArmored_ShouldEquipArmorToHero()
		{
			Armor initialArmor = new Armor();
			initialArmor.Name = "InitialArmor";
			Armor newArmor = new Armor();
			newArmor.Name = "NewArmor";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedArmor = initialArmor;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newArmor);

			gameManager.EquipArmor(newArmor);

			_log.WriteLine($"Hero equipped Armor: {gameManager.GetGameState().Hero.EquippedArmor}");
			Assert.Equal(newArmor, gameManager.GetGameState().Hero.EquippedArmor);
		}
		[Fact]
		public void EquipArmor_EquippingArmor_WhenArmored_ShouldPutInitialArmorInBag()
		{
			Armor initialArmor = new Armor();
			initialArmor.Name = "InitialArmor";
			Armor newArmor = new Armor();
			newArmor.Name = "NewArmor";
			gameManager.GetGameState().Location = new Town("Town");
			gameManager.GetGameState().Hero.EquippedArmor = initialArmor;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Add(newArmor);

			gameManager.EquipArmor(newArmor);

			_log.WriteLine($"Hero equipped armor: {gameManager.GetGameState().Hero.EquippedArmor}");
			Assert.Contains(initialArmor, gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void EquipArmor_ItemIsNotArmor_ShouldThrowArgumentException()
		{
			gameManager.GetGameState().Location = new Town("Town");
			Armor armor = null;

			Assert.Throws<ArgumentNullException>(() => gameManager.EquipArmor(armor));
		}
		#endregion
		#region StartFight
		[Fact]
        public void StartFight_ShouldChangeLocationToBattle()
        {
            gameManager.StartFight();

            _log.WriteLine($"Location efter startad battle: {gameManager.GetGameState().Location}");
            Assert.IsType<Battle>(gameManager.GetGameState().Location);
        }
		#endregion
		#region EnemyTurn
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

		/*
        [Fact]
        public void EnemyTurn_WhenEnemyHasNoHP_ShouldEndBattle()
        {
            var enemy = new Enemy { CurrentHP = 0 };
            gameManager.GetGameState().Location = new Battle("Battle", enemy);

            gameManager.EnemyTurn(enemy);

            _log.WriteLine($"location efter fiende är död (borde va town?): {gameManager.GetGameState().Location}");
            Assert.IsType<Town>(gameManager.GetGameState().Location);
        }
		*/
		#endregion
		#region Attack
		[Fact]
        public void Attack_ShouldInvokeHeroAttack()
        {
            var enemyMock = new Mock<Enemy>();
            gameManager.GetGameState().Location = new Battle("Battle", enemyMock.Object);

            gameManager.Attack();

            _log.WriteLine($"Enemy hp efter attack: {enemyMock.Object.CurrentHP}");
            Assert.NotEqual(enemyMock.Object.MaxHP, enemyMock.Object.CurrentHP);
        }
		#endregion
		#region Buy
		[Fact]
		public void Buy_ShouldReturnSameState_IfNotInShop()
		{
			// Arrange
			gameManager.GetGameState().Location = new Town("Town");
			int heroMoneyAtStart = 500;
			gameManager.GetGameState().Hero.Money = heroMoneyAtStart;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();

			// Act
			var result = gameManager.Buy(0);

			// Assert
			Assert.Equal(heroMoneyAtStart, result.Hero.Money);
			Assert.Empty(result.Hero.EquipmentInBag);
		}

		[Fact]
		public void Buy_ShouldReturnSameState_IfNoEquipmentForSale()
		{
			// Arrange
			gameManager.GetGameState().Location = new Shop("Shop");
			var shopLocation = (Shop)gameManager.GetGameState().Location;
			shopLocation.EquipmentForSale.Clear();
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			int heroMoneyAtStart = 500;
			gameManager.GetGameState().Hero.Money = heroMoneyAtStart;

			// Act
			var result = gameManager.Buy(0);

			// Assert
			Assert.Equal(heroMoneyAtStart, result.Hero.Money);
			Assert.Empty(result.Hero.EquipmentInBag);
			Assert.Empty(shopLocation.EquipmentForSale);
		}

		[Fact]
		public void Buy_ShouldReturnSameState_IfNotEnoughMoney()
		{
			// Arrange
			gameManager.GetGameState().Location = new Shop("Shop");
			var shopLocation = (Shop)gameManager.GetGameState().Location;
			var item = new Equipment { Price = 100 };
			shopLocation.EquipmentForSale.Add(item);
			int heroMoneyAtStart = 50;
			gameManager.GetGameState().Hero.Money = heroMoneyAtStart;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();


			// Act
			var result = gameManager.Buy(0);

			// Assert
			Assert.Equal(heroMoneyAtStart, result.Hero.Money);
			Assert.Contains(item, shopLocation.EquipmentForSale);
			Assert.DoesNotContain(item, result.Hero.EquipmentInBag);
		}

		[Fact]
		public void Buy_ShouldUpdateState_IfAllConditionsMet()
		{
			// Arrange
			gameManager.GetGameState().Location = new Shop("Shop");
			var shopLocation = (Shop)gameManager.GetGameState().Location;
			shopLocation.EquipmentForSale.Clear();
			var item = new Equipment { Price = 100 };
			shopLocation.EquipmentForSale.Add(item);
            int heroMoneyAtStart = 200;
			gameManager.GetGameState().Hero.Money = heroMoneyAtStart;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();

			// Act
			var result = gameManager.Buy(0);

			// Assert
			Assert.Equal(heroMoneyAtStart - item.Price, result.Hero.Money);
			Assert.Contains(item, result.Hero.EquipmentInBag);
			Assert.DoesNotContain(item, shopLocation.EquipmentForSale);
		}
		#endregion
		#region Sell
		[Fact]
		public void Sell_ShouldReturnSameState_IfNotInShop()
		{
			// Arrange
			gameManager.GetGameState().Location = new Town("Town");
			int heroMoneyAtStart = 500;
			gameManager.GetGameState().Hero.Money = heroMoneyAtStart;
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			var item = new Equipment { Price = 100 };
			gameManager.GetGameState().Hero.EquipmentInBag.Add(item);

			// Act
			var result = gameManager.Sell(0);

			// Assert
			Assert.Equal(heroMoneyAtStart, result.Hero.Money);
			Assert.Contains(item, result.Hero.EquipmentInBag);
		}

		[Fact]
		public void Sell_ShouldThrowException_IfIndexOutOfRange()
		{
			// Arrange
			gameManager.GetGameState().Location = new Shop("Shop");
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();

			// Act & Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => gameManager.Sell(0));
		}

		[Fact]
		public void Sell_ShouldUpdateState_IfAllConditionsMet()
		{
			// Arrange
			gameManager.GetGameState().Location = new Shop("Shop");
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();
			var item = new Equipment { Price = 100 };
			gameManager.GetGameState().Hero.EquipmentInBag.Add(item);
			int heroMoneyAtStart = 0;
			gameManager.GetGameState().Hero.Money = heroMoneyAtStart;

			// Act
			var result = gameManager.Sell(0);

			// Assert
			Assert.Equal(heroMoneyAtStart + item.Price, result.Hero.Money);
			Assert.DoesNotContain(item, result.Hero.EquipmentInBag);
		}
		#endregion
	}
}
