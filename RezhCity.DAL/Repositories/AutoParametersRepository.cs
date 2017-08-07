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
    public class AutoParametersRepository: IRepository<AutoParameters>
    {
        private RezhCityContext db;

        public AutoParametersRepository(RezhCityContext context)
        {
            db = context;
        }

        public IQueryable<AutoParameters> GetAll()
        {
            return db.AutoParameters;
        }

        public AutoParameters Get(Guid id)
        {
            return db.AutoParameters.Find(id);
        }

        public IEnumerable<AutoParameters> Find(Func<AutoParameters, Boolean> predicate)
        {
            return db.AutoParameters.Where(predicate).ToList();
        }

        public void Create(AutoParameters entity)
        {
            db.AutoParameters.Add(entity);
        }

        public void Update(AutoParameters entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            AutoParameters entity = db.AutoParameters.Find(id);
            if (entity != null)
            {
                db.AutoParameters.Remove(entity);
            }
        }
    }
}
