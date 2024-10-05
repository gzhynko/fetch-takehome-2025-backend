using PointsApp.Data.Repositories;
using PointsApp.Models;

namespace PointsApp.Services;

public class PointsService
{
    private readonly PointTransactionRepository _repository;

    public PointsService(PointTransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task AddTransaction(PointTransaction transaction)
    {
        if (transaction.Points < 0)
        {
            var pointsBalance = await GetPointsBalanceByPayer();
            if (!pointsBalance.ContainsKey(transaction.Payer) || pointsBalance[transaction.Payer] < Math.Abs(transaction.Points))
            {
                throw new ArgumentException("Unable to add transaction: not enough points on payer's balance");
            }
            
            var pointsRemainingToSubtract = Math.Abs(transaction.Points);
            var payerTransactions = await _repository.GetTransactionsByPayerChronological(transaction.Payer);
            for (var i = payerTransactions.Count - 1; i >= 0; i--)
            {
                if (pointsRemainingToSubtract == 0)
                    break;
                
                var availableToSubtract = Math.Min(payerTransactions[i].AvailablePoints, pointsRemainingToSubtract);
                payerTransactions[i].AvailablePoints -= availableToSubtract;
                pointsRemainingToSubtract -= availableToSubtract;
            }

            await _repository.SaveChanges();
            transaction.AvailablePoints = 0;
        }
        
        await _repository.AddTransaction(transaction);
    }
    
    public async Task<SpendResult[]> SpendPoints(int points)
    {
        var pointsAvailable = await _repository.GetTotalPointBalance();
        if (points > pointsAvailable)
        {
            throw new ArgumentException("Not enough points");
        }
        
        var spendBreakdown = new Dictionary<string, int>();
        var pointsRemaining = points;
        foreach (var transaction in await _repository.GetTransactionsChronological())
        {
            if (pointsRemaining == 0)
                break;
                
            var spendAmount = Math.Min(transaction.AvailablePoints, pointsRemaining);
                
            pointsRemaining -= spendAmount;
            transaction.AvailablePoints -= spendAmount;
            spendBreakdown[transaction.Payer] = spendBreakdown.TryGetValue(transaction.Payer, out var value)
                ? value - spendAmount
                : -spendAmount;
        }
            
        await _repository.SaveChanges();

        return spendBreakdown.ToList().Select(e => new SpendResult { Payer = e.Key, Points = e.Value }).ToArray();
    }

    public async Task<Dictionary<string, int>> GetPointsBalanceByPayer()
    {
        return await _repository.GetPointsBalanceByPayer();
    }
}