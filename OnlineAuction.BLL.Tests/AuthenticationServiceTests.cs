using AutoFixture;
using AutoFixture.NUnit3;
using NSubstitute;
using NUnit.Framework;
using OnlineAuction.BLL.DTO;
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
    class AuthenticationServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private IAuthenticationService _authentication;
        private IUnitOfWork _unitOfWork;
        private int id = 1;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _authentication = new AuthenticationService(_unitOfWork);
        }
        [Test]
        public void GetAuthenticationId_should_return_0_when_authentication_is_null()
        {
            var result = _authentication.GetAuthenticationId(null);

            Assert.AreEqual(result, 0);
        }
        [Theory,AutoData]
        public void GetAuthenticationId_should_return_0_when_Find_return_empty_list(AuthenticationDTO authentication)
        {
            var result = _authentication.GetAuthenticationId(authentication);

            Assert.AreEqual(result, 0);
        }
        [Theory, AutoData]
        public void GetAuthenticationId_should_Id_when_Find_authentication(AuthenticationDTO authentication)
        {
            var aut = _fixture.Build<Authentication>().With(a => a.Login, authentication.Login)
                .With(a=>a.Password,authentication.Password).Create();
            var list = new List<Authentication>();
            list.Add(aut);
            _unitOfWork.Authentication.Find(default).ReturnsForAnyArgs(list);

            var result = _authentication.GetAuthenticationId(authentication);

            Assert.AreEqual(result , aut.Id);
        }
        [Theory, AutoData]
        public void IsAdvancedUserDTO_should_return_minus_1_when_advanceUser_not_found(int userId)
        {
            _unitOfWork.AdvancedUser.Get(userId).Returns(u=>null);

            var result = _authentication.IsAdvancedUserDTO(userId);

            Assert.AreEqual(result, -1);
        }
        [Theory, AutoData]
        public void IsAdvancedUserDTO_should_return_0_when_advanceUser_not_admin(int userId)
        {
            var user = _fixture.Build<AdvancedUser>().With(u => u.Admin, false).Create();
            _unitOfWork.AdvancedUser.Get(userId).Returns(user);

            var result = _authentication.IsAdvancedUserDTO(userId);

            Assert.AreEqual(result, 0);
        }
        [Theory, AutoData]
        public void IsAdvancedUserDTO_should_return_1_when_advanceUser_admin(int userId)
        {
            var user = _fixture.Build<AdvancedUser>().With(u => u.Admin, true).Create();
            _unitOfWork.AdvancedUser.Get(userId).Returns(user);

            var result = _authentication.IsAdvancedUserDTO(userId);

            Assert.AreEqual(result, 1);
        }
    }
}
