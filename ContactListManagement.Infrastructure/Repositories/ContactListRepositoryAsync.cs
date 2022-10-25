using ContactListManagement.Core.Entities;
using ContactListManagement.Core.Interfaces;
using ContactListManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactListManagement.Infrastructure.Repositories
{
    public class ContactListRepositoryAsync : GenericRepositoryAsync<ContactList>, IContactListRepositoryAsync
    {
        private readonly DbSet<ContactList> _contactList;
        public ContactListRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _contactList = dbContext.Set<ContactList>();
        }
    }
}
