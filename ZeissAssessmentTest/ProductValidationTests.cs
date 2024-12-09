using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeissAssessment;

namespace TestProject1
{
    public class ProductValidationTests
    {
        [Fact]
        public void ValidateStockDecrement_ShouldReturnFalse_WhenProductIsNull()
        {
            // Arrange
            Product product = null;
            int quantity = 5;
            string errorMessage;

            // Act
            var result = ProductValidation.ValidateStockDecrement(product, quantity, out errorMessage);

            // Assert
            Assert.False(result);
            Assert.Equal("Product not found.", errorMessage);
        }

        [Fact]
        public void ValidateStockDecrement_ShouldReturnFalse_WhenQuantityIsZeroOrNegative()
        {
            // Arrange
            var product = new Product { AvailableQuantity = 10 };
            int[] invalidQuantities = { 0, -1 };
            string errorMessage;

            foreach (var quantity in invalidQuantities)
            {
                // Act
                var result = ProductValidation.ValidateStockDecrement(product, quantity, out errorMessage);

                // Assert
                Assert.False(result);
                Assert.Equal("Quantity must be greater than zero.", errorMessage);
            }
        }

        [Fact]
        public void ValidateStockDecrement_ShouldReturnFalse_WhenInsufficientStock()
        {
            // Arrange
            var product = new Product { AvailableQuantity = 5 };
            int quantityToDecrement = 10;
            string errorMessage;

            // Act
            var result = ProductValidation.ValidateStockDecrement(product, quantityToDecrement, out errorMessage);

            // Assert
            Assert.False(result);
            Assert.Equal("Insufficient stock available.", errorMessage);
        }

        [Fact]
        public void ValidateStockDecrement_ShouldReturnTrue_WhenValidStockDecrement()
        {
            // Arrange
            var product = new Product { AvailableQuantity = 10 };
            int quantityToDecrement = 5;
            string errorMessage;

            // Act
            var result = ProductValidation.ValidateStockDecrement(product, quantityToDecrement, out errorMessage);

            // Assert
            Assert.True(result);
            Assert.Null(errorMessage);
        }

        [Fact]
        public void ValidateStockIncrement_ShouldReturnFalse_WhenQuantityIsZeroOrNegative()
        {
            // Arrange
            int[] invalidQuantities = { 0, -1 };
            string errorMessage;

            foreach (var quantity in invalidQuantities)
            {
                // Act
                var result = ProductValidation.ValidateStockIncrement(quantity, out errorMessage);

                // Assert
                Assert.False(result);
                Assert.Equal("Quantity must be greater than zero.", errorMessage);
            }
        }

        [Fact]
        public void ValidateStockIncrement_ShouldReturnTrue_WhenValidStockIncrement()
        {
            // Arrange
            int validQuantity = 5;
            string errorMessage;

            // Act
            var result = ProductValidation.ValidateStockIncrement(validQuantity, out errorMessage);

            // Assert
            Assert.True(result);
            Assert.Null(errorMessage);
        }
    }

}
