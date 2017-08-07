using Microsoft.AspNet.Identity.EntityFramework;
using RezhCity.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.EF
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string connectionString) : base(connectionString) { }
        public DbSet<ClientProfile> Profiles { get; set; }
    }
}
