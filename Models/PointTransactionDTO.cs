namespace PointsApp.Models;

public class PointTransactionDTO
{
    public required string Payer { get; set; }
    public int Points { get; set; }
    public DateTime Timestamp { get; set; }
}