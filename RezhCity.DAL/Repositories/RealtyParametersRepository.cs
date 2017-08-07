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
    public class RealtyParametersRepository: IRepository<RealtyParameters>
    {
        private RezhCityContext db;

        public RealtyParametersRepository(RezhCityContext context)
        {
            db = context;
        }

        public IQueryable<RealtyParameters> GetAll()
        {
            return db.RealtyParameters;
        }

        public RealtyParameters Get(Guid id)
        {
            return db.RealtyParameters.Find(id);
        }

        public IEnumerable<RealtyParameters> Find(Func<RealtyParameters, Boolean> predicate)
        {
            return db.RealtyParameters.Where(predicate).ToList();
        }

        public void Create(RealtyParameters entity)
        {
            db.RealtyParameters.Add(entity);
        }

        public void Update(RealtyParameters entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            RealtyParameters entity = db.RealtyParameters.Find(id);
            if (entity != null)
            {
                db.RealtyParameters.Remove(entity);
            }
        }
    }
}
