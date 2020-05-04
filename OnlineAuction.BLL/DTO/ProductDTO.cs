using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class ProductDTO
    {
        private ICollection<ImageDTO> image;
        public virtual ICollection<ImageDTO> Images
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
        public virtual CategoryDTO Category { get; set; }
        public virtual DeliveryAndPaymentDTO DeliveryAndPayment { get; set; }
        public virtual LotDTO Lot { get; set; }
    }
}
