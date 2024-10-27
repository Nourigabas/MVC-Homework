using TV_Domain;

namespace TV_Infrastructure.Repository
{
    public interface ITVShowRepository : IRepository<TVShow>
    {
        TVShow Create(TVShow? TVShow);

        TVShow Update(TVShow? TVShow, Guid? TVShowId);

        TVShow GetItemWithAttachment(Guid TVShowId);
    }
}