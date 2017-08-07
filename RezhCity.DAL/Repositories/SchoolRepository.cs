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
    public class SchoolRepository: IRepository<School>
    {
        private RezhCityContext db;

        public SchoolRepository(RezhCityContext context)
        {
            db = context;
        }

        public IQueryable<School> GetAll()
        {
            return db.Schools;
        }

        public School Get(Guid id)
        {
            return db.Schools.Find(id);
        }

        public IEnumerable<School> Find(Func<School, Boolean> predicate)
        {
            return db.Schools.Where(predicate).ToList();
        }

        public void Create(School entity)
        {
            db.Schools.Add(entity);
        }

        public void Update(School entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            School entity = db.Schools.Find(id);
            if (entity != null)
            {
                db.Schools.Remove(entity);
            }
        }
    }
}
