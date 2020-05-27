using AutoFixture;
using NSubstitute;
using NUnit.Framework;
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
    class CatalogServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private ICatalogService _catalogService;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _catalogService = new CatalogService(_unitOfWork);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Test]
        public void GetLot_should_return_null_when_lod_not_found()
        {
            int id = 0;

            var result = _catalogService.GetLot(id);

            Assert.IsNull(result);
        }
        [Test]
        public void GetLot_should_return_lotDTO_when_lod_found()
        {
            var lot = _fixture.Create<Lot>();
            _unitOfWork.Lot.Get(lot.Id).Returns(lot);

            var result = _catalogService.GetLot(lot.Id);

            Assert.AreEqual(result.Id,lot.Id);
        }
    }
}
