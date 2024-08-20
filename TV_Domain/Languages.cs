using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV_Domain
{
    public class Languages
    {
        public Languages()
        {
            Id = new Guid();
        }
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<TVShowLanguages> TVShowLanguages { get; set; }

    }
}
