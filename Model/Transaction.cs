using System.Text.Json.Serialization;

namespace dotNetWedEve
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionType
    {
        Expense,
        Income
    }

    public class Transaction
    {
    public int Id { get; set; }  // PRIMARY KEY
    public required string Name { get; set; }
    public required decimal Amount { get; set; }

    public int CategoryId { get; set; }        // FK
    public Category? Category { get; set; }     // Navigation property

    public required TransactionType Type { get; set; }
    public DateTime Date { get; set; }
    public string? Notes { get; set; }
    }
}
