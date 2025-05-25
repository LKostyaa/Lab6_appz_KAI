using Xunit;
using Moq;
using AutoMapper;
using ChildrenLeisure.BLL.Services;
using ChildrenLeisure.BLL.Interfaces;
using ChildrenLeisure.DAL.Entities;
using ChildrenLeisure.DAL.Interfaces;
using ChildrenLeisure.BLL.DTOs;
using ChildrenLeisure.BLL.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

public class OrderServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPricingService> _pricingServiceMock;
    private readonly IMapper _mapper;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _pricingServiceMock = new Mock<IPricingService>();

        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();

        _service = new OrderService(_unitOfWorkMock.Object, _pricingServiceMock.Object, _mapper);
    }

    [Fact]
    public void CreateOrder_ShouldReturnDtoWithTotalPrice()
    {
        var attractions = new List<Attraction>
        {
            new Attraction { Id = 1, Name = "Батути", Price = 100 },
            new Attraction { Id = 2, Name = "Карусель", Price = 200 }
        };

        _unitOfWorkMock.Setup(x => x.AttractionRepository.GetAll())
            .Returns(attractions.AsQueryable());

        _pricingServiceMock.Setup(x => x.CalculateOrderPrice(It.IsAny<OrderDto>()))
            .Returns(300);

        _unitOfWorkMock.Setup(x => x.OrderRepository.Add(It.IsAny<Order>()));

        var result = _service.CreateOrder("Іван", "0501234567", false, null, new[] { 1, 2 });

        Assert.NotNull(result);
        Assert.Equal(300, result.TotalPrice);
    }
}
