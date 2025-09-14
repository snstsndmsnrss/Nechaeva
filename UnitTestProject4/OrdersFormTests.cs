using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MaterialManagementSystem.Tests
{
    [TestClass]
    public class OrdersFormTests
    {
        [TestMethod]
        public void ValidateOrderForm_ValidData_ReturnsTrue()
        {
            // Arrange - используем конкретные значения вместо new object()
            int partnerId = 1; // Вместо object используем конкретный ID
            int managerId = 1; // Вместо object используем конкретный ID
            string totalAmount = "1000,00";
            int itemsCount = 2;

            // Act
            bool result = ValidateOrderForm(partnerId, managerId, totalAmount, itemsCount);

            // Assert
            Assert.IsTrue(result, "Валидация должна возвращать true для корректных данных. " +
                $"Partner: {partnerId}, Manager: {managerId}, Amount: {totalAmount}, Items: {itemsCount}");
        }

        [TestMethod]
        public void ValidateOrderForm_NoPartner_ReturnsFalse()
        {
            // Arrange
            object partner = null; // null вместо объекта
            int managerId = 1;
            string totalAmount = "1000.00";
            int itemsCount = 2;

            // Act
            bool result = ValidateOrderForm(partner, managerId, totalAmount, itemsCount);

            // Assert
            Assert.IsFalse(result, "Валидация должна возвращать false при отсутствии партнера");
        }

        [TestMethod]
        public void ValidateOrderForm_NoManager_ReturnsFalse()
        {
            // Arrange
            int partnerId = 1;
            object manager = null; // null вместо объекта
            string totalAmount = "1000.00";
            int itemsCount = 2;

            // Act
            bool result = ValidateOrderForm(partnerId, manager, totalAmount, itemsCount);

            // Assert
            Assert.IsFalse(result, "Валидация должна возвращать false при отсутствии менеджера");
        }

        [TestMethod]
        public void ValidateOrderForm_NoItems_ReturnsFalse()
        {
            // Arrange
            int partnerId = 1;
            int managerId = 1;
            string totalAmount = "1000.00";
            int itemsCount = 0; // 0 items

            // Act
            bool result = ValidateOrderForm(partnerId, managerId, totalAmount, itemsCount);

            // Assert
            Assert.IsFalse(result, "Валидация должна возвращать false при отсутствии товаров");
        }

        [TestMethod]
        public void ValidateOrderForm_InvalidTotalAmount_ReturnsFalse()
        {
            // Arrange
            int partnerId = 1;
            int managerId = 1;
            string totalAmount = "0.00"; // Невалидная сумма
            int itemsCount = 2;

            // Act
            bool result = ValidateOrderForm(partnerId, managerId, totalAmount, itemsCount);

            // Assert
            Assert.IsFalse(result, "Валидация должна возвращать false при нулевой сумме");
        }

        [TestMethod]
        public void ValidateOrderForm_NegativeTotalAmount_ReturnsFalse()
        {
            // Arrange
            int partnerId = 1;
            int managerId = 1;
            string totalAmount = "-100.00"; // Отрицательная сумма
            int itemsCount = 2;

            // Act
            bool result = ValidateOrderForm(partnerId, managerId, totalAmount, itemsCount);

            // Assert
            Assert.IsFalse(result, "Валидация должна возвращать false при отрицательной сумме");
        }

        private bool ValidateOrderForm(object partner, object manager, string totalAmount, int itemsCount)
        {
            // Добавим отладочную информацию
            Console.WriteLine($"Partner: {partner}, Type: {partner?.GetType().Name}");
            Console.WriteLine($"Manager: {manager}, Type: {manager?.GetType().Name}");
            Console.WriteLine($"TotalAmount: {totalAmount}, ItemsCount: {itemsCount}");

            if (partner == null)
            {
                Console.WriteLine("FAIL: Partner is null");
                return false;
            }

            if (manager == null)
            {
                Console.WriteLine("FAIL: Manager is null");
                return false;
            }

            if (itemsCount <= 0)
            {
                Console.WriteLine("FAIL: Items count <= 0");
                return false;
            }

            if (!decimal.TryParse(totalAmount, out decimal totalAmountValue) || totalAmountValue <= 0)
            {
                Console.WriteLine($"FAIL: Total amount parsing failed. Value: '{totalAmount}'");
                return false;
            }

            Console.WriteLine("SUCCESS: All validations passed");
            return true;
        }
    }
}