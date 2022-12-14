using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_BL.DTOs;
using WebApi_DAL;
using WebApi_DAL.Entities;

namespace WebApi_BL
{
    public class GoodsService : IGoodsService
    {
        private readonly ILogger<GoodsService> _logger;
        private readonly IGoodsRepository _goodsRepository;
        private readonly SomeService _scopedExample;
        private readonly IMapper _mapper;

        public GoodsService(
            ILogger<GoodsService> logger,
            IGoodsRepository goodsRepository,
            SomeService scopedExample,
            IMapper mapper)
        {
            _logger = logger;
            _goodsRepository = goodsRepository;
            _scopedExample = scopedExample;
            _mapper = mapper;
        }

        public async Task<GoodDto> Create(CreateGoodDto goodDto)
        {
            if(goodDto.Price < 0)
            {
                throw new ArgumentException("Price should be greater than zero!");
            }

            var good = _mapper.Map<Good>(goodDto);

            var goodFromDb = await _goodsRepository.Create(good);

            return _mapper.Map<GoodDto>(goodFromDb);
        }

        public int TestGet()
        {
            return _scopedExample.Value;
        }

        public async Task<GoodDto> UpdateById(Guid id, GoodDto good)
        {
            var response = await _goodsRepository.UpdateById(id, _mapper.Map<Good>(good));

            return _mapper.Map<GoodDto>(response);
        }

        public Task<GoodDto> DeleteById(Guid id)
        {
            return Task.FromResult(new GoodDto());
        }

        public async Task<IEnumerable<GoodDto>> GetAll()
        {
            var goods = await _goodsRepository.GetAll();

            _logger.LogDebug("Test debug!");
            _logger.LogInformation($"Got {goods.Count()} items");
            var response = _mapper.Map<IEnumerable<GoodDto>>(goods);

            return response;
        }

        public Task<GoodDto> GetById(Guid id)
        {
            return null;
        }
    }
}
