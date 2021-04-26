using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model.domain;
using Persistance;

namespace Persistence.EF
{
    public class UserRepository : IUserRepository
    {
        internal DbContext context;
        internal DbSet<User> dbSet;
        
        public UserRepository(string connectionString)
        {
            Console.WriteLine("UserRepository " + connectionString);
            context = new UserContext(connectionString);
            dbSet = context.Set<User>();
        }

        public bool FindOne(string username, string password)
        {
            var user = dbSet.FirstOrDefault(u => u.Name == username && u.Password == password);
            Console.WriteLine(user);
            return user != null;
        }

        public virtual User GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(User entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(int id)
        {
            User entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(User entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(User entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public List<User> FindAll()
        {
            IQueryable<User> query = dbSet;
            return query.ToList();
        }
    }
}
