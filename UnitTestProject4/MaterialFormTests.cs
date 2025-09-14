using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace MaterialManagementSystem.Tests
{
    [TestClass]
    public class MaterialFormTests
    {
        [TestMethod]
        public void ValidateForm_ValidData_ReturnsTrue()
        {
            // Arrange - ���������� ����� ��� ���������� �����������
            string name = "�������� ��������";
            string quantity = "100";
            string price = "500.50"; // �������� ������� �� �����

            // Act
            bool result = ValidateMaterial(name, quantity, price);

            // Assert
            Assert.IsTrue(result, "��������� ������ ���������� true ��� ���������� ������");
        }

        [TestMethod]
        public void ValidateForm_ValidDataWithComma_ReturnsTrue()
        {
            // Arrange - ���� � ������� ��� ���������� ������������
            string name = "�������� ��������";
            string quantity = "100";
            string price = "500,50"; // ������� ��� ���������� �����������

            // Act
            bool result = ValidateMaterial(name, quantity, price);

            // Assert
            Assert.IsTrue(result, "��������� ������ �������� � ������� ��� ���������� ������������");
        }

        [TestMethod]
        public void ValidateForm_EmptyName_ReturnsFalse()
        {
            // Arrange
            string name = "";
            string quantity = "100";
            string price = "500.50";

            // Act
            bool result = ValidateMaterial(name, quantity, price);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateForm_NegativeQuantity_ReturnsFalse()
        {
            // Arrange
            string name = "�������� ��������";
            string quantity = "-10";
            string price = "500.50";

            // Act
            bool result = ValidateMaterial(name, quantity, price);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateForm_NegativePrice_ReturnsFalse()
        {
            // Arrange
            string name = "�������� ��������";
            string quantity = "100";
            string price = "-50.00";

            // Act
            bool result = ValidateMaterial(name, quantity, price);

            // Assert
            Assert.IsFalse(result);
        }

        private bool ValidateMaterial(string name, string quantity, string price)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            if (!int.TryParse(quantity, out int quantityValue) || quantityValue < 0)
                return false;

            // ���������� ������������ �������� ��� �������� �����
            if (!decimal.TryParse(price, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal priceValue) || priceValue < 0)
                return false;

            return true;
        }
    }
}