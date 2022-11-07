using CodingChallengeAPI.Core.IConfiguration;
using CodingChallengeAPI.Data;
using CodingChallengeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingChallengeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly ILogger<DepositController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public DepositController(ILogger<DepositController> logger
                                , IUnitOfWork unitOfWork)

        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        [Route("DepositMoney")]
        public async Task<IActionResult> Deposit(Deposit deposit)
        {

            var account = await _unitOfWork.Account.GetById(deposit.AccountId ?? 0);
            if (account == null)
                return NotFound();

            if (deposit.Amount <= 0)
                return new JsonResult("Something went wrong") { StatusCode = 500 };

            var outstanding = await _unitOfWork.Outstanding.GetById(deposit.AccountId ?? 0);
            if (outstanding == null)
                return NotFound();

            outstanding.Amount = outstanding.Amount + deposit.Amount;
            await _unitOfWork.Deposit.Add(deposit);
            await _unitOfWork.Outstanding.Update(outstanding);
            await _unitOfWork.CompleteAsync();
            return Ok(deposit);
        }



    }
}
