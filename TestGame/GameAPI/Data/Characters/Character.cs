namespace GameAPI.Data.Characters
{
    abstract internal class Character
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

        public void Attack(Character enemy)
        {
            int firstRoundDamage = CalcNormalDamage();
            int secondRoundDamage = firstRoundDamage - enemy.ArmorValue;
            enemy.CurrentHP -= secondRoundDamage;
        }
        abstract public int CalcNormalDamage();
    }
}
