namespace PointsApp.Models;

public class PointTransaction
{
    public long Id { get; set; }
    public required string Payer { get; set; }
    public int Points { get; set; }
    public int AvailablePoints { get; set; }
    public DateTime Timestamp { get; set; }
}