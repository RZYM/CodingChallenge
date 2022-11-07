using CodingChallengeAPI.Core.IConfiguration;
using CodingChallengeAPI.Core.IRepositories;
using CodingChallengeAPI.Core.Repositories;

namespace CodingChallengeAPI.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public IAccountReportsiroty Account { get; private set; }
        public IDepositReportsiroty Deposit { get; private set; }
        public IOutstandingReposiroty Outstanding { get; private set; }
        public ITransferTransactionsReposiroty TransferTransactions { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            Account = new AccountReposiroty(_context, _logger);
            Deposit = new DepositReposiroty(_context, _logger);
            Outstanding = new OutstandingReposiroty(_context, _logger);
            TransferTransactions = new TransferTransactionsReposiroty(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
