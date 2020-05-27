using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.BLL.Services;
using OnlineAuction.DAL.Interfaces;
using OnlineAuction.DAL;
using OnlineAuction.BLL.Interfaces;
using AutoFixture;
using OnlineAuction.BLL.DTO;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.BLL.Tests
{
    [TestFixture]
    class AdminServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private IManagerManagementService _adminService;
        private IValidationCheckService _validation;
        private IUnitOfWork _unitOfWork;
        private int id = 1;
       
        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _validation = Substitute.For<IValidationCheckService>();
            _adminService = new AdminService(_unitOfWork, _validation);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Test]
        public void DeleteManeger_should_stop_if_maneger_with_id_not_fount()
        {
            _unitOfWork.AdvancedUser.Get(default).ReturnsForAnyArgs(m => null);

            _adminService.DeleteManeger(id);

            _unitOfWork.AdvancedUser.DidNotReceive().Delete(default);
        }
        [Test]
        public void DeleteManeger_throw_OperationFaildException_if_maneger_is_admin_()
        {
            var user = _fixture.Build<AdvancedUser>().With(a => a.Admin, true).Create();
            _unitOfWork.AdvancedUser.Get(default).ReturnsForAnyArgs(user);

            var ex = Assert.Throws<OperationFaildException>(() => _adminService.DeleteManeger(id));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Is not manager Id"));
        }
        [Test]
        public void DeleteManeger_complete()
        {
            var user = _fixture.Build<AdvancedUser>().With(a => a.Admin, false).Create();
            _unitOfWork.AdvancedUser.Get(default).ReturnsForAnyArgs(user);

            _adminService.DeleteManeger(id);

            _unitOfWork.Received().Save();
        }

        [Test]
        public void AddManager_throw_ValidationException_if_ValidationResult__result_count_not_zero()
        {
            var list = new List<ValidationResult>();
            list.Add(_fixture.Create<ValidationResult>());
            _validation.Check<PersonDTO>(default).Returns(list);
            _validation.Check<AuthenticationDTO>(default).Returns(list);

            var ex = Assert.Throws<ValidationDTOException>(() => _adminService.AddManager(default, default));

            Assert.That(ex.Message, Is.Not.Empty);
        }
        [Test]
        public void AddManager_throw_OperationFaildException_if_authentication_login_already_exists()
        {
            var persone = _fixture.Create<PersonDTO>();
            var authentication = _fixture.Create<AuthenticationDTO>();
            var list = _fixture.CreateMany<Authentication>();
            var emptyList = new List<ValidationResult>();
            _validation.Check<PersonDTO>(default).ReturnsForAnyArgs(emptyList);
            _validation.Check<AuthenticationDTO>(default).ReturnsForAnyArgs(emptyList);
            _unitOfWork.Authentication.Find(default).ReturnsForAnyArgs(list);

            var ex = Assert.Throws<OperationFaildException>(() => _adminService.AddManager(persone, authentication));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Login already exists"));
        }
        [Test]
        public void AddManager_complite()
        {
            var persone = _fixture.Create<PersonDTO>();
            var authentication = _fixture.Create<AuthenticationDTO>();
            _unitOfWork.Authentication.Find(default).ReturnsForAnyArgs(new List<Authentication>());
            var emptyList = new List<ValidationResult>();
            _validation.Check<PersonDTO>(default).ReturnsForAnyArgs(emptyList);
            _validation.Check<AuthenticationDTO>(default).ReturnsForAnyArgs(emptyList);

            _adminService.AddManager(persone, authentication);

            _unitOfWork.Received().Save();
        }
    }
}
