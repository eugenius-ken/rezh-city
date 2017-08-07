using RezhCity.DAL.EF;
using RezhCity.DAL.Entities;
using RezhCity.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Repositories
{
    public class FileRepository : IRepository<File>
    {
        private RezhCityContext db;

        public FileRepository(RezhCityContext context)
        {
            db = context;
        }

        public IQueryable<File> GetAll()
        {
            return db.Files;
        }

        public File Get(Guid id)
        {
            return db.Files.Find(id);
        }

        public IEnumerable<File> Find(Func<File, Boolean> predicate)
        {
            return db.Files.Where(predicate).ToList();
        }

        public void Create(File entity)
        {
            db.Files.Add(entity);
        }

        public void Update(File entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            File entity = db.Files.Find(id);
            if (entity != null)
            {
                db.Files.Remove(entity);
            }
        }
    }
}
