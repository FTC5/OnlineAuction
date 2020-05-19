using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.BLL.Services;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Tests
{
    class RegistrationServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private IRegistrationService _registrationService;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _registrationService= new RegistrationService(_unitOfWork);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Test]
        public void AuthorizationRegistration_throw_ValidationException_when_login_empty()
        {
            var password = _fixture.Create<string>(); 

            Assert.Throws<ValidationException>(() => _registrationService.AuthorizationRegistration("", password));

        }
        [Test]
        public void AuthorizationRegistration_throw_ValidationException_when_login_small()
        {
            var password = _fixture.Create<string>();
            var login = "A";

            Assert.Throws<ValidationException>(() => _registrationService.AuthorizationRegistration(login, password));

        }
        [Test]
        public void AuthorizationRegistration_throw_ValidationException_when_password_small()
        {
            var password ="aaa";
            var login = _fixture.Create<string>();

            Assert.Throws<ValidationException>(() => _registrationService.AuthorizationRegistration(login, password));

        }
        [Test]
        public void AuthorizationRegistration_throw_OperationFaildException_when_login_already_exists()
        {
            var password = _fixture.Create<string>();
            var aut = _fixture.CreateMany<Authentication>();
            var login = _fixture.Create<string>();
            _unitOfWork.Authentication.Find(default).ReturnsForAnyArgs(aut);

            Assert.Throws<OperationFaildException>(() => _registrationService.AuthorizationRegistration(login, password));

        }
        [Test]
        public void AuthorizationRegistration_complited()
        {
            var password = _fixture.Create<string>();
            var login = _fixture.Create<string>();
            var list = new List<Authentication>();
            _unitOfWork.Authentication.Find(default).ReturnsForAnyArgs(new List<Authentication>());

            _registrationService.AuthorizationRegistration(login, password);

            _unitOfWork.Received().Save();
        }
        [Test]
        public void UserRegistration_throw_ValidationException_when_person_data_incorrect()
        {
            var person = _fixture.Create<PersonDTO>();

            Assert.Throws<ValidationException>(() => _registrationService.UserRegistration(default, person));

        }
        [Test]
        public void UserRegistration_throw_ArgumentNullException_when_authenticationId_not_found()
        {
            var person = _fixture.Build<PersonDTO>().With(p => p.PhoneNumber, "0007099921").Create();

            var ex = Assert.Throws<OperationFaildException>(() => _registrationService.UserRegistration(default, person));

            Assert.That(ex.Message, Is.EqualTo("Authorization not found"));
        }
        [Test]
        public void UserRegistration_complete()
        {
            var person = _fixture.Build<PersonDTO>().With(p => p.PhoneNumber, "0007099921").Create();
            _unitOfWork.Authentication.Get(default).ReturnsForAnyArgs(_fixture.Create<Authentication>());

            _registrationService.UserRegistration(default, person);

            _unitOfWork.User.ReceivedWithAnyArgs().Create(default);
        }
    }

   
}
