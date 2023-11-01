using GameAPI.Data.Characters;
using GameAPI.Data.Items.Equipment.Armors;
using System.Reflection;
using Xunit.Sdk;

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
		public class LocationDataAttribute : DataAttribute
		{
			public override IEnumerable<object[]> GetData(MethodInfo testMethod)
			{
				yield return new object[] { new Location("NonDerivedLocation") };
				yield return new object[] { new Town("Town") };
				yield return new object[] { new Shop("Shop") };
			}
		}
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
		[Fact]
		public void StartFight_ShouldAddAnEnemyToBattle()
		{
			gameManager.StartFight();
			Battle battleLocation = (Battle) gameManager.GetGameState().Location;

			_log.WriteLine($"Location efter startad battle: {gameManager.GetGameState().Location}");

			Assert.IsType<Enemy>(battleLocation.Enemy);
		}
		// ADD TEST FOR CHECKING NULLREFERENCE WHEN NO ENEMY IS IN BATTLE! ASK TED!
		#endregion
		#region ChooseRandomEnemy
		[Fact]
		public void ChooseRandomEnemy_ReturnsMemberwiseClonePart_NotSameObject()
		{
			// Arrange
			var enemyList = new List<Enemy>
			{
				new Enemy() {Name = "OnlyEnemy"}
			};
			gameManager.GetGameState().EnemyList = enemyList;

			// Act
			Enemy enemyResult = gameManager.ChooseRandomEnemy();

			// Assert
			Assert.DoesNotContain(enemyResult, gameManager.GetGameState().EnemyList);
			Assert.NotEqual(enemyResult, gameManager.GetGameState().EnemyList[0]);
			// The enemyResult is functionally the same as the enemy in the EnemyList but since it
			// is a clone, DoesNotContain and NotEquals shows us it is not the same object
		}
		[Fact]
		public void ChooseRandomEnemy_ReturnsMemberwiseClonePart_SameProperties()
		{
			// Arrange
			var enemyList = new List<Enemy>
			{
				new Enemy() {Name = "OnlyEnemy"}
			};
			gameManager.GetGameState().EnemyList = enemyList;

			// Act
			Enemy enemyResult = gameManager.ChooseRandomEnemy();

			// Assert
			Assert.Equal(enemyResult.Name, gameManager.GetGameState().EnemyList[0].Name);
		}
		[Fact]
		public void ChooseRandomEnemy_WhenEnemyListIsNull_ShouldThrowNullReferenceException()
		{
			// Arrange
			gameManager.GetGameState().EnemyList = null;

			// Assert
			Assert.Throws<NullReferenceException>(() => gameManager.ChooseRandomEnemy());
		}
		[Fact]
		public void ChooseRandomEnemy_WhenEnemyListIsEmpty_ShouldThrowArgumentOutOfRangeException()
		{
			// Arrange
			gameManager.GetGameState().EnemyList = new List<Enemy>();

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => gameManager.ChooseRandomEnemy());
		}
		#endregion
		#region EndBattle
		[Fact]
		public void EndBattle_WhenLootDrops_ShouldAddLootToHero()
		{
			// Arrange
			Equipment loot = new Weapon();
			Enemy enemy = new Enemy()
			{
				NoDropChance = 0
			};
			enemy.LootTable.Clear();
			enemy.LootTable.Add(loot);
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();

			// Act
			gameManager.EndBattle(enemy);

			// Assert
			Assert.Contains(loot, gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void EndBattle_WhenLootDoesntDrop_ShouldNotAddLootToHero()
		{
			// Arrange
			Equipment loot = new Weapon();
			Enemy enemy = new Enemy()
			{
				NoDropChance = 100
			};
			enemy.LootTable.Clear();
			enemy.LootTable.Add(loot);
			gameManager.GetGameState().Hero.EquipmentInBag.Clear();

			// Act
			gameManager.EndBattle(enemy);

			// Assert
			Assert.DoesNotContain(loot, gameManager.GetGameState().Hero.EquipmentInBag);
		}
		[Fact]
		public void EndBattle_ShouldAddXpToHero()
		{
			// Arrange
			Enemy enemy = new Enemy()
			{
				XpValue = 100
			};
			gameManager.GetGameState().Hero.Xp = 0;

			// Act
			gameManager.EndBattle(enemy);

			// Assert
			Assert.Equal(gameManager.GetGameState().Hero.Xp, enemy.XpValue);
		}
		[Fact]
		public void EndBattle_ShouldAddMoneyToHero()
		{
			// Arrange
			Enemy enemy = new Enemy()
			{
				MoneyValue = 100
			};
			gameManager.GetGameState().Hero.Money = 0;

			// Act
			gameManager.EndBattle(enemy);

			// Assert
			Assert.Equal(gameManager.GetGameState().Hero.Money, enemy.MoneyValue);
		}
		[Fact]
		public void EndBattle_IfNewXpIsEnough_ShouldLevelUpHero()
		{
			// Arrange
			Equipment loot = new Weapon();
			Enemy enemy = new Enemy()
			{
				XpValue = 1000
			};
			gameManager.GetGameState().Hero.Xp = 0;
			gameManager.GetGameState().Hero.Level = 1;

			// Act
			gameManager.EndBattle(enemy);

			// Assert
			Assert.True(gameManager.GetGameState().Hero.Level > 1);
		}
		[Fact]
		public void EndBattle_IfNewXpIsNotEnough_ShouldNotLevelUpHero()
		{
			// Arrange
			Equipment loot = new Weapon();
			Enemy enemy = new Enemy()
			{
				XpValue = 1
			};
			gameManager.GetGameState().Hero.Xp = 0;
			gameManager.GetGameState().Hero.Level = 1;

			// Act
			gameManager.EndBattle(enemy);

			// Assert
			Assert.False(gameManager.GetGameState().Hero.Level > 1);
		}
		[Fact]
		public void EndBattle_ShouldReturnHeroToTown()
		{
			// Arrange
			Enemy enemy = new Enemy();
			gameManager.GetGameState().Location = new Battle("Battle", enemy);

			// Act
			gameManager.EndBattle(enemy);

			// Assert
			Assert.IsType<Town>(gameManager.GetGameState().Location);
		}
		[Fact]
		public void EndBattle_IfEnemyIsNull_ShouldThrowArgumentNullException()
		{
			// Arrange
			Enemy enemy = null;

			// Assert
			Assert.Throws<ArgumentNullException>(() => gameManager.EndBattle(enemy));
		}
		#endregion
		#region EnemyTurn
		[Fact]
		public void EnemyTurn_IfEnemyDoesDamage_ShouldUpdateDamageTakenValue()
		{
			// Arrange
			gameManager.GetGameState().Hero.ArmorValue = 0;
			Enemy enemy = new Enemy()
			{
				AttackPower = 2
			};
			gameManager.GetGameState().Location = new Battle("Battle", enemy);
			Battle battleLocation = (Battle) gameManager.GetGameState().Location;
			battleLocation.DamageTakenLastTurn = 0;

			// Act
			gameManager.EnemyTurn(enemy);

			// Assert
			Assert.Equal(battleLocation.DamageTakenLastTurn, enemy.AttackPower);
		}
		[Fact]
		public void EnemyTurn_IfEnemyDoesDamage_ShouldDealDamageToHero()
		{
			// Arrange
			int heroFullHp = 10;
			gameManager.GetGameState().Hero.ArmorValue = 0;
			gameManager.GetGameState().Hero.MaxHP = heroFullHp;
			gameManager.GetGameState().Hero.CurrentHP = heroFullHp;

			Enemy enemy = new Enemy()
			{
				AttackPower = 2
			};
			gameManager.GetGameState().Location = new Battle("Battle", enemy);

			// Act
			gameManager.EnemyTurn(enemy);

			// Assert
			Assert.Equal(gameManager.GetGameState().Hero.CurrentHP, heroFullHp - enemy.AttackPower);
		}
		[Fact]
		public void EnemyTurn_IfHeroArmorHigherThanEnemyAttackPower_ShouldSetDamageTakenValueToZero()
		{
			// Arrange
			gameManager.GetGameState().Hero.ArmorValue = 5000;
			Enemy enemy = new Enemy()
			{
				AttackPower = 2
			};
			gameManager.GetGameState().Location = new Battle("Battle", enemy);
			Battle battleLocation = (Battle)gameManager.GetGameState().Location;
			battleLocation.DamageTakenLastTurn = 10;

			// Act
			gameManager.EnemyTurn(enemy);

			// Assert
			Assert.Equal(0, battleLocation.DamageTakenLastTurn);
		}
		[Fact]
		public void EnemyTurn_IfHeroArmorHigherThanEnemyAttackPower_ShouldNotDealDamageToHero()
		{
			// Arrange
			int heroFullHp = 10;
			gameManager.GetGameState().Hero.ArmorValue = 5000;
			gameManager.GetGameState().Hero.MaxHP = heroFullHp;
			gameManager.GetGameState().Hero.CurrentHP = heroFullHp;

			Enemy enemy = new Enemy()
			{
				AttackPower = 2
			};
			gameManager.GetGameState().Location = new Battle("Battle", enemy);

			// Act
			gameManager.EnemyTurn(enemy);

			// Assert
			Assert.Equal(gameManager.GetGameState().Hero.CurrentHP, heroFullHp);
		}
		[Fact]
		public void EnemyTurn_IfEnemyIsNull_ShouldThrowArgumentNullException()
		{
			// Arrange
			Enemy enemy = null;
			gameManager.GetGameState().Location = new Battle("Battle", enemy);

			// Assert
			Assert.Throws<ArgumentNullException>(() => gameManager.EnemyTurn(enemy));
		}
		[Fact]
		public void EnemyTurn_IfLocationIsNull_ShouldThrowArgumentNullException()
		{
			// Arrange
			Enemy enemy = new Enemy();
			gameManager.GetGameState().Location = null;

			// Assert
			Assert.Throws<ArgumentNullException>(() => gameManager.EnemyTurn(enemy));
		}
		[Theory]
		[LocationData]
		public void EnemyTurn_IfLocationIsNotBattle_ShouldThrowArgumentException(Location location)
		{
			// Arrange
			Enemy enemy = new Enemy();
			gameManager.GetGameState().Location = location;

			// Assert
			Assert.Throws<ArgumentException>(() => gameManager.EnemyTurn(enemy));
		}
		#endregion
		#region EnemyOrEnd
		[Fact]
		public void EnemyOrEnd_WhenEnemyIsAlive_ShouldStillBeInBattle()
		{
			// Arrange
			int enemyHp = 10;
			Enemy enemy = new Enemy()
			{
				MaxHP = enemyHp,
				CurrentHP = enemyHp
			};
			gameManager.GetGameState().Location = new Battle("Battle", enemy);

			// Act
			GameState result = gameManager.EnemyOrEnd(enemy);

			// Assert
			Assert.Equal("Battle", result.Location.Name);
		}
		[Fact]
		public void EnemyOrEnd_WhenEnemyIsDead_ShouldNoLongerBeInBattle()
		{
			// Arrange
			int enemyHp = 0;
			Enemy enemy = new Enemy()
			{
				MaxHP = enemyHp,
				CurrentHP = enemyHp
			};
			gameManager.GetGameState().Location = new Battle("Battle", enemy);

			// Act
			GameState result = gameManager.EnemyOrEnd(enemy);

			// Assert
			Assert.NotEqual("Battle", result.Location.Name);
		}
		[Fact]
		public void EnemyOrEnd_WhenEnemyIsDead_ShouldPutLocationToTown()
		{
			// Arrange
			int enemyHp = 0;
			Enemy enemy = new Enemy()
			{
				MaxHP = enemyHp,
				CurrentHP = enemyHp
			};
			gameManager.GetGameState().Location = new Battle("Battle", enemy);

			// Act
			GameState result = gameManager.EnemyOrEnd(enemy);

			// Assert
			Assert.Equal("Town", result.Location.Name);
		}
		[Fact]
		public void EnemyOrEnd_IfEnemyIsNull_ShouldThrowArgumentNullException()
		{
			// Arrange
			Enemy enemy = null;
			gameManager.GetGameState().Location = new Battle("Battle", enemy);

			// Assert
			Assert.Throws<ArgumentNullException>(() => gameManager.EnemyOrEnd(enemy));
		}
		[Fact]
		public void EnemyOrEnd_IfLocationIsNull_ShouldThrowArgumentNullException()
		{
			// Arrange
			Enemy enemy = new Enemy();
			gameManager.GetGameState().Location = null;

			// Assert
			Assert.Throws<ArgumentNullException>(() => gameManager.EnemyOrEnd(enemy));
		}
		[Theory]
		[LocationData]
		public void EnemyOrEnd_IfLocationIsNotBattle_ShouldThrowArgumentException(Location location)
		{
			// Arrange
			Enemy enemy = new Enemy();
			gameManager.GetGameState().Location = location;

			// Assert
			Assert.Throws<ArgumentException>(() => gameManager.EnemyOrEnd(enemy));
		}
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
