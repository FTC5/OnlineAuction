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
        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<DeliveryAndPayment> DeliveryAndPayment { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Lot> Lot { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Moderation> Moderation { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
