using System;
using System.Linq;
using CrudRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudRepository.Data
{
    // very simple Repository without validation
    public class CustomerRepository : ICustomerRepository
    {
        protected DbContext Context { get; }

        public CustomerRepository(DbContext context) => Context = context;

        public Guid Create(Customer c)
        {
            //var entry =  Context.Add(c); // OR
            //var entry =  Context.Add<Customer>(c); // OR
            //Context.Entry(c).State = EntityState.Added; // OR

            var entry = Context.Attach<Customer>(c);
            Context.SaveChanges();
            return entry.Entity.Id;
        }

        public void Update(Customer c)
        {
            Context.Update(c);

            Context.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            try
            {
                Context.Remove<Customer>(Read(id));
                return Context.SaveChanges() == 1; // just one record
            }
            catch
            {
                return false;
            }
        }

        public Customer Read(Guid id) => Context.Find<Customer>(id);

        public Customer[] ReadAll() => Context.Set<Customer>().AsNoTracking().ToArray();
    }
}