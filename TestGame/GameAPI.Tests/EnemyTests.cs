using GameAPI.Data.Items.Equipment;

namespace GameAPI.Tests
{
    public class EnemyTests
    {
        #region Constructor
        private readonly ITestOutputHelper _log;

        public EnemyTests(ITestOutputHelper output)
        {
            _log = output;
        }
        #endregion

        [Fact]
        public void CalcNormalDamage_ReturnsAttackPowerValue()
        {
            // Arrange
            var enemy = new Enemy();
            enemy.AttackPower = 25;

            // Act
            int damage = enemy.CalcNormalDamage();

            //Log
            _log.WriteLine($"fiendes attackpower: {damage}");

            // Assert
            Assert.Equal(25, damage);
        }

        [Fact]
        public void DropEquipment_ReturnsNull_WhenLootTableIsEmpty()
        {
            // Arrange
            var enemy = new Enemy();
            enemy.LootTable = new List<Equipment>();

            // Act
            var equipment = enemy.DropEquipment();

            //Log
            _log.WriteLine($"dina drops finns inte");

            // Assert
            Assert.Null(equipment);
        }

        [Fact]
        public void DropEquipment_ReturnsEquipmentFromLootTable()
        {
            // Arrange
            var enemy = new Enemy();
            var mockEquipment = new Mock<Equipment>().Object;
            mockEquipment.Name = "infantry lasgun";
            enemy.LootTable.Add(mockEquipment);

            // Act
            var droppedEquipment = enemy.DropEquipment();

            //Log
            _log.WriteLine($"Dropped equipment: {droppedEquipment.Name}");

            // Assert
            Assert.Contains(droppedEquipment, enemy.LootTable);
        }
    }
}
