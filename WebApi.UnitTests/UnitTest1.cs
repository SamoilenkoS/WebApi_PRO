using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using WebApi_BL;
using WebApi_BL.DTOs;
using WebApi_DAL;
using WebApi_DAL.Entities;

namespace WebApi.UnitTests
{
    public class GoodsServiceTests
    {
        private Mock<IGoodsRepository> _goodsRepositoryMock;
        private Mock<ILogger<GoodsService>> _loggerMock;
        private Mock<SomeService> _someService;
        private Mock<IMapper> _mapperMock;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<GoodsService>>();
            _goodsRepositoryMock = new Mock<IGoodsRepository>();
            _someService = new Mock<SomeService>();
            _mapperMock = new Mock<IMapper>();
            _fixture = new Fixture();
        }

        [Test]
        public async Task Create_WhenValidItemPassed_ShouldCallCreateInRepository()
        {
            var goodsService = new GoodsService(
                _loggerMock.Object,
                _goodsRepositoryMock.Object,
                _someService.Object,
                _mapperMock.Object);
            int price = 20;
            var createGoodDto = _fixture.Create<CreateGoodDto>();
            createGoodDto.Price = price;
            var goodFromMapper = _fixture.Create<Good>();
            goodFromMapper.Price = price;
            var goodFromRepository = _fixture.Create<Good>();
            goodFromRepository.Price = price;
            var expected = _fixture.Create<GoodDto>();
            expected.Price = price;

            _mapperMock.Setup(x => x.Map<Good>(createGoodDto))
                .Returns(goodFromMapper)
                .Verifiable();
            _goodsRepositoryMock.Setup(x => x.Create(goodFromMapper))
                .ReturnsAsync(goodFromRepository)
                .Verifiable();
            _mapperMock.Setup(x => x.Map<GoodDto>(goodFromRepository))
                .Returns(expected)
                .Verifiable();

            var actual = await goodsService.Create(createGoodDto);

            actual.Should().BeEquivalentTo(expected);
            _goodsRepositoryMock.Verify();
            _mapperMock.Verify();
        }

        [Test]
        public void Create_WhenPriceTooLowItemPassed_ShouldThrowArgumentException()
        {
            var goodsService = new GoodsService(
                _loggerMock.Object,
                _goodsRepositoryMock.Object,
                _someService.Object,
                _mapperMock.Object);
            int price = -1;
            var createGoodDto = _fixture.Create<CreateGoodDto>();
            createGoodDto.Price = price;

            Assert.ThrowsAsync<ArgumentException>(async ()
                => await goodsService.Create(createGoodDto));

            _mapperMock.Verify(x => x.Map<It.IsAnyType>(It.IsAny<object>()), Times.Never);
            _goodsRepositoryMock.Verify(x => x.Create(It.IsAny<Good>()), Times.Never);
        }
    }
}