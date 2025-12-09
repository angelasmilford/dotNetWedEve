using System.Collections.Generic;

namespace dotNetWedEve.Data
{
    public class Category
    {
        public int Id { get; set; }

        // Required properties
        public required string Name { get; set; }
        public required string Color { get; set; }

        // Navigation property — initialized to avoid nulls
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}

