using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi_BL.DTOs;
using WebApi_DAL;
using WebApi_DAL.Entities;

namespace WebApi_BL
{
    public class GoodsService : IGoodsService
    {
        private readonly IGoodsRepository _goodsRepository;
        private readonly SomeService _scopedExample;
        private readonly IMapper _mapper;

        public GoodsService(
            IGoodsRepository goodsRepository,
            SomeService scopedExample,
            IMapper mapper)
        {
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

        public Task<GoodDto> UpdateById(Guid id, GoodDto good)
        {
            throw new NotImplementedException();
        }

        public Task<GoodDto> DeleteById(Guid id)
        {
            return Task.FromResult(new GoodDto());
        }

        public async Task<IEnumerable<GoodDto>> GetAll()
        {
            var goods = await _goodsRepository.GetAll();

            return null;
        }

        public Task<GoodDto> GetById(Guid id)
        {
            return null;
        }
    }
}
