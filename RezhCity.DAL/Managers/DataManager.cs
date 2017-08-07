using RezhCity.DAL.Entities;
using RezhCity.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using RezhCity.DAL.Enums;

namespace RezhCity.DAL.Managers
{
    public class DataManager
    {
        private const int ItemsPerPage = 20; //items' count per page

        public IUnitOfWork db { get; set; }

        public DataManager(IUnitOfWork uow)
        {
            db = uow;
        }

        #region Advertisements
        public IQueryable<Advertisement> GetAdvertisementsQuery(Category? category = null)
        {
            var query = db.Advertisements.GetAll().Where(a => a.Premoderated);
            if(category.HasValue)
                query = query.Where(a => a.Category == category);

            return query;
        }

        public IQueryable<Advertisement> SearchAdvQuery(Category? category = null, 
            AdvType? type = null, RealtyTradeType? realtyType = null, 
            AdvObject? obj = null, 
            string keyword = null)
        {
            if (type.HasValue && realtyType.HasValue)
                throw new Exception("type and realtyType cannot be defined together");

            var query = db.Advertisements.GetAll().Where(a => a.Premoderated);

            if (category.HasValue)
                query = query.Where(a => a.Category == category);

            if (obj.HasValue)
                query = query.Where(a => a.Object == obj);

            if (!String.IsNullOrEmpty(keyword))
                query = query.Where(a => a.Name.Contains(keyword) || a.Description.Contains(keyword));

            if (type.HasValue)
                query = query.Where(a => a.Type == type);
            else if (realtyType.HasValue)
                query = query.Where(a => a.RealtyParameters.Type == realtyType);

            return query;
        }

        public IQueryable<Advertisement> SetPagingForQuery(IQueryable<Advertisement> query, int page)
        {
            return query.OrderByDescending(_ => _.Created).Skip(ItemsPerPage * (page - 1)).Take(ItemsPerPage);
        }

        public int GetPagesCount(IQueryable<Advertisement> query)
        {
            var count = query.Count();
            var result = query.Count() / ItemsPerPage;
            return (result == 1 && count % ItemsPerPage == 0) || (result != 0 && count % ItemsPerPage == 0) ? result : result + 1;
        }

        public Advertisement GetAdvertisement(Guid id)
        {
            return db.Advertisements.Get(id);
        }

        public void AddAdvertisement(Advertisement adv)
        {
            db.Advertisements.Create(adv);
            db.Save();
        }

        public void EditAdvertisement(Advertisement adv)
        {
            db.Advertisements.Update(adv);
            db.Save();
        }

        public void DeleteAdvertisement(Guid id)
        {
            db.Advertisements.Delete(id);
            db.Save();
        }

        public Advertisement GetAdvertisementForPremoderation()
        {
            return db.Advertisements.GetAll().Where(a => !a.Premoderated).FirstOrDefault();
        }

        public int GetCountNotPremoderatedAdvertisements()
        {
            return db.Advertisements.GetAll().Where(a => !a.Premoderated).Count();
        }
        #endregion

        #region Images
        public Image GetImageById(Guid id)
        {
            return db.Images.Get(id);
        }

        public void AddImage(Image entity)
        {
            db.Images.Create(entity);
            db.Save();
        }

        public void AddImages(IEnumerable<Image> entities)
        {
            foreach (var entity in entities)
            {
                db.Images.Create(entity);
            }
            db.Save();
        }
        #endregion

        #region Thumbnails
        public Thumbnail GetThumbnailById(Guid id)
        {
            return db.Thumbnails.Get(id);
        }

        public Thumbnail GetThumbnailByAdvId(Guid id)
        {
            return db.Thumbnails.GetAll().Where(t => t.AdvertisementId == id).OrderBy(t => t.Created).FirstOrDefault();
        }

        public void AddThumbnail(Thumbnail entity)
        {
            db.Thumbnails.Create(entity);
            db.Save();
        }
        #endregion

        #region Resumes
        public IQueryable<Resume> GetResumes()
        {
            var query = db.Resumes.GetAll().Where(a => a.Premoderated);
            return query;
        }

        public IQueryable<Resume> SearchResumeQuery(VacancyCategory? category = null, string keyword = null)
        {
            var query = db.Resumes.GetAll().Where(a => a.Premoderated);

            if (category.HasValue)
                query = query.Where(r => r.Category == category);

            if (!String.IsNullOrEmpty(keyword))
                query = query.Where(r => r.Name.Contains(keyword) || r.Position.Contains(keyword) || r.Description.Contains(keyword));

            return query;
        }

        public IQueryable<Resume> SetPagingForQuery(IQueryable<Resume> query, int page)
        {
            return query.OrderByDescending(_ => _.Created).Skip(ItemsPerPage * (page - 1)).Take(ItemsPerPage);
        }

        public int GetPagesCount(IQueryable<Resume> query)
        {
            var count = query.Count();
            var result = query.Count() / ItemsPerPage;
            return result == 1 || (result != 0 && count % ItemsPerPage == 0) ? result : result + 1;
        }

        public Thumbnail GetThumbnailByResumeId(Guid id)
        {
            return db.Thumbnails.GetAll().Where(t => t.ResumeId == id).FirstOrDefault();
        }

        public Resume GetResume(Guid id)
        {
            return db.Resumes.Get(id);
        }

        public File GetFile(Guid id)
        {
            return db.Files.Get(id);
        }

        public void AddResume(Resume resume)
        {
            db.Resumes.Create(resume);
            db.Save();
        }

        public void EditResume(Resume resume)
        {
            db.Resumes.Update(resume);
            db.Save();
        }

        public void DeleteResume(Guid id)
        {
            db.Resumes.Delete(id);
            db.Save();
        }

        public Resume GetResumeForPremoderation()
        {
            return db.Resumes.GetAll().Where(r => !r.Premoderated).FirstOrDefault();
        }

        public int GetCountNotPremoderatedResume()
        {
            return db.Resumes.GetAll().Where(r => !r.Premoderated).Count();
        }
        #endregion

        #region Vacancy

        public IQueryable<Vacancy> GetVacancies()
        {
            var query = db.Vacancies.GetAll().Where(a => a.Premoderated);
            return query;
        }

        public IQueryable<Vacancy> SearchVacancyQuery(VacancyCategory? category = null, string keyword = null)
        {
            var query = db.Vacancies.GetAll().Where(a => a.Premoderated);

            if (category.HasValue)
                query = query.Where(r => r.Category == category);

            if (!String.IsNullOrEmpty(keyword))
                query = query.Where(r => r.Duties.Contains(keyword) || r.Position.Contains(keyword) || r.Description.Contains(keyword));

            return query;
        }

        public IQueryable<Vacancy> SetPagingForQuery(IQueryable<Vacancy> query, int page)
        {
            return query.OrderByDescending(_ => _.Created).Skip(ItemsPerPage * (page - 1)).Take(ItemsPerPage);
        }

        public int GetPagesCount(IQueryable<Vacancy> query)
        {
            var count = query.Count();
            var result = query.Count() / ItemsPerPage;
            return result == 1 || (result != 0 && count % ItemsPerPage == 0) ? result : result + 1;
        }

        public Vacancy GetVacancy(Guid id)
        {
            return db.Vacancies.Get(id);
        }

        public void AddVacancy(Vacancy vacancy)
        {
            db.Vacancies.Create(vacancy);
            db.Save();
        }

        public void EditVacancy(Vacancy vacancy)
        {
            db.Vacancies.Update(vacancy);
            db.Save();
        }

        public void DeleteVacancy(Guid id)
        {
            db.Vacancies.Delete(id);
            db.Save();
        }

        public Vacancy GetVacancyForPremoderation()
        {
            return db.Vacancies.GetAll().Where(r => !r.Premoderated).FirstOrDefault();
        }

        public int GetCountNotPremoderatedVacancies()
        {
            return db.Vacancies.GetAll().Where(r => !r.Premoderated).Count();
        }
        #endregion

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
