using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public GoodsService(
            IGoodsRepository goodsRepository,
            SomeService scopedExample)
        {
            _goodsRepository = goodsRepository;
            _scopedExample = scopedExample;
        }

        public async Task<GoodDto> Create(CreateGoodDto goodDto)
        {
            if(goodDto.Price < 0)
            {
                throw new ArgumentException("Price should be greater than zero!");
            }

            var good = new Good
            {
                CreatedAt = DateTime.Now,
                Price = goodDto.Price,
                Title = goodDto.Title
            };

            var goodFromDb = await _goodsRepository.Create(good);

            return new GoodDto
            {
                Id = goodFromDb.Id,
                Price = goodFromDb.Price,
                Title = goodFromDb.Title,
                CreatedAt = goodFromDb.CreatedAt
            };
        }

        public Task<Good> DeleteById(Guid id)
        {
            return _goodsRepository.DeleteById(id);
        }

        public Task<IEnumerable<Good>> GetAll()
        {
            return _goodsRepository.GetAll();
        }

        public Task<Good> GetById(Guid id)
        {
            return _goodsRepository.GetById(id);
        }

        public int TestGet()
        {
            return _scopedExample.Value;
        }

        public Task<Good> UpdateById(Guid id, Good good)
        {
            return _goodsRepository.UpdateById(id, good);
        }

        public Task<GoodDto> UpdateById(Guid id, GoodDto good)
        {
            throw new NotImplementedException();
        }

        Task<GoodDto> IGoodsService.DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<GoodDto>> IGoodsService.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<GoodDto> IGoodsService.GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
