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
    public class AdvertisementRepository : IRepository<Advertisement>
    {
        private RezhCityContext db;

        public AdvertisementRepository(RezhCityContext context)
        {
            db = context;
        }

        public IQueryable<Advertisement> GetAll()
        {
            return db.Advertisements;
        }

        public Advertisement Get(Guid id)
        {
            return db.Advertisements.Find(id);
        }

        public IEnumerable<Advertisement> Find(Func<Advertisement, Boolean> predicate)
        {
            return db.Advertisements.Where(predicate).ToList();
        }

        public void Create(Advertisement entity)
        {
            db.Advertisements.Add(entity);
        }

        public void Update(Advertisement entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            Advertisement entity = db.Advertisements.Find(id);
            if (entity != null)
            {
                db.Advertisements.Remove(entity);
            }
        }
    }
}
