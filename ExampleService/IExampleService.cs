using System;
using System.Collections.Generic;
using Domain;

namespace ExampleService
{
    public interface IExampleService
    {
        Person GetPerson(Guid id);

        List<Person> GetAllPeople();
    }
}