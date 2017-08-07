using RezhCity.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Advertisement> Advertisements { get; }
        IRepository<AutoParameters> AutoParameters { get; }
        IRepository<RealtyParameters> RealtyParameters { get; }
        IRepository<Thumbnail> Thumbnails { get; }
        IRepository<Image> Images { get; }
        IRepository<Resume> Resumes { get; }
        IRepository<Workplace> Workplaces { get; }
        IRepository<School> Schools { get; }
        IRepository<File> Files { get; }
        IRepository<Vacancy> Vacancies { get; }

        void Save();
    }
}
