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
    public class WorkplaceRepository: IRepository<Workplace>
    {
        private RezhCityContext db;

        public WorkplaceRepository(RezhCityContext context)
        {
            db = context;
        }

        public IQueryable<Workplace> GetAll()
        {
            return db.Workplaces;
        }

        public Workplace Get(Guid id)
        {
            return db.Workplaces.Find(id);
        }

        public IEnumerable<Workplace> Find(Func<Workplace, Boolean> predicate)
        {
            return db.Workplaces.Where(predicate).ToList();
        }

        public void Create(Workplace entity)
        {
            db.Workplaces.Add(entity);
        }

        public void Update(Workplace entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            Workplace entity = db.Workplaces.Find(id);
            if (entity != null)
            {
                db.Workplaces.Remove(entity);
            }
        }
    }
}
