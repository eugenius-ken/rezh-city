using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.Entities
{
    public class Image
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Path { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.Now;

        public Guid? AdvertisementId { get; set; }
        public Guid? ResumeId { get; set; }

        public virtual Advertisement Advertisement { get; set; }
        public virtual Resume Resume { get; set; }
    }
}
