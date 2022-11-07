using CodingChallengeAPI.Core.IRepositories;
using CodingChallengeAPI.Data;
using CodingChallengeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingChallengeAPI.Core.Repositories
{
    public class OutstandingReposiroty : GenericRepository<Outstanding>, IOutstandingReposiroty
    {
        public OutstandingReposiroty(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<bool> Update(Outstanding entity)
        {
            try
            {
                var existsingOutstanding = await _dbSet.Where(x => x.AccountId == entity.AccountId).FirstOrDefaultAsync();
                if (existsingOutstanding != null)
                {
                    existsingOutstanding.Amount = entity.Amount;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo Update method error}", typeof(AccountReposiroty));
                return false;
            }
        }

    }
}
