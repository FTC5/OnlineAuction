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

namespace OnlineAuction.BLL.Tests
{
    [TestFixture]
    class AdminServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private IAdminService _adminService;
        private IUnitOfWork _unitOfWork;
        private int id = 1;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _adminService = new AdminService(_unitOfWork);
        }
        [Test]
        public void DeleteCategory_should_throw_OperationFaildException_when_parentCategory_is_null()
        {
            var category = _fixture.Build<Category>().Without(c => c.ParentCategory).Create();
            _unitOfWork.Category.Get(id).Returns(category);

            var ex=Assert.Throws<OperationFaildException>(() => _adminService.DeleteCategory(id));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Cant delete Main category"));
        }
        [Test]
        public void DeleteCategory_should_throw_OperationFaildException_when_category_use_in_lot()
        {
            var category = _fixture.Create<Category>();
            _fixture.RepeatCount = 1;
            var list = new List<Lot>();
            var lots = _fixture.CreateMany<Lot>();
            _unitOfWork.Category.Get(id).Returns(category);
            _unitOfWork.Lot.Find(i => { return true; }).ReturnsForAnyArgs(lots);

            var ex = Assert.Throws<OperationFaildException>(() => _adminService.DeleteCategory(id));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Lots with this category exists"));
        }
        [Test]
        public void DeleteCategory_should_throw_OperationFaildException_when_Category_have_chield_category()
        {
            var category = _fixture.Create<Category>();
            _fixture.RepeatCount = 1;
            var catList = new List<Category>();
            catList.Add(category);
            _unitOfWork.Category.Get(id).Returns(category);
            _unitOfWork.Lot.Find(i => { return true; }).ReturnsForAnyArgs(new List<Lot>());
            _unitOfWork.Category.Find(i => { return true; }).ReturnsForAnyArgs(catList);

            var ex = Assert.Throws<OperationFaildException>(() => _adminService.DeleteCategory(id));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Category is parent for other"));
        }
        [Test]
        public void DeleteCategory_should_stor_if_category_with_id_not_fount()
        {
            _unitOfWork.Category.Get(id).Returns(l => null);

            Assert.Throws<Exception>(() => _adminService.DeleteCategory(id));

            _unitOfWork.DidNotReceiveWithAnyArgs().Lot.Find(default);
        }
        [Test]
        public void DeleteCategory_delete_category()
        {
            var category = _fixture.Create<Category>();
            _unitOfWork.Category.Get(id).Returns(category);
            _unitOfWork.Lot.Find(i => { return true; }).ReturnsForAnyArgs(new List<Lot>());
            _unitOfWork.Category.Find(i => { return true; }).ReturnsForAnyArgs(new List<Category>());

            _adminService.DeleteCategory(id);

            _unitOfWork.Received().Save();
        }

        //[Test]
        //public void UpdateCategory_should_stor_if_category_with_id_not_fount()
        //{

        //}
        //[Test]
        //public void UpdateCategory_should_throw_OperationFaildException_when_parentCategory_is_null()
        //{

        //}
        //[Test]
        //public void UpdateCategory_should_throw_OperationFaildException_when_Category_with_name_already_exists()
        //{

        //}
        //[Test]
        //public void UpdateCategory_update_category()
        //{

        //}

        //[Test]
        //public void AddCategory_throw_ValidationException_if_category__without_name()
        //{

        //}
        //[Test]
        //public void AddCategory_should_stop_if_category_null()
        //{

        //}
        //[Test]
        //public void AddCategory_should_stop_if_category__already_exists()
        //{

        //}
        //[Test]
        //public void AddCategory_add_category()
        //{

        //}

        //[Test]
        //public void DeleteManeger_should_stop_if_maneger_with_id_not_fount()
        //{

        //}
        //[Test]
        //public void DeleteManeger_throw_OperationFaildException_if_maneger_is_admin_()
        //{

        //}
        //[Test]
        //public void DeleteManeger_delete_maneger()
        //{

        //}

        //[Test]
        //public void AddManager_throw_ValidationException_if_person_or_authentication_vaild_error()
        //{

        //}
        //[Test]
        //public void AddManager_throw_OperationFaildException_if_authentication_login_already_exists()
        //{

        //}
        //[Test]
        //public void AddManager_add_manager()
        //{

        //}
    }
}
