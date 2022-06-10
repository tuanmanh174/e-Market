using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T Get(int id, bool isActive = true);
        void Insert(T entity, bool saveChange = true);
        void Update(T entity, bool saveChange = true);
        void Delete(T entity, bool saveChange = true);
    }

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        
        public Repository(AppDbContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }
        public T Get(int id, bool isActive = true)
        {
            return entities.FirstOrDefault(s=>s.id == id && (s.Active || !isActive));
        }
        public void Insert(T entity, bool saveChange = true)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;
            entities.Add(entity);
            if (saveChange)
                _context.SaveChanges();
        }
        public void Update(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.UpdatedTime = DateTime.Now;
            if (saveChange)
                _context.SaveChanges();
        }
        public void Delete(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(entity);
            if (saveChange)
                _context.SaveChanges();
        }
    }
}
