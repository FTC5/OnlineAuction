using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IValidationCheckService
    {
        List<ValidationResult> Check<T>(T item);
    }
}
