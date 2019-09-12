using BreakAway.Services;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using AutoFixture;
using BreakAway.Entities;
using System.Linq;
using System;

namespace Tests.Services
{
    [TestFixture]
    public class FilterServiceTests
    {
        Mock<IContactFilter> _mockContactFilter;
        Fixture _fixture;

        [SetUp]
        public void Initialize()
        {
            _mockContactFilter = new Mock<IContactFilter>(MockBehavior.Strict);
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public void FilterService_can_be_setup()
        {
            //Arrange
            var contactFilters = new List<IContactFilter>
            {
                _mockContactFilter.Object,
            };

            //Act
            var service = new FilterService(contactFilters);

            //Assert
            Assert.IsNotNull(service);
        }

        [Test]
        public void FilterService_throw_exception_if_filters_are_null()
        {
            //Arrange
            //Act
            //Arrange
            Assert.Throws<ArgumentNullException>(() => new FilterService(null));
        }

        [Test]
        public void GetfilteredContacts_returns_all_contacts_if_filters_are_empty()
        {
            //Arrange
            var contactFilters = new List<IContactFilter>();
            var service = new FilterService(contactFilters);
            var contacts = _fixture.CreateMany<Contact>();

            //Act
            var result = service.GetFilteredContacts(contacts.AsQueryable(), It.IsAny<FilterModel>(),1,25,out int totalPages);

            //Assert
            Assert.That(result, Has.Length.EqualTo(contacts.Count()));
        }

        [Test]
        public void GetFilteredContacts_donot_call_filterContact_if_filters_are_empty()
        {
            //Arrange
            var contactFilters = new List<IContactFilter>();
            var service = new FilterService(contactFilters);
            var contacts = _fixture.CreateMany<Contact>();

            //Act
            var result = service.GetFilteredContacts(contacts.AsQueryable(), It.IsAny<FilterModel>(), 1, 1, out int totalPages);

            //Assert
            _mockContactFilter.Verify(c => c.GetFilteredContacts(It.IsAny<IQueryable<Contact>>(), It.IsAny<FilterModel>()), Times.Never);
        }

        [Test]
        public void GetFilteredContacts_returns_all_contacts_if_CanFilter_return_false()
        {
            //Arrange                       
            _mockContactFilter.Setup(p => p.CanFilter(It.IsAny<FilterModel>())).Returns(false);

            var filters = new List<IContactFilter>()
            {
                _mockContactFilter.Object
            };

            var sut = new FilterService(filters);

            var contacts = _fixture.CreateMany<Contact>();

            //Act 
            var result = sut.GetFilteredContacts(contacts.AsQueryable(), null, 1, 25, out int totalPages);

            //Assert
            Assert.That(result, Has.Length.EqualTo(contacts.Count()));
        }

        [Test]
        public void GetFilteredContacts_calls_filterContacts_if_CanFilter_returns_true()
        {
            //Arrange                       
            var contacts = _fixture.CreateMany<Contact>();
            _mockContactFilter.Setup(p => p.CanFilter(It.IsAny<FilterModel>())).Returns(true);
            _mockContactFilter.Setup(p => p.GetFilteredContacts(It.IsAny<IQueryable<Contact>>(), It.IsAny<FilterModel>())).Returns(contacts.AsQueryable());

            var filters = new List<IContactFilter>()
            {
                _mockContactFilter.Object
            };

            var sut = new FilterService(filters);

            //Act 
            var result = sut.GetFilteredContacts(contacts.AsQueryable(), null, 1, 1, out int totalPages);

            //Assert
            _mockContactFilter.Verify(p => p.GetFilteredContacts(It.IsAny<IQueryable<Contact>>(), It.IsAny<FilterModel>()), Times.Once);
        }

        //??
        [Test]
        public void GetFilteredContacts_base_case_test()
        {
            //Arrange
            var contactFilters = new List<IContactFilter>()
            {
                _mockContactFilter.Object,
                _mockContactFilter.Object
            };
            var service = new FilterService(contactFilters);
            var contacts = _fixture.CreateMany<Contact>();
            var model = new Mock<FilterModel>().Object;

            _mockContactFilter.Setup(p => p.CanFilter(It.IsAny<FilterModel>())).Returns(true);
            _mockContactFilter.Setup(p => p.GetFilteredContacts(It.IsAny<IQueryable<Contact>>(), It.IsAny<FilterModel>())).Returns(contacts.AsQueryable());

            //Act
            var result = service.GetFilteredContacts(contacts.AsQueryable(), model, 1, 1, out int totalPages);

            //Assert
            Assert.That(result, Has.Length.AtLeast(0));
        }
    }
}
