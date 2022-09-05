using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi_BL.DTOs;

namespace WebApi_BL
{
    public interface IGoodsService
    {
        Task<IEnumerable<GoodDto>> GetAll();
        Task<GoodDto> GetById(Guid id);
        Task<GoodDto> DeleteById(Guid id);
        Task<GoodDto> UpdateById(Guid id, GoodDto good);
        Task<GoodDto> Create(CreateGoodDto good);
        int TestGet();
    }
}
