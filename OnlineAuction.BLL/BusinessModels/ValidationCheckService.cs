using OnlineAuction.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.BusinessModels
{
    public class ValidationCheckService : IValidationCheckService
    {
        public List<ValidationResult> Check<T>(T item)
        {
            var results = new List<ValidationResult>();
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(item);
            Validator.TryValidateObject(item, context, results, true);
            return results;
        }
    }
}
