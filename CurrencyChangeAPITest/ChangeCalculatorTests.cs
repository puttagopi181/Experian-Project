using CurrencyChangeAPI.Utilities;

namespace CurrencyChangeAPITest
{
    public class ChangeCalculatorTests
    {
        [Fact]
        public void CalculateChange_ExactPayment_ReturnsEmptyList()
        {
            // Arrange
            decimal paidAmount = 100m;
            decimal productPrice = 100m;

            // Act
            var result = ChangeCalculator.CalculateChange(paidAmount, productPrice);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void CalculateChange_OverPayment_ReturnsCorrectChange()
        {
            // Arrange
            decimal paidAmount = 100m;
            decimal productPrice = 76.35m;

            // Act
            var result = ChangeCalculator.CalculateChange(paidAmount, productPrice);

            // Assert
            var expectedChange = new List<string>
            {
                "1 x £20",
                "1 x £2",
                "1 x £1",
                "1 x 50p",
                "1 x 10p",
                "1 x 5p"
            };
            Assert.Equal(expectedChange, result);
        }

        [Fact]
        public void CalculateChange_SmallDenominations_ReturnsCorrectChange()
        {
            // Arrange
            decimal paidAmount = 1.41m;
            decimal productPrice = 1m;

            // Act
            var result = ChangeCalculator.CalculateChange(paidAmount, productPrice);

            // Assert
            var expectedChange = new List<string>
            {
                "1 x 20p",
                "1 x 10p",
                "1 x 10p",
                "1 x 1p"
            };
            Assert.Equal(expectedChange, result);
        }

        [Fact]
        public void CalculateChange_NoChangeNeeded_ReturnsEmptyList()
        {
            // Arrange
            decimal paidAmount = 50m;
            decimal productPrice = 50m;

            // Act
            var result = ChangeCalculator.CalculateChange(paidAmount, productPrice);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void CalculateChange_InsufficientPayment_ReturnsEmptyList()
        {
            // Arrange
            decimal paidAmount = 40m;
            decimal productPrice = 50m;

            // Act
            var result = ChangeCalculator.CalculateChange(paidAmount, productPrice);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
