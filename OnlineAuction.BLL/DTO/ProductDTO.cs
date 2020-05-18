using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Enter the name of the lot")]
        public string Name { get; set; }
        public int Price { get; set; } = 0;
        [Required(ErrorMessage = "Enter a description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Specify a location")]
        public string Location { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Lot dont have category")]
        public virtual CategoryDTO Category { get; set; }
        [Required(ErrorMessage = "Lot dont have delivery and payment")]
        public virtual DeliveryAndPaymentDTO DeliveryAndPayment { get; set; }
    }
}
