using CurrencyChangeAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyChangeAPITest;

public class ChangeControllerTests
{
    private readonly ChangeController _controller;

    public ChangeControllerTests()
    {
        _controller = new ChangeController();
    }

    [Fact]
    public void GetChange_ExactPayment_ReturnsNoChangeMessage()
    {
        // Arrange
        decimal paidAmount = 100m;
        decimal productPrice = 100m;

        // Act
        var result = _controller.GetChange(paidAmount, productPrice) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("The Paid amount and Product Price are equal hence no change", result.Value);
    }

    [Fact]
    public void GetChange_OverPayment_ReturnsCorrectChange()
    {
        // Arrange
        decimal paidAmount = 100m;
        decimal productPrice = 76.35m;

        // Act
        var result = _controller.GetChange(paidAmount, productPrice) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);

        var expectedChange = new List<string>
            {
                "1 x £20",
                "1 x £2",
                "1 x £1",
                "1 x 50p",
                "1 x 10p",
                "1 x 5p"
            };

        var response = result.Value as dynamic;
        Assert.Equal("Your change is:", response?.Message);
        Assert.Equal(expectedChange, response?.Change);
    }

    [Fact]
    public void GetChange_InsufficientPayment_ReturnsBadRequest()
    {
        // Arrange
        decimal paidAmount = 40m;
        decimal productPrice = 50m;

        // Act
        var result = _controller.GetChange(paidAmount, productPrice) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Paid amount is less than the product price.", result.Value);
    }

    [Fact]
    public void GetChange_SmallDenominations_ReturnsCorrectChange()
    {
        // Arrange
        decimal paidAmount = 1.41m;
        decimal productPrice = 1m;

        // Act
        var result = _controller.GetChange(paidAmount, productPrice) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);

        var expectedChange = new List<string>
            {
                "2 x 20p",
                "1 x 1p"
            };

        var response = result.Value as dynamic;
        Assert.Equal("Your change is:", response?.Message);
        Assert.Equal(expectedChange, response?.Change);
    }
}
