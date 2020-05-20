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
    class BoughLotServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private IBoughLotService _boughLotService;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _boughLotService = new BoughLotService(_unitOfWork);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Test]
        public void GetSels_should_return_null_when_user_not_found()
        {
            var result = _boughLotService.GetSales(default);

            Assert.IsNull(result);
        }
        [Test]
        public void GetSels_should_return_IEnumerable_LotViewDTO_when_user_found()
        {

            var user = _fixture.Build<User>().With(u => u.Sels, _fixture.CreateMany<Lot>().ToList()).Create();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            var result = _boughLotService.GetSales(default);

            Assert.AreEqual(result.Count(),user.Sels.Count());
        }
        [Test]
        public void GetSels_should_return_empty_IEnumerable_when_user_found_without_sels()
        {
            var user = _fixture.Build<User>().Without(u => u.Sels).Create();
            _unitOfWork.User.Get(default).ReturnsForAnyArgs(user);

            var result = _boughLotService.GetSales(default);

            Assert.AreEqual(result.Count(),0);
        }
        [Test]
        public void DeleteBought_should_stop_when_user_not_found()
        {
            _boughLotService.DeleteBought(default, default);

            _unitOfWork.DidNotReceive().Save();
        }
        [Test]
        public void DeleteBought_should_stop_when_lot_not_found()
        {
            var user = _fixture.Create<User>();
            _unitOfWork.User.Get(default).Returns(user);
            _boughLotService.DeleteBought(default, default);

            _unitOfWork.DidNotReceive().Save();
        }
        [Test]
        public void DeleteBought_should_completed_when_lot_and_user_found()
        {
            int lotid = 1;
            var lot = _fixture.Build<Lot>().With(l => l.Id, lotid).Create();
            var user = _fixture.Create<User>();
            user.Bought.Add(lot);
            _unitOfWork.User.Get(default).Returns(user);
            _boughLotService.DeleteBought(default, lotid);

            _unitOfWork.Received().Save();
        }
    }
}
