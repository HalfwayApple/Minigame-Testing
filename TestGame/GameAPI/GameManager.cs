using GameAPI.Data.Events;
using GameAPI.Data.Items.Equipment.Armors;
using GameAPI.Data.Items.Equipment.Weapons;

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
	}
}
