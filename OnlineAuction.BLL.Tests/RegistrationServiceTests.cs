using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using OnlineAuction.BLL.BusinessModels;
using OnlineAuction.BLL.DTO;
using OnlineAuction.BLL.Infrastructure;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.BLL.Services;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Tests
{
    class RegistrationServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private IRegistrationService _registrationService;
        private IUnitOfWork _unitOfWork;
        private IValidationCheckService _validation;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _validation = Substitute.For<IValidationCheckService>();
            _registrationService = new RegistrationService(_unitOfWork,_validation);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Test]
        public void AuthorizationRegistration_throw_ValidationException_when_validation_result_count_not_zero()
        {
            var list = new List<ValidationResult>();
            list.Add(_fixture.Create<ValidationResult>());
            _validation.Check<AuthenticationDTO>(default).ReturnsForAnyArgs(list);

            var ex=Assert.Throws<ValidationDTOException>(() => _registrationService.AuthorizationRegistration(default, default));

            Assert.IsNotNull(ex);
        }
        [Test]
        public void AuthorizationRegistration_throw_OperationFaildException_when_login_already_exists()
        {
            var aut = _fixture.CreateMany<Authentication>();
            _validation.Check<AuthenticationDTO>(default).ReturnsForAnyArgs(new List<ValidationResult>());
            _unitOfWork.Authentication.Find(default).ReturnsForAnyArgs(aut);

            var ex = Assert.Throws<OperationFaildException>(() => _registrationService.AuthorizationRegistration(default, default));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Login already exists"));

        }
        [Test]
        public void AuthorizationRegistration_complited()
        {
            _validation.Check<AuthenticationDTO>(default).ReturnsForAnyArgs(new List<ValidationResult>());
            _unitOfWork.Authentication.Find(default).ReturnsForAnyArgs(new List<Authentication>());

            _registrationService.AuthorizationRegistration(default, default);

            _unitOfWork.Received().Save();
        }
        [Test]
        public void UserRegistration_throw_ValidationException_when_validation_result_count_not_zero()
        {
            var list = new List<ValidationResult>();
            list.Add(_fixture.Create<ValidationResult>());
            _validation.Check<PersonDTO>(default).ReturnsForAnyArgs(list);

            var ex = Assert.Throws<ValidationDTOException>(() => _registrationService.UserRegistration(default, default));

            Assert.IsNotNull(ex);
        }
        [Test]
        public void UserRegistration_throw_OperationFaildException_when_authenticationId_not_found()
        {
            _validation.Check<PersonDTO>(default).ReturnsForAnyArgs(new List<ValidationResult>());

            var ex = Assert.Throws<OperationFaildException>(() => _registrationService.UserRegistration(default, default));

            Assert.That(ex.Message, Is.EqualTo("Authorization not found"));
        }
        [Test]
        public void UserRegistration_complete()
        {
            var person = _fixture.Create<PersonDTO>();
            _validation.Check<PersonDTO>(default).ReturnsForAnyArgs(new List<ValidationResult>());
            _unitOfWork.Authentication.Get(default).ReturnsForAnyArgs(_fixture.Create<Authentication>());

            _registrationService.UserRegistration(default, person);

            _unitOfWork.Received().Save();
        }
    }

   
}
