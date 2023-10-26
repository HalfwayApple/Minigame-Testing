using GameAPI.Data.Characters;
using GameAPI.Data.Events;
using GameAPI.Data.Items.Equipment;
using GameAPI.Data.Items.Equipment.Armors;
using GameAPI.Data.Items.Equipment.Weapons;
using System.Xml.Linq;

namespace GameAPI
{
	public class GameManager
    {
        private GameState _state;
        public GameManager() 
        {
            _state = new GameState();
        }

        public GameState GetGameState()
        {
            return _state;
        }

        public GameState ReturnToTown()
        {
			_state.Location = new Town("Town");
            return _state;
		}

		#region Shopping

        public GameState EnterShop()
        {
			if (_state.Location.GetType() != typeof(Town)) { return _state; }

            Shop shop = new ("Shop");
			Armor breastplate = new()
			{
				Name = "Breastplate",
				ArmorValue = 1,
				Price = 35
			};
            shop.EquipmentForSale.Add(breastplate);

			_state.Location = shop;

            return _state;
		}

		public GameState Buy(int index)
        {
            if (_state.Location.GetType() != typeof(Shop)) { return _state; }

            Shop shopLocation = (Shop) _state.Location;

            if (shopLocation.EquipmentForSale.Count == 0) {  return _state; }

            Equipment item = shopLocation.EquipmentForSale[index];

            if (_state.Hero.Money < item.Price) { return _state; }

            _state.Hero.Money -= item.Price;
            _state.Hero.EquipmentInBag.Add(item);
            shopLocation.EquipmentForSale.Remove(item);

            return _state;
        }

        public GameState Sell(int index)
        {
			if (_state.Location.GetType() != typeof(Shop)) { return _state; }
			if (index < 0 || index > _state.Hero.EquipmentInBag.Count()) throw new ArgumentOutOfRangeException(nameof(index), "index cannot be less than 0 or greater than the mount of items in the bag");

			Equipment item = _state.Hero.EquipmentInBag[index];
			_state.Hero.Money += item.Price;
			_state.Hero.EquipmentInBag.Remove(item);

            return _state;
		}

		#endregion

		#region Equip

		/// <summary>
		/// 1) Checks if the hero can currently equip items. 2) Checks the type of equipment. 3) Matches the type to the correct Equip function to equip the item
		/// </summary>
		/// <param name="index"></param>
		/// <returns>GameState</returns>
		public GameState Equip(int index)
        {
            if (CanEquip() == false) { return _state; }

            var item = _state.Hero.EquipmentInBag[index];
			Type itemType = item.GetType();

            if (itemType == typeof(Weapon))
            {
                return EquipWeapon((Weapon)item);
            }
            else if (itemType == typeof(Armor))
            {
                return EquipArmor((Armor)item);
            }
            else
            {
                return _state;
            }
        }

        /// <summary>
        /// Checks if the hero can currently equip items
        /// </summary>
        /// <returns>True if location is Town, false otherwise</returns>
        internal bool CanEquip()
        {
            Location location = _state.Location;
            if (location.GetType() == typeof(Town))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Equips the specified weapon
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns>GameState</returns>
        internal GameState EquipWeapon(Weapon weapon)
        {
            _state.Hero.EquipWeapon(weapon);
            return _state;
        }

        /// <summary>
        /// Equips the specified armor
        /// </summary>
        /// <param name="armor"></param>
        /// <returns>GameState</returns>
		internal GameState EquipArmor(Armor armor)
		{
			_state.Hero.EquipArmor(armor);
			return _state;
		}
		#endregion

		#region Battle admin

        /// <summary>
        /// Creates a Battle location with a random enemy from the EnemyList in GameState, and puts the game there
        /// </summary>
        /// <returns>GameState</returns>
		public GameState StartFight()
        {
            Enemy enemy = ChooseRandomEnemy();
            _state.Location = new Battle("Battle", enemy);

            return _state;
		}

        /// <summary>
        /// Choses a random enemy from the GameStates EnemyList
        /// </summary>
        /// <returns>Random Enemy</returns>
        internal Enemy ChooseRandomEnemy()
        {
			Random rng = new Random();
			int randomNumber = rng.Next(0, _state.EnemyList.Count);
			Enemy enemy = (Enemy)_state.EnemyList[randomNumber].Clone();

            return enemy;
		}

		/// <summary>
		/// Makes enemy drop loot if possible, gives xp, checks for potential levelup, and sets location back to Town
		/// </summary>
		/// <param name="enemy"></param>
		/// <returns>GameState</returns>
		internal GameState EndBattle(Enemy enemy)
		{
			Equipment? loot = enemy.DropEquipment();
			if (loot != null)
			{
				_state.Hero.EquipmentInBag.Add(loot);
			}
			_state.Hero.Xp += enemy.XpValue;
            _state.Hero.Money += enemy.MoneyValue;
			_state.Hero.LevelUpCheck();

            return ReturnToTown();
		}

		/// <summary>
		/// Checks if enemy is still alive and either attacks the hero or ends the battle
		/// </summary>
		/// <param name="enemy"></param>
		/// <returns>GameState</returns>
		internal GameState EnemyTurn(Enemy enemy)
		{
			Battle battleLocation = (Battle)_state.Location;

			if (enemy.CurrentHP > 0)
			{
				battleLocation.DamageTakenLastTurn = enemy.Attack(_state.Hero);
				return _state;
			}
			else
			{
				return EndBattle(enemy);
			}
		}

		#endregion

		/// <summary>
		/// Attacks enemy, then calls EnemyTurn() to either further the battle or end it
		/// </summary>
		/// <returns>GameState</returns>
		public GameState Attack()
        {
            Battle battleLocation = (Battle) _state.Location;
            Enemy enemy = battleLocation.Enemy;

            battleLocation.DamageDoneLastTurn = _state.Hero.Attack(enemy);

			return EnemyTurn(enemy);
		}
	}
}
