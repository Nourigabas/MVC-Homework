using System.ComponentModel.DataAnnotations;

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

        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<TVShowLanguages> TVShowLanguages { get; set; }
    }
}