using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IAuthenticationRepository Authentication { get ; }
        IRepository<Bet> Bet { get; }
        ICategoryRepository Category { get; }
        IRepository<DeliveryAndPayment> DeliveryAndPayment { get; }
        IRepository<Image> Image { get; }
        ILotRepository Lot { get; }
        IAdvancedUserRepository AdvancedUser { get; }
        IRepository<Moderation> Moderation { get; }
        IRepository<Product> Product { get; }
        IRepository<User> User { get; }

        void Save();
    }
}
