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
using OnlineAuction.BLL.DTO;

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
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
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
            category.ParentCategory = _fixture.Create<Category>();
            _fixture.RepeatCount = 1;
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
            category.ParentCategory = _fixture.Create<Category>();
            var catList = new List<Category>();
            catList.Add(category);
            _unitOfWork.Category.Get(id).Returns(category);
            _unitOfWork.Lot.Find(i => { return true; }).ReturnsForAnyArgs(new List<Lot>());
            _unitOfWork.Category.Find(i => { return true; }).ReturnsForAnyArgs(catList);

            var ex = Assert.Throws<OperationFaildException>(() => _adminService.DeleteCategory(id));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Category is parent for other"));
        }
        [Test]
        public void DeleteCategory_should_stop_if_category_with_id_not_fount()
        {
            _unitOfWork.Category.Get(id).Returns(l => null);

            _adminService.DeleteCategory(id);

            _unitOfWork.DidNotReceiveWithAnyArgs().Lot.Find(default);
        }
        [Test]
        public void DeleteCategory_delete_category()
        {
            var category = _fixture.Create<Category>();
            category.ParentCategory = _fixture.Create<Category>();
            _unitOfWork.Category.Get(id).Returns(category);
            _unitOfWork.Lot.Find(i => { return true; }).ReturnsForAnyArgs(new List<Lot>());
            _unitOfWork.Category.Find(i => { return true; }).ReturnsForAnyArgs(new List<Category>());

            _adminService.DeleteCategory(id);

            _unitOfWork.Received().Save();
        }
        [Test]
        public void UpdateCategory_should_stop_if_name_empty()
        {
            string name = " ";
            
            _adminService.UpdateCategory(id, name);

            _unitOfWork.Category.DidNotReceiveWithAnyArgs().Find(default);
        }
        [Test]
        public void UpdateCategory_should_throw_OperationFaildException_when_category_with_id_not_fount()
        {
            _unitOfWork.Category.Get(id).Returns(c => null);

            var ex = Assert.Throws<OperationFaildException>(() => _adminService.UpdateCategory(id,"A"));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Category not found"));
        }
        [Test]
        public void UpdateCategory_should_throw_OperationFaildException_when_parentCategory_is_null()
        {
            var category = _fixture.Build<Category>().Without(c => c.ParentCategory).Create();
            _unitOfWork.Category.Get(id).Returns(category);

            var ex = Assert.Throws<OperationFaildException>(() => _adminService.UpdateCategory(id, "A"));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Cant update Main category"));
        }
        [Test]
        public void UpdateCategory_should_throw_OperationFaildException_when_Category_with_name_already_exists()
        {
            var category = _fixture.Create<Category>();
            _fixture.RepeatCount = 1;
            var catList = _fixture.CreateMany<Category>();
            category.ParentCategory = catList.First();
            _unitOfWork.Category.Get(id).Returns(category);
            _unitOfWork.Category.Find(default).ReturnsForAnyArgs(catList);

            var ex = Assert.Throws<OperationFaildException>(() => _adminService.UpdateCategory(id, "A"));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Category already exists"));
        }
        [Test]
        public void UpdateCategory_update_category()
        {
            var category = _fixture.Create<Category>();
            category.ParentCategory = _fixture.Create<Category>();
            _unitOfWork.Category.Get(id).Returns(category);
            _unitOfWork.Category.Find(default).ReturnsForAnyArgs(new List<Category>());

            _adminService.UpdateCategory(id, "A");

            _unitOfWork.Received().Save();
        }

        [Test]
        public void AddCategory_throw_ValidationException_if_category__without_name()
        {
            string name = " ";
            var category = _fixture.Build<CategoryDTO>().With(c =>c.Name, name).Create();

            var ex = Assert.Throws<OnlineAuction.BLL.Infrastructure.ValidationException>(() => _adminService.AddCategory(category));

            Assert.That(ex.Message, Is.Not.Empty);
        }
        [Test]
        public void AddCategory_should_stop_if_category_null()
        {
            CategoryDTO category = null;

            var ex = Assert.Throws<ArgumentNullException>(() => _adminService.AddCategory(category));

            Assert.That(ex.Message, Is.EqualTo("Category is null"));
        }
        [Test]
        public void AddCategory_should_stop_if_category__already_exists()
        {
            var category = _fixture.Create<CategoryDTO>();
            var list = _fixture.CreateMany<Category>();
            _unitOfWork.Category.Find(default).ReturnsForAnyArgs(list);

            _adminService.AddCategory(category);

            _unitOfWork.DidNotReceive().Save();

        }
        [Test]
        public void AddCategory_add_category()
        {
            var category = _fixture.Create<CategoryDTO>();
            _unitOfWork.Category.Find(default).ReturnsForAnyArgs(new List<Category>());

            _adminService.AddCategory(category);

            _unitOfWork.Received().Save();
        }

        [Test]
        public void DeleteManeger_should_stop_if_maneger_with_id_not_fount()
        {
            _unitOfWork.AdvancedUser.Get(default).ReturnsForAnyArgs(m => null);

            _adminService.DeleteManeger(id);

            _unitOfWork.AdvancedUser.DidNotReceive().Delete(default);
        }
        [Test]
        public void DeleteManeger_throw_OperationFaildException_if_maneger_is_admin_()
        {
            var user = _fixture.Build<AdvancedUser>().With(a => a.Admin, true).Create();
            _unitOfWork.AdvancedUser.Get(default).ReturnsForAnyArgs(user);

            var ex = Assert.Throws<OperationFaildException>(() => _adminService.DeleteManeger(id));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Is not manager Id"));
        }
        [Test]
        public void DeleteManeger_delete_maneger()
        {
            var user = _fixture.Build<AdvancedUser>().With(a => a.Admin, false).Create();
            _unitOfWork.AdvancedUser.Get(default).ReturnsForAnyArgs(user);

            _adminService.DeleteManeger(id);

            _unitOfWork.Received().Save();
        }

        [Test]
        public void AddManager_throw_ValidationException_if_person_or_authentication_valid_error()
        {
            var persone= _fixture.Create<PersonDTO>();
            var authentication=_fixture.Create<AuthenticationDTO>();

            var ex = Assert.Throws<ValidationException>(() => _adminService.AddManager(persone, authentication));

            Assert.That(ex.Message, Is.Not.Empty);
        }
        [Test]
        public void AddManager_throw_OperationFaildException_if_authentication_login_already_exists()
        {
            string login = "cake";
            var persone = _fixture.Create<PersonDTO>();
            persone.PhoneNumber = "0987098911";
            var authentication = _fixture.Build<AuthenticationDTO>().With(b=>b.Login,login).Create();
            _unitOfWork.Authentication.Find(default).ReturnsForAnyArgs(_fixture.CreateMany<Authentication>());

            var ex = Assert.Throws<OperationFaildException>(() => _adminService.AddManager(persone, authentication));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Login already exists"));
        }
        [Test]
        public void AddManager_complite()
        {
            string login = "cake";
            var persone = _fixture.Create<PersonDTO>();
            persone.PhoneNumber = "0987098911";
            var authentication = _fixture.Build<AuthenticationDTO>().With(b => b.Login, login).Create();
            _unitOfWork.Authentication.Find(default).ReturnsForAnyArgs(new List<Authentication>());

            _adminService.AddManager(persone, authentication);

            _unitOfWork.Received().Save();
        }
    }
}
