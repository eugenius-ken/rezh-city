using Ninject.Modules;
using RezhCity.DAL.Interfaces;
using RezhCity.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Managers
{
    public class ManagerModule : NinjectModule
    {
        private string connectionString;
        public ManagerModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
