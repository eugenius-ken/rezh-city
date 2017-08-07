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
    public class ResumeRepository : IRepository<Resume>
    {
        private RezhCityContext db;

        public ResumeRepository(RezhCityContext context)
        {
            db = context;
        }

        public IQueryable<Resume> GetAll()
        {
            return db.Resumes;
        }

        public Resume Get(Guid id)
        {
            return db.Resumes.Find(id);
        }

        public IEnumerable<Resume> Find(Func<Resume, Boolean> predicate)
        {
            return db.Resumes.Where(predicate).ToList();
        }

        public void Create(Resume entity)
        {
            db.Resumes.Add(entity);
        }

        public void Update(Resume entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            Resume entity = db.Resumes.Find(id);
            if (entity != null)
            {
                db.Resumes.Remove(entity);
            }
        }
    }
}
