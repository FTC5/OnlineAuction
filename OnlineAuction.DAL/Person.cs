using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL
{
    public class Person
    {
        [Key,ForeignKey("Authentication")]
        public int Id { get; set; }
        [Column("FirstName", TypeName = "ntext")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Column("LastName", TypeName = "ntext")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Column("DateOfBirth", TypeName = "ntext")]
        [MaxLength(50)]
        public string DateOfBirth { get; set; }
        public int PhoneNumber { get; set; }
        public Authentication Authentication { get; set; }
    }
}
