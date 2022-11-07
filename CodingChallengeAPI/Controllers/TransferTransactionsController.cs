using CodingChallengeAPI.Core.IConfiguration;
using CodingChallengeAPI.Data;
using CodingChallengeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingChallengeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransferTransactionsController : ControllerBase
    {

        private readonly ILogger<TransferTransactionsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public TransferTransactionsController(ILogger<TransferTransactionsController> logger
                                , IUnitOfWork unitOfWork)

        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        [Route("TransferMoney")]
        public async Task<IActionResult> TransferMoney(TransferTransactions transferTransactions)
        {
            if (transferTransactions.TransferFrom == transferTransactions.TransferTo)
                return BadRequest();

            var outstandingFrom = await _unitOfWork.Outstanding.GetById(transferTransactions.TransferFrom ?? 0);
            var outstandingTo = await _unitOfWork.Outstanding.GetById(transferTransactions.TransferTo ?? 0);
            if (outstandingFrom == null)
                return BadRequest();

            if (outstandingTo == null)
                return BadRequest();

            if (outstandingFrom.Amount < transferTransactions.TransferAmount)
                return BadRequest();


            await _unitOfWork.TransferTransactions.Add(transferTransactions);
            outstandingFrom.Amount = outstandingFrom.Amount - transferTransactions.TransferAmount;
            await _unitOfWork.Outstanding.Update(outstandingFrom);
            outstandingTo.Amount = outstandingTo.Amount + transferTransactions.TransferAmount;
            await _unitOfWork.Outstanding.Update(outstandingTo);
            await _unitOfWork.CompleteAsync();
            return Ok(transferTransactions);
        }
    }
}
