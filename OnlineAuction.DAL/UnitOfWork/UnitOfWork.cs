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
    public class UnitOfWork : IUnitOfWork
    {    
        AuthenticationRepository authentication;
        BetRepository bet;
        CategoryReposytory category;
        DeliveryAndPaymentRepository deliveryAndPayment;
        ImageRepository image;
        LotReposytory lot;
        AdvancedUserRepository advancedUser;
        ModerationRepository moderation;
        ProductReposytory product;
        UserRepository user;
        private OnlineAuctionContext db;

        public UnitOfWork(string connectionString)
        {
            this.db = new OnlineAuctionContext(connectionString);
        }

        public IRepository<Authentication> Authentication
        {
            get
            {
                if (authentication == null)
                {
                    authentication = new AuthenticationRepository(db);
                }
                return authentication;
            }
        }

        public IRepository<Bet> BetRepository
        {
            get
            {
                if (bet == null)
                {
                    bet = new BetRepository(db);
                }
                return bet;
            }
        }

        public IRepository<Category> Category
        {
            get
            {
                if (category == null)
                {
                    category = new CategoryReposytory(db);
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
                    deliveryAndPayment = new DeliveryAndPaymentRepository(db);
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
                    image = new ImageRepository(db);
    			}
                return image;
            }
        }

        public IRepository<Lot> Lot 
        {
            get
            {
                if (lot == null)
                {
                    lot = new LotReposytory (db);
    			}
                return lot;
            }
        }

        public IRepository<AdvancedUser> AdvancedUser
        {
            get
            {
                if (advancedUser == null)
                {
                    advancedUser = new AdvancedUserRepository(db);
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
                    moderation = new ModerationRepository (db);
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
                    product = new ProductReposytory (db);
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
                    user = new UserRepository (db);
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
                    bet = new BetRepository (db);
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
