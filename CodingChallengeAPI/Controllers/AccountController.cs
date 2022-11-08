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
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(ILogger<AccountController> logger
                                , IUnitOfWork unitOfWork)

        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("ListAccount")]
        public async Task<IActionResult> ListAccount()
        {
            return Ok(await _unitOfWork.Account.All());
        }

        [HttpGet]
        [Route("GetAccountById")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _unitOfWork.Account.GetById(id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount(Account account)
        {
            var acc = await _unitOfWork.Account.All();
            if (acc.Count() > 0)
            {
                var exists = acc.Where(x => x.IBAN == account.IBAN).FirstOrDefault();
                if (exists != null)
                    return BadRequest();

            }

            if (ModelState.IsValid)
            {
                await _unitOfWork.Account.Add(account);
                await _unitOfWork.CompleteAsync();

                var ou = new Outstanding
                {
                    Amount = 0,
                    AccountId = account.AccountId
                };
                await _unitOfWork.Outstanding.Add(ou);
                await _unitOfWork.CompleteAsync();
                return Ok(account);
            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };

        }

        //[HttpPut]
        //[Route("UpdateAccount")]
        //public async Task<IActionResult> UpdateAccount(long id, Account account)
        //{
        //    if (id != account.AccountId)
        //        return BadRequest();

        //    await _unitOfWork.Account.Update(account);
        //    await _unitOfWork.CompleteAsync();
        //    return NoContent();

        //}

        [HttpDelete]
        [Route("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount(long id)
        {
            var account = await _unitOfWork.Account.GetById(id);
            if (account == null)
                return BadRequest();


            await _unitOfWork.Account.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok(id);

        }

    }
}
