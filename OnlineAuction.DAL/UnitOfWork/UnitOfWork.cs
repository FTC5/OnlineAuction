using OnlineAuction.DAL.Context;
using OnlineAuction.DAL.Interfaces;
using OnlineAuction.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        IAdvancedRepositor<Authentication>  authentication;
        IRepository<Bet> bet;
        IAdvancedRepositor<Category> category;
        IRepository<DeliveryAndPayment> deliveryAndPayment;
        IRepository<Image> image;
        IAdvancedRepositor<Lot> lot;
        IAdvancedUserRepository advancedUser;
        IRepository<Moderation> moderation;
        IRepository<Product> product;
        IRepository<User> user;
        private OnlineAuctionContext db;
        private string connectionString;

        public UnitOfWork(string connectionString)
        {
            this.db = new OnlineAuctionContext(connectionString);
            this.connectionString = connectionString;
        }

        public IAdvancedRepositor<Authentication> Authentication
        {
            get
            {
                if (authentication == null)
                {
                    authentication = new AuthenticationRepository(connectionString);
                }
                return authentication;
            }
        }

        public IAdvancedRepositor<Category> Category
        {
            get
            {
                if (category == null)
                {
                    category = new CategoryReposytory(connectionString);
                }
                return category;
            }
        }

        public IRepository<DeliveryAndPayment> DeliveryAndPayment
        {
            get
            {
                if (deliveryAndPayment == null)
                {
                    deliveryAndPayment = new DeliveryAndPaymentRepository(connectionString);
                }
                return deliveryAndPayment;
            }
        }

        public IRepository<Image> Image
        {
            get
            {
                if (image == null)
                {
                    image = new ImageRepository(connectionString);
    			}
                return image;
            }
        }

        public IAdvancedRepositor<Lot> Lot 
        {
            get
            {
                if (lot == null)
                {
                    lot = new LotReposytory (connectionString);
    			}
                return lot;
            }
        }

        public IAdvancedUserRepository AdvancedUser
        {
            get
            {
                if (advancedUser == null)
                {
                    advancedUser = new AdvancedUserRepository(connectionString);
    			}
                return advancedUser;
            }
        }

        public IRepository<Moderation> Moderation 
        {
            get
            {
                if (moderation == null)
                {
                    moderation = new ModerationRepository (connectionString);
    			}
                return moderation;
            }
        }

        public IRepository<Product> Product 
        {
            get
            {
                if (product == null)
                {
                    product = new ProductReposytory (connectionString);
    			}
                return product;
            }
        }

        public IRepository<User> User 
        {
            get
            {
                if (user == null)
                {
                    user = new UserRepository (connectionString);
    			}
                return user;
            }
        }

        public IRepository<Bet> Bet 
        {
            get
            {
                if (bet == null)
                {
                    bet = new BetRepository (connectionString);
    			}
                return bet;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        internal virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
