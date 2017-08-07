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
    public class ImageRepository : IRepository<Image>
    {
        private RezhCityContext db;

        public ImageRepository(RezhCityContext context)
        {
            db = context;
        }

        public IQueryable<Image> GetAll()
        {
            return db.Images;
        }

        public Image Get(Guid id)
        {
            return db.Images.Find(id);
        }

        public IEnumerable<Image> Find(Func<Image, Boolean> predicate)
        {
            return db.Images.Where(predicate).ToList();
        }

        public void Create(Image entity)
        {
            db.Images.Add(entity);
        }

        public void Update(Image entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            Image entity = db.Images.Find(id);
            if (entity != null)
            {
                db.Images.Remove(entity);
            }
        }
    }
}
