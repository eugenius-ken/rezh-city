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
    public class VacancyRepository : IRepository<Vacancy>
    {
        private RezhCityContext db;

        public VacancyRepository(RezhCityContext context)
        {
            db = context;
        }

        public IQueryable<Vacancy> GetAll()
        {
            return db.Vacancies;
        }

        public Vacancy Get(Guid id)
        {
            return db.Vacancies.Find(id);
        }

        public IEnumerable<Vacancy> Find(Func<Vacancy, Boolean> predicate)
        {
            return db.Vacancies.Where(predicate).ToList();
        }

        public void Create(Vacancy entity)
        {
            db.Vacancies.Add(entity);
        }

        public void Update(Vacancy entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            Vacancy entity = db.Vacancies.Find(id);
            if (entity != null)
            {
                db.Vacancies.Remove(entity);
            }
        }
    }
}
