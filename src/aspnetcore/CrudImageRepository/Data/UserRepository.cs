using System;
using System.Linq;
using CrudImageRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudImageRepository.Data
{
    // very simple Repository without validation
    public class UserRepository : IUserRepository
    {
        protected DbContext Context { get; }

        public UserRepository(DbContext context) => Context = context;

        public int Create(User u)
        {
            //var entry =  Context.Add(c); // OR
            //var entry =  Context.Add<User>(c); // OR
            //Context.Entry(c).State = EntityState.Added; // OR

            var entry = Context.Attach<User>(u);
            Context.SaveChanges();
            return entry.Entity.Id;
        }

        public void Update(User u)
        {
            Context.Update(u);
            Context.SaveChanges();
        }

        public bool Delete(int id)
        {
            try
            {
                Context.Remove<User>(Read(id));
                return Context.SaveChanges() == 1; // just one record
            }
            catch
            {
                return false;
            }
        }

        public User Read(int id) => Context.Find<User>(id);

        public User[] ReadAll() => Context.Set<User>().AsNoTracking().ToArray();
    }
}