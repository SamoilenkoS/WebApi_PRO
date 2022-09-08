using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApi_BL;
using WebApi_BL.DTOs;

namespace WebApi_PRO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoodsController : ControllerBase
    {
        private readonly ILogger<GoodsController> _logger;
        private readonly IGoodsService _goodsService;
        private readonly SomeService _scopedExample;

        public GoodsController(
            ILogger<GoodsController> logger,
            IGoodsService goodsService,
            SomeService singletonExample)
        {
            _logger = logger;
            _goodsService = goodsService;
            _scopedExample = singletonExample;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Called {nameof(GetAll)}");
            var goods = await _goodsService.GetAll();

            return Ok(goods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var good = await _goodsService.GetById(id);

            return good != null ? Ok(good) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGood(CreateGoodDto good)
        {
            var goodDto = await _goodsService.Create(good);

            return Ok(goodDto);
        }

        [HttpGet("value/{id}")]
        public string GetValue(int id)
        {
            return $"In controller: {_scopedExample.Value} In goods service: {_goodsService.TestGet()}";
        }
    }
}
