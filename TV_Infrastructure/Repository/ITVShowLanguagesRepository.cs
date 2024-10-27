using TV_Domain;

namespace TV_Infrastructure.Repository
{
    public interface ITVShowLanguagesRepository : IRepository<TVShowLanguages>
    {
        List<TVShowLanguages> ConnectingTVShowAndLanguages(Guid TVShowId, List<Guid> LanguagesIds);

        List<TVShowLanguages> UpdateTVShowAndLanguages(Guid TVShowId, List<Guid> LanguagesIds);
    }
}