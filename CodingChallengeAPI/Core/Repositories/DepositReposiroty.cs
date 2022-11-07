using CodingChallengeAPI.Core.IRepositories;
using CodingChallengeAPI.Data;
using CodingChallengeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingChallengeAPI.Core.Repositories
{
    public class DepositReposiroty : GenericRepository<Deposit>, IDepositReportsiroty
    {
        public DepositReposiroty(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<bool> Add(Deposit entity)
        {
            try
            {
                entity.FeeAmount = ((entity.Amount * 0.1m) / 100.0m);
                entity.Amount = entity.Amount - ((entity.Amount * 0.1m) / 100.0m);
                _dbSet.Add(entity);
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo Add method error}", typeof(DepositReposiroty));
                return false;
            }
    
          
        }

    }
}
