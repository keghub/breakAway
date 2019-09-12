using AutoFixture;
using BreakAway.Entities;
using BreakAway.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Services
{
    [TestFixture]
    public class FristNameContactFilterTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Initialise()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public void Can_be_setup()
        {
            //Arrange
            //Act
            var sut = new FirstNameContactFilter();

            //Assert
            Assert.IsNotNull(sut);
        }

        [Test]
        public void CanFilter_throw_exception_if_filter_is_null()
        {
            //Arrange
            var sut = new FirstNameContactFilter();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => sut.CanFilter(null));
        }

        [Test]
        public void CanFilter_return_false_if_firstname_is_null()
        {
            //Arrange
            var model = new FilterModel();
            var sut = new FirstNameContactFilter();

            //Act
            var result = sut.CanFilter(model);

            //Assert
            Assert.AreEqual(result, false);
        }

        [Test]
        public void CanFilter_return_false_if_firstname_is_whitespace()
        {
            //Arrange
            var model = new FilterModel();
            model.FirstName = " ";

            var sut = new FirstNameContactFilter();

            //Act
            var result = sut.CanFilter(model);

            //Assert
            Assert.AreEqual(result, false);
        }

        [Test]
        public void CanFilter_return_false_if_firstname_is_empty()
        {
            //Arrange
            var model = new FilterModel();
            model.FirstName = String.Empty;

            var sut = new FirstNameContactFilter();
            //Act
            var result = sut.CanFilter(model);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanFilter_return_true_on_firstname()
        {
            //Arrange
            var model = new FilterModel
            {
                FirstName = "Kevin"
            };

            var sut = new FirstNameContactFilter();
            //Act
            var result = sut.CanFilter(model);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GetFilteredContacts_throw_exception_if_contacts_are_null()
        {
            //Arrage
            var model = new FilterModel
            {
                FirstName = "K"
            };
            var sut = new FirstNameContactFilter();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => sut.GetFilteredContacts(null, model));
        }

        [Test]
        public void GetFilteredContacts_return_empty_if_contacts_are_empty()
        {
            //Arrange
            var contacts = new List<Contact>();

            var model = new FilterModel
            {
                FirstName = "K"
            };

            var filter = new FirstNameContactFilter();

            //Act
            var result = filter.GetFilteredContacts(contacts.AsQueryable(), model);

            //Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetFilteredContacts_return_empty_if_no_matching_firstname()
        {
            //Arrage
            var contact1 = new Contact
            {
                FirstName = "Kevin"
            };
            var contact2 = new Contact
            {
                FirstName = "Danfeng"
            };
            var contacts = new List<Contact>
            {
                contact1,
                contact2
            };

            var model = new FilterModel
            {
                FirstName = "b"
            };

            var filter = new FirstNameContactFilter();

            var filteredContacts = new List<Contact>
            {
                contact1
            };

            //Act
            var result = filter.GetFilteredContacts(contacts.AsQueryable(), model);

            //Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetFilteredContacts_return_contact_With_Uppercase_filter()
        {
            //Arrage
            var contacts = _fixture.CreateMany<Contact>();

            var model = new FilterModel
            {
                FirstName = contacts.First().FirstName.ToUpper()
            };

            var filter = new FirstNameContactFilter();

            var filteredContacts = new List<Contact>
            {
                contacts.First()
            };
            //Act
            var result = filter.GetFilteredContacts(contacts.AsQueryable(), model).ToList();

            //Assert
          

            Assert.AreEqual(filteredContacts, result);
            //Assert.AreEqual(filteredContacts.First().FirstName, result.First().FirstName);
            //Assert.That(result.Count() == 1);
        }

        // Case sensitive ??
        [Test]
        public void GetFilteredContacts_return_danfeng_With_Lowercase()
        {
            //Arrage
            var contact1 = new Contact
            {
                FirstName = "Kevin"
            };
            var contact2 = new Contact
            {
                FirstName = "Danfeng"
            };
            var contacts = new List<Contact>
            {
                contact1,
                contact2
            };

            var model = new FilterModel
            {
                FirstName = "danfeng"
            };

            var filter = new FirstNameContactFilter();

            var filteredContacts = new List<Contact>
            {
                contact2
            };

            //Act
            var result = filter.GetFilteredContacts(contacts.AsQueryable(), model);

            //Assert
            Assert.AreEqual(filteredContacts.AsQueryable(), result);
        }


        [Test]
        public void GetFilteredContacts_return_collection_of_contact_with_autofixer()
        {
            //Arrage
            //var fixture = new Fixture { RepeatCount = 10 };
            //var contacts = fixture
            //    .Repeat(fixture.Create<Contact>);

            var contacts = _fixture.CreateMany<Contact>();

            var model = new FilterModel
            {
                FirstName = "k"
            };

            var filter = new FirstNameContactFilter();

            var filteredContacts = new List<Contact>();

            //Act
            var result = filter.GetFilteredContacts(contacts.AsQueryable(), model);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
