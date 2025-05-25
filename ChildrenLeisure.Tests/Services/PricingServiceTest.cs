using Xunit;
using ChildrenLeisure.BLL.Services;
using ChildrenLeisure.BLL.DTOs;
using System.Collections.Generic;

public class PricingServiceTests
{
    [Fact]
    public void CalculateOrderPrice_ShouldSumCorrectly()
    {
        var service = new PricingService();
        var order = new OrderDto
        {
            IsBirthdayParty = true,
            FairyCharacter = new FairyCharacterDto { PricePerHour = 200 },
            SelectedAttractions = new List<AttractionDto>
            {
                new AttractionDto { Price = 100 },
                new AttractionDto { Price = 150 }
            }
        };

        var price = service.CalculateOrderPrice(order);
        Assert.Equal(500 + 100 + 150 + 200, price);
    }
}
