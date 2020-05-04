using System.Data.Entity;

namespace OnlineAuction.DAL.Context
{
    public class OnlineAuctionContext : DbContext
    {
        static OnlineAuctionContext()
        {
            Database.SetInitializer<OnlineAuctionContext>(null);
        }
        public OnlineAuctionContext(string connectionString = "OnlineAuctionDB") : base(connectionString)
        {

        }
        public virtual DbSet<Authentication> Authentication { get; set; }
        public virtual DbSet<Bet> Bet { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<DeliveryAndPayment> DeliveryAndPayment { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Lot> Lot { get; set; }
        public virtual DbSet<AdvancedUser> AdvancedUser { get; set; }
        public virtual DbSet<Moderation> Moderation { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Lot>().HasRequired(s => s.Product)
                .WithRequiredPrincipal(ad => ad.Lot);
            modelBuilder.Entity<Moderation>().HasRequired(s => s.Lot)
               .WithRequiredPrincipal(ad => ad.Moderation);
            modelBuilder.Entity<Bet>()
                .HasRequired<User>(u => u.User)
                .WithMany(b => b.Bets)
                .HasForeignKey<int>(s => s.UserId);
        }
    }
}
