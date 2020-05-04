using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL.Context
{
    public class DropCreateDBOnModelChanged : DropCreateDatabaseIfModelChanges<OnlineAuctionContext>
    {
        protected override void Seed(OnlineAuctionContext context)
        {
            List<string> categorys = new List<string>() { "Клеокціонування", "Мистецтво і Атикваріат", "Книги, Букіністика",
                "Мода та краса","Телефони та аксесуари","Електро техніка та техніка" };
            var mainCategory = new Category() { Name = "Main" };
            context.Category.Add(mainCategory);
            var cat = new Category() { Name = categorys[0], ParentCategory = mainCategory };
            context.Category.Add(cat);
            cat = new Category() { Name = categorys[1], ParentCategory = mainCategory };
            context.Category.Add(cat);
            for (int i = 2; i < categorys.Count; i++)
            {
                var category = new Category() { Name = categorys[i], ParentCategory = mainCategory };
                context.Category.Add(category);
            }

            List<string> firstNames = new List<string>() { "Kari", "Terry", "Dan", "Peter" };
            List<string> lastNames = new List<string>() { "Hensien", "Adams", "Park", "Houston" };
            var authenticationAdmin = new Authentication() { Login = "Cacus", Password = "qwerty" };
            var authenticationUser1 = new Authentication() { Login = "User1", Password = "qwerty" };
            var authenticationUser2 = new Authentication() { Login = "User2", Password = "qwerty" };
            var authenticationManager = new Authentication() { Login = "Manager", Password = "qwerty" };
            context.Authentication.Add(authenticationAdmin);
            context.Authentication.Add(authenticationUser1);
            context.Authentication.Add(authenticationUser2);
            context.Authentication.Add(authenticationManager);
            var user1 = new User() { FirstName = firstNames[0], LastName = lastNames[0], DateOfBirth = "-", PhoneNumber = 0964573341, Authentication = authenticationUser1 };
            var user2 = new User() { FirstName = firstNames[1], LastName = lastNames[1], DateOfBirth = "-", PhoneNumber = 0964573342, Authentication = authenticationUser2 };
            var admin = new AdvancedUser() { FirstName = firstNames[2], LastName = lastNames[2], DateOfBirth = "-", PhoneNumber = 0964573343, Authentication = authenticationAdmin,Admin=true};
            var manager = new AdvancedUser() { FirstName = firstNames[3], LastName = lastNames[3], DateOfBirth = "-", PhoneNumber = 0964573344, Authentication = authenticationManager };
            context.User.Add(user1);
            context.User.Add(user2);
            context.AdvancedUser.Add(admin);
            context.AdvancedUser.Add(manager);
            var image = new Image() { Link = "https://media.istockphoto.com/photos/fisherman-caught-a-boot-on-spoonbait-picture-id153986903" };
            var del = new DeliveryAndPayment { CostOfdelivery = "0", PurchaseReturns = "-", SendingAbroad = "-",DeliveryMethod="-" };
            var product = new Product() { Category = cat, DeliveryAndPayment = del, Location = "XD", Description = "-", Price = 100, Name = "Ботинок На Ложкибейт" };
            var lot = new Lot() { MinimumStroke = 100, Product = product, TermDay = 6, User = user1 };
            var moderation = new Moderation() { Lot = lot };
            lot.Moderation = moderation;
            product.Lot = lot;
            del.Product = product;
            image.Product = product;
            context.Product.Add(product);
            context.Image.Add(image);
            context.DeliveryAndPayment.Add(del);
            context.Lot.Add(lot);
            context.Moderation.Add(moderation);
            context.SaveChanges(); 
        }
    }
}
