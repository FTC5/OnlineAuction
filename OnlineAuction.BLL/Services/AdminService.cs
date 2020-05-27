using AutoMapper;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.BLL.Services
{
    public class AdminService :Service, IManagerManagementService
    {
        IValidationCheckService validation;
        public AdminService(IUnitOfWork db,IValidationCheckService validation) : base(db)
        {
            this.validation = validation;
        }
        public IEnumerable<AdvancedUserDTO> GetManegers()
        {
            var manegers = db.AdvancedUser.Find(m => !m.Admin);
            return mapper.Map<IEnumerable<AdvancedUserDTO>>(manegers);
        }
        public void DeleteManeger(int id)
        {
            var maneger = db.AdvancedUser.Get(id);
            if (maneger == null)
                return;
            if (maneger.Admin==true)
                throw new OperationFaildException("Operation Failed : Is not manager Id");
            db.Authentication.Delete(id);
            db.Save();
        }
        public void AddManager(PersonDTO person,AuthenticationDTO authent)//validation
        {
            var results = validation.Check<PersonDTO>(person);
            results.AddRange(validation.Check<AuthenticationDTO>(authent));
            if (results.Count>0)
                throw new Infrastructure.ValidationDTOException("Can not add new manager", results);
              
            AdvancedUserDTO advUser = mapper.Map<AdvancedUserDTO>(person);
            advUser.Admin = false;
            string login = authent.Login;
            var aut = db.Authentication.Find(a => a.Login == login);
            if (aut.Count()!=0)
                throw new OperationFaildException("Operation Failed : Login already exists");

            var authentication = mapper.Map<Authentication>(authent);
            var user = mapper.Map<AdvancedUser>(advUser);
            user.Authentication = authentication;
            db.AdvancedUser.Create(user);
            db.Save();
        }
    }
}
