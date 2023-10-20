using GameAPI.Data.Characters;
using GameAPI.Data.Events;
using GameAPI.Data.Items.Equipment;
using GameAPI.Data.Items.Equipment.Armors;
using GameAPI.Data.Items.Equipment.Weapons;
using System;

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

		#region Equip
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
        internal bool CanEquip()
        {
            Location location = _state.Location;
            if (location.GetType() == typeof(Town))
            {
                return true;
            }
            return false;
        }
        internal GameState EquipWeapon(Weapon weapon)
        {
            _state.Hero.EquipWeapon(weapon);
            return _state;
        }
		internal GameState EquipArmor(Armor armor)
		{
			_state.Hero.EquipArmor(armor);
			return _state;
		}
		#endregion

        public GameState StartFight()
        {
            Enemy enemy = ChooseRandomEnemy();
            _state.Location = new Battle("Battle", enemy);

            return _state;
		}

        internal Enemy ChooseRandomEnemy()
        {
			Random rng = new Random();
			int randomNumber = rng.Next(0, _state.EnemyList.Count);
			Enemy enemy = _state.EnemyList[randomNumber];

            return enemy;
		}

		internal GameState EndBattle(Enemy enemy)
		{
			Equipment? loot = enemy.DropEquipment();
			if (loot != null)
			{
				_state.Hero.EquipmentInBag.Add(loot);
			}
			_state.Hero.Xp += enemy.XpValue;
			_state.Hero.LevelUpCheck();
            _state.Location = new Town("Town");

            return _state;
		}

        public GameState Attack()
        {
            Battle battleLocation = (Battle) _state.Location;
            Enemy enemy = battleLocation.Enemy;

            _state.Hero.Attack(enemy);

            return EnemyTurn(enemy);
		}

        internal GameState EnemyTurn(Enemy enemy)
        {
			if (enemy.CurrentHP > 0)
			{
				enemy.Attack(_state.Hero);
				return _state;
			}
			else
			{
				return EndBattle(enemy);
			}
		}
	}
}
