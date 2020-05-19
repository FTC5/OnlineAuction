using AutoFixture;
using AutoFixture.NUnit3;
using NSubstitute;
using NUnit.Framework;
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
    class ManagerServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private IManagerService _managerService;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _managerService = new ManagerService(_unitOfWork);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Test]
        public void PreventLot_return_throw_LotNotFoundExaption_when_lot_not_found()
        {
            var ex = Assert.Throws <LotNotFoundExaption> (() => _managerService.PreventLot(default,default));

            StringAssert.Contains("Lot with id=", ex.Message);
        }
        [Test]
        public void PreventLot_return_throw_ValidationException_when_comment_empty()
        {
            var lot = _fixture.Create<Lot>();
            _unitOfWork.Lot.Get(default).ReturnsForAnyArgs(lot);
            var ex = Assert.Throws<ValidationException>(() => _managerService.PreventLot(default, ""));

            Assert.That(ex.Message, Is.EqualTo("Write a comment, namely for what reason the lot was not accepted"));
        }
        [Theory,AutoData]
        public void PreventLot_completed(string comment)
        {
            var lot = _fixture.Create<Lot>();
            _unitOfWork.Lot.Get(default).ReturnsForAnyArgs(lot);

            _managerService.PreventLot(default, comment);

            _unitOfWork.Received().Save();
        }
       

    }
}
