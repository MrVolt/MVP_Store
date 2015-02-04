using System.Data.Entity;

namespace Store.Model
{
    public class StoreModel : DbContext
    {
        public StoreModel()
            : base("name=StoreModel")
        {
            Database.SetInitializer<StoreModel>(null);
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<ContrAgents> ContrAgents { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<HistoryOfInvoice> HistoryOfInvoice { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoices>()
                .HasMany(e => e.HistoryOfInvoice)
                .WithRequired(e => e.Invoices)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);
        }
    }
}
