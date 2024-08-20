using Microsoft.EntityFrameworkCore;
using TV_Domain;

namespace TV_Infrastructure.Repository
{
    public class TVShowRepository : GenericRepository<TVShow>, ITVShowRepository
    {
        private readonly TVDBContext TV_DBContext;
        public TVShowRepository(TVDBContext TV_DBContext) : base(TV_DBContext)
        {
            this.TV_DBContext = TV_DBContext;
        }

        public TVShow Create(TVShow? TVShow)
        {
            TV_DBContext.Add(TVShow);
            SaveChange();
            return TVShow;
        }
        public TVShow GetItemWithAttachment(Guid TVShowId)
        {
           var TVShow= TV_DBContext.TVShows.Include(a => a.Attachment)
                                           .Where(x => x.Id == TVShowId)
                                           .FirstOrDefault();
            return TVShow;
        }

        public TVShow Update(TVShow? TVShow, Guid? TVShowId)
        {
            var TVShowForUpdate = All().Where(x => x.Id == TVShowId).FirstOrDefault();
            TV_DBContext.Update(TVShowForUpdate);
            return TVShowForUpdate;

        }

        
    }
}
