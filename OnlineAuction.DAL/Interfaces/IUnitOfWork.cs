using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<Authentication> Authentication { get ; }
        IRepository<Bet> Bet { get; }
        IRepository<Category> Category { get; }
        IRepository<DeliveryAndPayment> DeliveryAndPayment { get; }
        IRepository<Image> Image { get; }
        IRepository<Lot> Lot { get; }
        IRepository<AdvancedUser> AdvancedUser { get; }
        IRepository<Moderation> Moderation { get; }
        IRepository<Product> Product { get; }
        IRepository<User> User { get; }

        void Save();
    }
}
