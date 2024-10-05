using PointsApp.Models;

namespace PointsApp.Services;

public interface IPointTransactionRepository
{
    Task AddTransaction(PointTransaction transaction);
    Task<List<PointTransaction>> GetTransactionsChronological();
    Task<List<PointTransaction>> GetTransactionsByPayerChronological(string payer);
    Task<Dictionary<string, int>> GetPointsBalanceByPayer();
    Task<int> GetTotalPointBalance();
    Task SaveChanges();
}