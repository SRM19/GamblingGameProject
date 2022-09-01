using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GamblingGame.Models.DTO;
using GamblingGame.Repository;
using GamblingGame.Service;
using System.Security.Claims;

namespace GamblingGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GambleController : ControllerBase
    {
        private readonly ILogger<GambleController> _logger;
        protected ResponseDto _response;
        private IGambleService _service;
        public GambleController(IGambleService service, ILogger<GambleController> logger)
        {
            _logger = logger;
            _service = service;
            _response = new ResponseDto();
        }

        /// <summary>
        /// Gamble game method
        /// </summary>
        /// <param name="gambleInput"></param>
        /// <returns>GambleResult</returns>
        [Authorize]
        [HttpPost]
        public async Task<object> Gamble([FromBody]GambleInput gambleInput)
        {
            try
            {
                string userId = User.Identity.Name;
                int luckyNum = _service.GenerateLuckyNumber();
                GambleResult result = await _service.Gamble(gambleInput, userId, luckyNum);
                _response.Result = result;
                if (result.Status == "Won")
                    _response.DisplayMessage = string.Format("{0} {1}",userId, Constants.WinMessage);
                else
                    _response.DisplayMessage = string.Format("{0} {1}", userId, Constants.LoseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, "Exception occured");
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return _response;
            
        }

    }
}
