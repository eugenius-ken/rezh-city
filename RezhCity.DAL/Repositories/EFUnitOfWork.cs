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
    public class EFUnitOfWork : IUnitOfWork
    {
        private RezhCityContext db;

        private AdvertisementRepository advertisementRepository;
        private AutoParametersRepository autoParametersRepository;
        private RealtyParametersRepository realtyParametersRepository;
        private ImageRepository imageRepository;
        private ThumbnailRepository thumbnailRepository;
        private ResumeRepository resumeRepository;
        private WorkplaceRepository workplaceRepository;
        private SchoolRepository schoolRepository;
        private FileRepository fileRepository;
        private VacancyRepository vacancyRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new RezhCityContext(connectionString);
        }

        public IRepository<Advertisement> Advertisements
        {
            get
            {
                if (advertisementRepository == null)
                    advertisementRepository = new AdvertisementRepository(db);

                return advertisementRepository;
            }
        }

        public IRepository<AutoParameters> AutoParameters
        {
            get
            {
                if (autoParametersRepository == null)
                    autoParametersRepository = new AutoParametersRepository(db);

                return autoParametersRepository;
            }
        }
        public IRepository<RealtyParameters> RealtyParameters
        {
            get
            {
                if (realtyParametersRepository == null)
                    realtyParametersRepository = new RealtyParametersRepository(db);

                return realtyParametersRepository;
            }
        }


        public IRepository<Image> Images
        {
            get
            {
                if (imageRepository == null)
                    imageRepository = new ImageRepository(db);

                return imageRepository;
            }
        }

        public IRepository<Thumbnail> Thumbnails
        {
            get
            {
                if (thumbnailRepository == null)
                    thumbnailRepository = new ThumbnailRepository(db);

                return thumbnailRepository;
            }
        }

        public IRepository<Resume> Resumes
        {
            get
            {
                if (resumeRepository == null)
                    resumeRepository = new ResumeRepository(db);

                return resumeRepository;
            }
        }

        public IRepository<Workplace> Workplaces
        {
            get
            {
                if (workplaceRepository == null)
                    workplaceRepository = new WorkplaceRepository(db);

                return workplaceRepository;
            }
        }

        public IRepository<School> Schools
        {
            get
            {
                if (schoolRepository == null)
                    schoolRepository = new SchoolRepository(db);

                return schoolRepository;
            }
        }

        public IRepository<File> Files
        {
            get
            {
                if (fileRepository == null)
                    fileRepository = new FileRepository(db);

                return fileRepository;
            }
        }

        public IRepository<Vacancy> Vacancies
        {
            get
            {
                if (vacancyRepository == null)
                    vacancyRepository = new VacancyRepository(db);

                return vacancyRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
