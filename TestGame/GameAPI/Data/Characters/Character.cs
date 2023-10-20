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
        public void Attack(Character opponent)
        {
            int firstRoundDamage = CalcNormalDamage();
            int secondRoundDamage = firstRoundDamage - opponent.ArmorValue;

			if (secondRoundDamage < 0) { secondRoundDamage = 0; } // Stop high armor vs low damage attacks from becoming healing

			opponent.CurrentHP -= secondRoundDamage;
        }
        abstract public int CalcNormalDamage();
    }
}
