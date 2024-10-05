using PointsApp.Models;

namespace PointsApp.Services;

public interface IPointsService
{ 
    Task AddTransaction(PointTransaction transaction);
    Task<SpendResult[]> SpendPoints(int points);
    Task<Dictionary<string, int>> GetPointsBalanceByPayer();
}