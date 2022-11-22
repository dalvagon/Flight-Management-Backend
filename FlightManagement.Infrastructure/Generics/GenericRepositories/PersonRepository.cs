﻿using FlightManagement.Business.Entities;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class PersonRepository : Repository<Person>
    {
        public PersonRepository(DatabaseContext context) : base(context)
        {
        }
    }
}