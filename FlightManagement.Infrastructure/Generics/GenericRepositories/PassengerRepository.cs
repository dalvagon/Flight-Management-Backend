using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagement.Domain.Entities;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class PassengerRepository : Repository<Passenger>
    {
        public PassengerRepository(DatabaseContext context) : base(context)
        {
        }
    }
}