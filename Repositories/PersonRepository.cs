using Contracts;
using Contracts.RepositoryInterfaces;
using Entities;
using Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;

namespace Repositories
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(DBContext context, ILogger logger): base(context,logger)
        {
        }

        public override async Task Update(Person entity)
        {
            var personToUpdate = _context.People.Find(entity.IdPerson);

            if(personToUpdate is not null)
            {
                personToUpdate.Name = entity.Name;
                personToUpdate.LastName = entity.LastName;
                personToUpdate.Cellphone = entity.Cellphone;
                personToUpdate.Address = entity.Address;
                personToUpdate.Dni = entity.Dni;
                personToUpdate.Email = entity.Email;
            }
        }
    }
}