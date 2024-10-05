using Microsoft.EntityFrameworkCore;
using PointsApp.Models;

namespace PointsApp.Data.Repositories;

public class PointTransactionRepository
{
    private readonly ApplicationDbContext _context;

    public PointTransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddTransaction(PointTransaction transaction)
    {
        await _context.PointTransactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<List<PointTransaction>> GetTransactionsChronological()
    {
        return await _context.PointTransactions.OrderBy(g => g.Timestamp).ToListAsync();
    }

    public async Task<List<PointTransaction>> GetTransactionsByPayerChronological(string payer)
    {
        return await _context.PointTransactions
            .Where(g => g.Payer == payer)
            .OrderBy(g => g.Timestamp)
            .ToListAsync();
    }

    public async Task<Dictionary<string, int>> GetPointsBalanceByPayer()
    {
        return await _context.PointTransactions
            .GroupBy(transaction => transaction.Payer)
            .Select(g => new
            {
                Name = g.Key,
                Points = g.Sum(t => t.AvailablePoints)
            })
            .ToDictionaryAsync(g => g.Name, g => g.Points);
    }

    public async Task<int> GetTotalPointBalance()
    {
        return await _context.PointTransactions.SumAsync(g => g.AvailablePoints);
    }
    

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}