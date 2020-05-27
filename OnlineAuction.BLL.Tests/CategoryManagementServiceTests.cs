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
    class CategoryManagementServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private ICategoryManagementService _categoryManagementService;
        private IValidationCheckService _validation;
        private IUnitOfWork _unitOfWork;
        private int id = 1;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _validation = Substitute.For<IValidationCheckService>();
            _categoryManagementService = new CategoryManagementService(_unitOfWork, _validation);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Test]
        public void DeleteCategory_should_throw_OperationFaildException_when_parentCategory_is_null()
        {
            var category = _fixture.Build<Category>().Without(c => c.ParentCategory).Create();
            _unitOfWork.Category.Get(id).Returns(category);

            var ex = Assert.Throws<OperationFaildException>(() => _categoryManagementService.DeleteCategory(id));

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

            var ex = Assert.Throws<OperationFaildException>(() => _categoryManagementService.DeleteCategory(id));

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

            var ex = Assert.Throws<OperationFaildException>(() => _categoryManagementService.DeleteCategory(id));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Category is parent for other"));
        }
        [Test]
        public void DeleteCategory_should_stop_if_category_with_id_not_fount()
        {
            _unitOfWork.Category.Get(id).Returns(l => null);

            _categoryManagementService.DeleteCategory(id);

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

            _categoryManagementService.DeleteCategory(id);

            _unitOfWork.Received().Save();
        }
        [Test]
        public void UpdateCategory_should_stop_if_name_empty()
        {
            string name = " ";

            _categoryManagementService.UpdateCategory(id, name);

            _unitOfWork.Category.DidNotReceiveWithAnyArgs().Find(default);
        }
        [Test]
        public void UpdateCategory_should_throw_OperationFaildException_when_category_with_id_not_fount()
        {
            _unitOfWork.Category.Get(id).Returns(c => null);

            var ex = Assert.Throws<OperationFaildException>(() => _categoryManagementService.UpdateCategory(id, "A"));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Category not found"));
        }
        [Test]
        public void UpdateCategory_should_throw_OperationFaildException_when_parentCategory_is_null()
        {
            var category = _fixture.Build<Category>().Without(c => c.ParentCategory).Create();
            _unitOfWork.Category.Get(id).Returns(category);

            var ex = Assert.Throws<OperationFaildException>(() => _categoryManagementService.UpdateCategory(id, "A"));

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

            var ex = Assert.Throws<OperationFaildException>(() => _categoryManagementService.UpdateCategory(id, "A"));

            Assert.That(ex.Message, Is.EqualTo("Operation Failed : Category already exists"));
        }
        [Test]
        public void UpdateCategory_update_category()
        {
            string name = "A";
            var category = _fixture.Create<Category>();
            category.ParentCategory = _fixture.Create<Category>();
            _unitOfWork.Category.Get(id).Returns(category);
            _unitOfWork.Category.Find(default).ReturnsForAnyArgs(new List<Category>());

            _categoryManagementService.UpdateCategory(id, name);

            _unitOfWork.Received().Save();
        }

        [Test]
        public void AddCategory_throw_ValidationException_if_ValidationResult__result_count_not_zero()
        {
            var list = new List<ValidationResult>();
            list.Add(_fixture.Create<ValidationResult>());
            _validation.Check<CategoryDTO>(default).ReturnsForAnyArgs(list);
            var category = _fixture.Create<CategoryDTO>();

            var ex = Assert.Throws<ValidationDTOException>(() => _categoryManagementService.AddCategory(category));

            Assert.That(ex.Message, Is.Not.Empty);
        }
        [Test]
        public void AddCategory_should_stop_if_category_null()
        {
            CategoryDTO category = null;

            _categoryManagementService.AddCategory(category);

            _unitOfWork.Category.DidNotReceiveWithAnyArgs().Find(default);
        }
        [Test]
        public void AddCategory_should_stop_if_category__already_exists()
        {
            _validation.Check<CategoryDTO>(default).ReturnsForAnyArgs(new List<ValidationResult>());
            var category = _fixture.Create<CategoryDTO>();
            var list = _fixture.CreateMany<Category>();
            _unitOfWork.Category.Find(default).ReturnsForAnyArgs(list);

            _categoryManagementService.AddCategory(category);

            _unitOfWork.DidNotReceive().Save();

        }
        [Test]
        public void AddCategory_can_complete_if_category_dont_have_parent_category()
        {
            _validation.Check<CategoryDTO>(default).ReturnsForAnyArgs(new List<ValidationResult>());
            var category = _fixture.Create<CategoryDTO>();
            _unitOfWork.Category.Find(default).ReturnsForAnyArgs(new List<Category>());

            _categoryManagementService.AddCategory(category);

            _unitOfWork.Received().Save();

        }
        [Test]
        public void AddCategory_can_complete_if_category_have_parent_category()
        {
            _validation.Check<CategoryDTO>(default).ReturnsForAnyArgs(new List<ValidationResult>());
            var category = _fixture.Create<CategoryDTO>();
            var categoryParent= _fixture.Create<Category>();
            _unitOfWork.Category.Find(default).ReturnsForAnyArgs(new List<Category>());
            _unitOfWork.Category.Get(default).ReturnsForAnyArgs(categoryParent);

            _categoryManagementService.AddCategory(category);

            _unitOfWork.Received().Save();

        }
    }
}
