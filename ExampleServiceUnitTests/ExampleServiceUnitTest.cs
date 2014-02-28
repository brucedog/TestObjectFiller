using System;
using System.Collections.Generic;
using Domain;
using ExampleService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using TestObjectFiller;

namespace ExampleServiceUnitTests
{
    [TestClass]
    public class ExampleServiceUnitTest
    {
        private Person _filledPerson = new Person();
        private List<Person> _filledPeople = new List<Person>();

        [TestInitialize]
        public void InitializeTests()
        {
            _filledPerson = ObjectFiller.FillThisObject(_filledPerson, _filledPerson.GetType());

            for (int i = 0; i < 5; i++)
            {
                var filledPerson = ObjectFiller.FillThisObject(_filledPerson, _filledPerson.GetType());
                _filledPeople.Add(filledPerson);
            }
        }

        [TestMethod]
        public void ExampleService_TestMethod()
        {
            IExampleService mockExampleService = MockRepository.GenerateMock<IExampleService>();
            
            mockExampleService.Stub(s => s.GetPerson(Arg<Guid>.Is.Anything)).Return(_filledPerson);

            Assert.IsNotNull(_filledPerson.Id);
            Assert.IsNotNull(_filledPerson.FirstName);
            Assert.IsNotNull(_filledPerson.LastName);
            Assert.IsNotNull(_filledPerson.Address.StreetName);
            Assert.IsNotNull(_filledPerson.Address.Country);
        }

        [TestMethod]
        public void ExampleService_List_TestMethod()
        {
            IExampleService mockExampleService = MockRepository.GenerateMock<IExampleService>();

            mockExampleService.Stub(s => s.GetAllPeople()).Return(_filledPeople);

            foreach (Person person in _filledPeople)
            {
                Assert.IsNotNull(person.Id);
                Assert.IsNotNull(person.FirstName);
                Assert.IsNotNull(person.LastName);
                Assert.IsNotNull(person.Address.StreetName);
                Assert.IsNotNull(person.Address.Country);
            }
        }
    }
}
