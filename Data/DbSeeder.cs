using Microsoft.EntityFrameworkCore;

namespace dotNetWedEve.Data
{
    public static class DbSeeder
    {
        public static async Task Seed(dotNetWedEveContext db)
        {
            // Ensure database is created
            await db.Database.EnsureCreatedAsync();

            // -------------------
            // 1. Seed Categories
            // -------------------
            if (!db.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Food", Color = "Red" },
                    new Category { Name = "Transport", Color = "Blue" },
                    new Category { Name = "Entertainment", Color = "Green" },
                    new Category { Name = "Salary", Color = "Orange" },
                    new Category { Name = "Misc", Color = "Grey" }
                };

                db.Categories.AddRange(categories);
                await db.SaveChangesAsync();
            }

            // -------------------
            // 2. Seed Transactions
            // -------------------
            if (!db.Transactions.Any())
            {
                var categories = await db.Categories.ToListAsync();
                var transactions = new List<Transaction>();

                for (int month = 1; month <= 12; month++)
                {
                    var dateBase = new DateTime(2025, month, 5);

                    // Income
                    transactions.Add(new Transaction
                    {
                        Name = $"Salary {dateBase:MMMM}",
                        Amount = 5000 + month * 50,
                        Type = TransactionType.Income,
                        CategoryId = categories.First(c => c.Name == "Salary").Id,
                        Date = dateBase
                    });

                    // Expenses
                    transactions.Add(new Transaction
                    {
                        Name = "Groceries",
                        Amount = 250 + month * 5,
                        Type = TransactionType.Expense,
                        CategoryId = categories.First(c => c.Name == "Food").Id,
                        Date = dateBase.AddDays(3)
                    });

                    transactions.Add(new Transaction
                    {
                        Name = "Bus Pass",
                        Amount = 80 + month,
                        Type = TransactionType.Expense,
                        CategoryId = categories.First(c => c.Name == "Transport").Id,
                        Date = dateBase.AddDays(7)
                    });

                    transactions.Add(new Transaction
                    {
                        Name = "Movie/Entertainment",
                        Amount = 40 + month * 2,
                        Type = TransactionType.Expense,
                        CategoryId = categories.First(c => c.Name == "Entertainment").Id,
                        Date = dateBase.AddDays(10)
                    });

                    transactions.Add(new Transaction
                    {
                        Name = "Coffee/Misc",
                        Amount = 20 + month,
                        Type = TransactionType.Expense,
                        CategoryId = categories.First(c => c.Name == "Misc").Id,
                        Date = dateBase.AddDays(12)
                    });
                }

                db.Transactions.AddRange(transactions);
                await db.SaveChangesAsync();
            }
        }
    }
}
