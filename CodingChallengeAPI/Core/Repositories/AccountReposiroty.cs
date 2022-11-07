using CodingChallengeAPI.Core.IRepositories;
using CodingChallengeAPI.Data;
using CodingChallengeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingChallengeAPI.Core.Repositories
{
    public class AccountReposiroty : GenericRepository<Account>, IAccountReportsiroty
    {
        public AccountReposiroty(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Account>> All()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo All method error}", typeof(AccountReposiroty));
                return new List<Account>();
            }
        }

        public override async Task<bool> Update(Account entity)
        {
            try
            {
                var existsingAccount = await _dbSet.Where(x => x.AccountId == entity.AccountId).FirstOrDefaultAsync();
                if (existsingAccount == null)
                {
                    return await Add(entity);
                }

                existsingAccount.AccountName = entity.AccountName;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo Update method error}", typeof(AccountReposiroty));
                return false;
            }
        }

        public override async Task<bool> Delete(long id)
        {
            try
            {
                var exist = await _dbSet.Where(x => x.AccountId == id).FirstOrDefaultAsync();
                if (exist != null)
                {
                    _dbSet.Remove(exist);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo Delete method error}", typeof(AccountReposiroty));
                return false;
            }
        }


    }
}
