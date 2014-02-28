using System;
using System.Collections.Generic;
using Domain;

namespace ExampleService
{
    public class ExampleService : IExampleService 
    {
        public Person GetPerson(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetAllPeople()
        {
            throw new NotImplementedException();
        }
    }
}
