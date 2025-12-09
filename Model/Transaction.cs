using System;

namespace dotNetWedEve.Data
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public class Transaction
    {
        public int Id { get; set; }

        // Required properties
        public required string Name { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public required TransactionType Type { get; set; }
        public DateTime Date { get; set; }

        // Optional notes
        public string? Notes { get; set; }

        // Navigation property — use null-forgiving operator
        public Category Category { get; set; } = null!;
    }
}
