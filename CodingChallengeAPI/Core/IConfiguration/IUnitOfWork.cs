using CodingChallengeAPI.Core.IRepositories;

namespace CodingChallengeAPI.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IAccountReportsiroty Account { get; }
        IDepositReportsiroty Deposit { get; }
        IOutstandingReposiroty Outstanding { get; }
        ITransferTransactionsReposiroty TransferTransactions { get; }
        Task CompleteAsync();
    }
}
