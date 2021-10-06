using System;
using System.Net.Http;
using System.Threading.Tasks;
using Management.CrossCuttingConcerns.Logging.ElasticSearch.Abstract;
using Management.Domain.Log;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ElasticSearchTestController : ControllerBase
    {
        private readonly ILogService _logService;

        public ElasticSearchTestController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            Random rnd = new Random();
            var log = new LogModel
            {
                IpAddress = "189.34.231.234",
                ShortMessage = "Test logu basıldı" + rnd.Next(0, 28),
                FullMessage = "Error logları basılıyor adasa",
                QueryString = "?q=test",
                CreatedOnUtc = DateTime.UtcNow,
                RequestUrl = "/ElasticSearchTest/Test",
                LogLevelId = 2,
                Id = Guid.NewGuid().ToString()
            };

            await _logService.InsertLog(log);
            return Ok("Log basıldı");
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _logService.GetLogsAsync();
            return Ok(list);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string id)
        {
            var log = await _logService.Find(id);
            return Ok(log);
        }

        [HttpGet]
        public async Task<IActionResult> GetListByLevelId([FromQuery] int levelId)
        {
            var list = await _logService.GetLogsByLevelId(levelId);
            return Ok(list);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LogModel logModel)
        {
            await _logService.UpdateLog(logModel);
            return Ok(true);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var log = await _logService.Find(id);
            await _logService.DeleteAsync(log);
            return Ok(true);
        }

    }
}