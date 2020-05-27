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
    class UserServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private IUserService _userService;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _userService = new UserService(_unitOfWork);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Test]
        public void AddLotToSubscription_throw_LotNotFoundExaption_when_lot_not_found()
        {
            int userId = 0;
            int lotId = 0;

            var ex=Assert.Throws<LotNotFoundExaption>(() => _userService.AddLotToSubscription(userId, lotId));

            Assert.That(ex.Message, Is.EqualTo("Lot not found"));
        }
        [Test]
        public void AddLotToSubscription_throw_UserNotFoundExaption_when_user_not_found()
        {
            var lot = _fixture.Create<Lot>();
            _unitOfWork.Lot.Get(default).ReturnsForAnyArgs(lot);

            var ex = Assert.Throws<UserNotFoundExaption>(() => _userService.AddLotToSubscription(default, default));

            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }
        [Test]
        public void AddLotToSubscription_stop_when_user_subscribe_to_lot()
        {
            var user = _fixture.Create<User>();
            var lot = _fixture.Create<Lot>();
            user.Subscriptions.Add(lot);
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);
            _unitOfWork.Lot.Get(default).ReturnsForAnyArgs(lot);

            _userService.AddLotToSubscription(default, default);

            _unitOfWork.DidNotReceive().Save();
        }
        [Test]
        public void AddLotToSubscription_complete()
        {
            var user = _fixture.Create<User>();
            var lot = _fixture.Create<Lot>();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);
            _unitOfWork.Lot.Get(default).ReturnsForAnyArgs(lot);

            _userService.AddLotToSubscription(default, default);

            _unitOfWork.Received().Save();
        }
        [Test]
        public void GetSubscription_return_null_when_user_not_found()
        {
            var result=_userService.GetSubscription(default);

            Assert.IsNull(result);
        }
        [Test]
        public void GetSubscription_return_empty_list_when_user_dont_have_Subscriptions()
        {
            var user = _fixture.Build<User>().Without(u => u.Subscriptions).Create();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);
            var result = _userService.GetSubscription(default);

            Assert.AreEqual(0, result.Count());
        }
        [Test]
        public void GetSubscription_return_Subscriptions()
        {
            var user = _fixture.Create<User>();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            var result = _userService.GetSubscription(default);
            
            Assert.AreEqual(user.Subscriptions.Count(),result.Count());
        }
        [Test]
        public void DeleteSubscription_stop_when_user_not_found()
        {
            int userId = 0;
            int lotId = 0;

            _userService.DeleteSubscription(userId, lotId);

            _unitOfWork.DidNotReceive().Save();
        }
        [Test]
        public void DeleteSubscription_complete()
        {
            var user = _fixture.Create<User>();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            _userService.DeleteSubscription(default, default);

            _unitOfWork.Received().Save();
        }
    }
}
