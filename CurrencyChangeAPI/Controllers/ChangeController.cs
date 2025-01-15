using CurrencyChangeAPI.Swagger_Examples;
using CurrencyChangeAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace CurrencyChangeAPI.Controllers;

[ApiController]
public class ChangeController : ControllerBase
{
    /// <summary>
    /// Calculates the change for a given paid amount and product price.
    /// </summary>
    /// <param name="paidAmount">The amount paid by the customer.</param>
    /// <param name="productPrice">The price of the product.</param>
    /// <returns>The calculated change or an appropriate message.</returns>
    /// <response code="200">Returns the change as a list of denominations.</response>
    /// <response code="400">If the paid amount is less than the product price.</response>
    [HttpGet("api/getchange")]
    [ProducesResponseType(typeof(ChangeResponse), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [SwaggerResponseExample(200, typeof(ChangeResponseExample))]
    [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
    public IActionResult GetChange(decimal paidAmount, decimal productPrice)
    {
        if (paidAmount < productPrice)
        {
            return BadRequest("Paid amount is less than the product price.");
        }

        var change = ChangeCalculator.CalculateChange(paidAmount, productPrice);

        if (change.Count == 0)
        {
            return Ok("The Paid amount and Product Price are equal hence no change");
        }

        return Ok(new ChangeResponse
        {
            Message = "Your change is:",
            Change = change
        });
    }
}
