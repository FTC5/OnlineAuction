using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ProductModel
    {
        private ICollection<ImageModel> image;
        public virtual ICollection<ImageModel> Images
        {
            get { return image; }
            set { image = value; }
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryModel Category { get; set; }
        public virtual DeliveryAndPaymentModel DeliveryAndPayment { get; set; }
        public virtual LotModel Lot { get; set; }
    }
}
