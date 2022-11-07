using CodingChallengeAPI.Core.IRepositories;
using CodingChallengeAPI.Data;
using CodingChallengeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingChallengeAPI.Core.Repositories
{
    public class TransferTransactionsReposiroty : GenericRepository<TransferTransactions>, ITransferTransactionsReposiroty
    {
        public TransferTransactionsReposiroty(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }

    }
}
