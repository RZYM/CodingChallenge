using CodingChallengeAPI.Core.IConfiguration;
using CodingChallengeAPI.Data;
using CodingChallengeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingChallengeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OutstandingController : ControllerBase
    {

        private readonly ILogger<OutstandingController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public OutstandingController(ILogger<OutstandingController> logger
                                , IUnitOfWork unitOfWork)

        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Route("ListOutstanding")]
        public async Task<IActionResult> ListOutstanding()
        {
            var outstanding = await _unitOfWork.Outstanding.All();
            await _unitOfWork.Account.All();
            return Ok(outstanding);
        }

        [HttpGet]
        [Route("GetOutstandingByAccountId")]
        public async Task<IActionResult> GetOutStandingById(long id)
        {
            var outstanding = await _unitOfWork.Outstanding.GetById(id);
            if (outstanding == null)
                return NotFound();

            outstanding.Account = await _unitOfWork.Account.GetById(outstanding.AccountId ?? 0);

            return Ok(outstanding);
        }


    }
}
