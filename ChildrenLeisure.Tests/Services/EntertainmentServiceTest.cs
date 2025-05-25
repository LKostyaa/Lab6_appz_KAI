using Xunit;
using Moq;
using AutoMapper;
using ChildrenLeisure.BLL.Services;
using ChildrenLeisure.DAL.Entities;
using ChildrenLeisure.DAL.Interfaces;
using ChildrenLeisure.BLL.Mapping;
using System.Collections.Generic;
using System.Linq;

public class EntertainmentServiceTests
{
    [Fact]
    public void GetAllAttractions_ShouldReturnCorrectDtos()
    {
        var mockUow = new Mock<IUnitOfWork>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        var mapper = config.CreateMapper();

        mockUow.Setup(u => u.AttractionRepository.GetAll())
            .Returns(new List<Attraction>
            {
                new Attraction { Id = 1, Name = "Гірка", Price = 100 }
            }.AsQueryable());

        var service = new EntertainmentService(mockUow.Object, mapper);
        var result = service.GetAllAttractions();

        Assert.Single(result);
        Assert.Equal("Гірка", result[0].Name);
    }
}
