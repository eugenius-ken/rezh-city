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
    public class ThumbnailRepository : IRepository<Thumbnail>
    {
        private RezhCityContext db;

        public ThumbnailRepository(RezhCityContext context)
        {
            db = context;
        }

        public IQueryable<Thumbnail> GetAll()
        {
            return db.Thumbnails;
        }

        public Thumbnail Get(Guid id)
        {
            return db.Thumbnails.Find(id);
        }

        public IEnumerable<Thumbnail> Find(Func<Thumbnail, Boolean> predicate)
        {
            return db.Thumbnails.Where(predicate).ToList();
        }

        public void Create(Thumbnail entity)
        {
            db.Thumbnails.Add(entity);
        }

        public void Update(Thumbnail entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            Thumbnail entity = db.Thumbnails.Find(id);
            if (entity != null)
            {
                db.Thumbnails.Remove(entity);
            }
        }
    }
}
