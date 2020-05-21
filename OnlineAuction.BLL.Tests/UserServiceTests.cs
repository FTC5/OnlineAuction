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
            Assert.Throws<LotNotFoundExaption>(() => _userService.AddLotToSubscription(default, default));
        }
        [Test]
        public void AddLotToSubscription_throw_UserNotFoundExaption_when_user_not_found()
        {
            var lot = _fixture.Create<Lot>();
            _unitOfWork.Lot.Get(default).ReturnsForAnyArgs(lot);

            Assert.Throws<UserNotFoundExaption>(() => _userService.AddLotToSubscription(default, default));
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
            var user = _fixture.Build<User>().Without(u => u.Subscriptions).Create() ;
            var lot = _fixture.Create<Lot>();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);
            var result = _userService.GetSubscription(default);

            Assert.AreEqual(0, result.Count());
        }
        [Test]
        public void GetSubscription_return_Subscriptions()
        {
            var lot = _fixture.Create<Lot>();
            var user = _fixture.Create<User>();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            var result = _userService.GetSubscription(default);
            
            Assert.AreEqual(user.Subscriptions.Count(),result.Count());
        }
        [Test]
        public void DeleteSubscription_stop_when_user_not_found()
        {
            _userService.DeleteSubscription(default, default);

            _unitOfWork.DidNotReceive().Save();
        }
        [Test]
        public void GetSubscription_complete()
        {
            var user = _fixture.Create<User>();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            _userService.DeleteSubscription(default, default);

            _unitOfWork.Received().Save();
        }
        [Test]
        public void GetUserLot_return_null_when_user_not_found()
        {
            var result=_userService.GetUserLot(default);

            Assert.IsNull(result);
        }
        [Test]
        public void GetUserLot_return_empty_list_when_user_without_his_lots()
        {
            var user = _fixture.Build<User>().Without(u=>u.UserLots).Create();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            var result = _userService.GetUserLot(default);

            Assert.AreEqual(0,result.Count());
        }
        [Test]
        public void GetsUserLot_return__list_when_user_have_lots()
        {
            var lot = _fixture.Create<Lot>();
            var user = _fixture.Create<User>();
            user.UserLots.Add(lot);
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            var result = _userService.GetUserLot(default);

            Assert.AreEqual(user.UserLots.Count, result.Count());
        }

        [Test]
        public void UpdateLot_stop_when_change_lot_is_null()
        {
            int userId = 0;

            _userService.UpdateLot(userId, default);

            _unitOfWork.User.DidNotReceive().Get(userId);
        }
        [Test]
        public void UpdateLot_throw_UserNotFoundExaption_when_user_not_found()
        {
            int userId = 0;
            var lotDTO = _fixture.Create<LotDTO>();

            var ex = Assert.Throws<UserNotFoundExaption>(() => _userService.UpdateLot(userId, lotDTO)); ;

            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }
        [Test]
        public void UpdateLot_stop_and_start_create_lot_when_user_lot_with_change_lot_id_not_found()
        {
            var lotDTO = _fixture.Create<LotDTO>();
            var user = _fixture.Build<User>().Without(u => u.UserLots).Create();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            _userService.UpdateLot(default, lotDTO);

            _unitOfWork.Lot.DidNotReceiveWithAnyArgs().Update(default);
        }
        [Test]
        public void UpdateLot_stop_when_lot_moderation_result_true()
        {
            var user = _fixture.Create<User>();
            var lot = _fixture.Build<Lot>().With(l => l.ModerationResult, true).Create();
            var lotDTO = _fixture.Build<LotDTO>().With(ldto => ldto.Id, lot.Id).Create();
            lotDTO.UserId = user.Id;
            user.UserLots.Add(lot);
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            _userService.UpdateLot(default, lotDTO);

            _unitOfWork.Lot.DidNotReceiveWithAnyArgs().Update(default);
        }
        [Test]
        public void UpdateLot_complete()
        {
            var user = _fixture.Create<User>();
            var lot = _fixture.Build<Lot>().With(l => l.ModerationResult, false).Create();
            var lotDTO = _fixture.Build<LotDTO>().With(ldto => ldto.Id, lot.Id).Create();
            lotDTO.UserId = user.Id;
            user.UserLots.Add(lot);
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            _userService.UpdateLot(default, lotDTO);

            _unitOfWork.Received().Save();
        }
    }
}
