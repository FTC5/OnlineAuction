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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.Tests
{
    class UserLotServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private IUserLotService _userLotService;
        private IUnitOfWork _unitOfWork;
        private ICleanService _cleanService;
        private IValidationCheckService _validation;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _cleanService = Substitute.For<ICleanService>();
            _validation = Substitute.For<IValidationCheckService>();
            _userLotService = new UserLotService(_unitOfWork, _cleanService, _validation);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Test]
        public void GetUserLot_return_null_when_user_not_found()
        {
            var userId = 0;

            var result = _userLotService.GetUserLot(userId);

            Assert.IsNull(result);
        }
        [Test]
        public void GetUserLot_return_empty_list_when_user_without_his_lots()
        {
            var user = _fixture.Build<User>().Without(u => u.UserLots).Create();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            var result = _userLotService.GetUserLot(default);

            Assert.AreEqual(0, result.Count());
        }
        [Test]
        public void GetsUserLot_return__list_when_user_have_lots()
        {
            var lot = _fixture.Create<Lot>();
            var user = _fixture.Create<User>();
            user.UserLots.Add(lot);
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            var result = _userLotService.GetUserLot(default);

            Assert.AreEqual(user.UserLots.Count, result.Count());
        }

        [Test]
        public void UpdateLot_stop_when_change_lot_is_null()
        {
            int userId = 0;

            _userLotService.UpdateLot(userId, default);

            _unitOfWork.User.DidNotReceive().Get(userId);
        }
        [Test]
        public void UpdateLot_throw_UserNotFoundExaption_when_user_not_found()
        {
            int userId = 0;
            var lotDTO = _fixture.Create<LotDTO>();

            var ex = Assert.Throws<UserNotFoundExaption>(() => _userLotService.UpdateLot(userId, lotDTO)); ;

            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }
        [Test]
        public void UpdateLot_stop_and_start_create_lot_when_user_lot_with_change_lot_id_not_found()
        {
            var lotDTO = _fixture.Create<LotDTO>();
            var user = _fixture.Build<User>().Without(u => u.UserLots).Create();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);
            _validation.Check<LotDTO>(default).ReturnsForAnyArgs(new List<ValidationResult>());

            _userLotService.UpdateLot(default, lotDTO);

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

            _userLotService.UpdateLot(default, lotDTO);

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
            _validation.Check<LotDTO>(default).ReturnsForAnyArgs(new List<ValidationResult>());

            _userLotService.UpdateLot(default, lotDTO);

            _unitOfWork.Received().Save();
        }
    }
}
