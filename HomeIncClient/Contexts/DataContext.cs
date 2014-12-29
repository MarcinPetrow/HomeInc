using HomeIncClient.Models;
using System.Data.Entity;

namespace HomeIncClient.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
    }
}