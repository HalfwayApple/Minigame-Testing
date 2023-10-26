namespace GameAPI.Data.Characters
{
    abstract public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int MaxMana {  get; set; }
        public int CurrentMana { get; set; }
        public int AttackPower { get; set; }
        public int ArmorValue { get; set; }

        /// <summary>
        /// Attacks input opponent and reduces their max hp
        /// </summary>
        /// <param name="opponent"></param>
        public int Attack(Character opponent)
        {
            int unmigitatedDamage = CalcNormalDamage();
            int totalDamageDealt = unmigitatedDamage - opponent.ArmorValue;

			if (totalDamageDealt < 0) { totalDamageDealt = 0; } // Stop high armor vs low damage attacks from becoming healing

			opponent.CurrentHP -= totalDamageDealt;

            return totalDamageDealt;
        }
        public int CalcNormalDamage()
        {
			int damage = AttackPower;
			return damage;
		}
    }
}
