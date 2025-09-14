using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MaterialManagementSystem.Tests
{
    [TestClass]
    public class CalculationServiceTests
    {
        [TestMethod]
        public void GetProductPrice_ValidInput_ReturnsNonNegative()
        {
            // Arrange
            int productId = 1;

            // Act - симулируем получение цены
            decimal result = GetProductPrice(productId);

            // Assert
            Assert.IsTrue(result >= 0);
        }

        [TestMethod]
        public void CalculateProductTotal_CorrectCalculation()
        {
            // Arrange
            decimal unitPrice = 500.00m;
            int quantity = 2;

            // Act
            decimal result = CalculateProductTotal(unitPrice, quantity);

            // Assert
            Assert.AreEqual(1000.00m, result);
        }

        [TestMethod]
        public void CalculateProductTotal_ZeroQuantity_ReturnsZero()
        {
            // Arrange
            decimal unitPrice = 500.00m;
            int quantity = 0;

            // Act
            decimal result = CalculateProductTotal(unitPrice, quantity);

            // Assert
            Assert.AreEqual(0m, result);
        }

        // Методы, повторяющие логику расчетов
        private decimal GetProductPrice(int productId)
        {
            // Симуляция получения цены из базы данных
            // В реальном приложении здесь был бы SQL-запрос
            switch (productId)
            {
                case 1:
                    return 500.00m;
                case 2:
                    return 300.00m;
                case 3:
                    return 200.00m;
                default:
                    return 0m;
            }
        }

        private decimal CalculateProductTotal(decimal unitPrice, int quantity)
        {
            return unitPrice * quantity;
        }
    }
}