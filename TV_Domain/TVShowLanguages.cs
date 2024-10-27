using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_Domain
{
    public class TVShowLanguages
    {
        public TVShowLanguages()
        {
            Id = new Guid();
        }

        [Key]
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; } = false;

        [ForeignKey("LanguageID")]
        public Guid LanguageID { get; set; }

        public virtual Languages Languages { get; set; }

        [ForeignKey("TVShowId")]
        public Guid TVShowID { get; set; }

        public virtual TVShow TVShow { get; set; }
    }
}