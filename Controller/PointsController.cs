using Microsoft.AspNetCore.Mvc;
using PointsApp.Models;
using PointsApp.Services;

namespace PointsApp.Controller
{
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly PointsService _pointsService;

        public PointsController(PointsService pointsService)
        {
            _pointsService = pointsService;
        }
        
        [Route("/add")]
        [HttpPost]
        public async Task<IActionResult> AddPointTransaction(PointTransactionDTO transactionDto)
        {
            // convert from DTO
            var transaction = new PointTransaction
            {
                Payer = transactionDto.Payer,
                Points = transactionDto.Points,
                AvailablePoints = transactionDto.Points,
                Timestamp = transactionDto.Timestamp,
            };

            try
            {
                await _pointsService.AddTransaction(transaction);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [Route("/spend")]
        [HttpPost]
        public async Task<ActionResult<SpendResult[]>> SpendPoints(SpendRequest request)
        {
            SpendResult[] result;
            try
            {
                result = await _pointsService.SpendPoints(request.Points);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            
            return Ok(result);
        }
        
        [Route("/balance")]
        [HttpGet]
        public async Task<ActionResult<ICollection<KeyValuePair<string, int>>>> GetPointsBalance()
        {
            return Ok(await _pointsService.GetPointsBalanceByPayer());
        }
    }
}
