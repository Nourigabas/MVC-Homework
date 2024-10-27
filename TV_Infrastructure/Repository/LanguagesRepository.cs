using TV_Domain;

namespace TV_Infrastructure.Repository
{
    public class LanguagesRepository : GenericRepository<Languages>, ILanguagesRepository
    {
        private readonly TVDBContext TV_DBContext;

        public LanguagesRepository(TVDBContext TV_DBContext) : base(TV_DBContext)
        {
            this.TV_DBContext = TV_DBContext;
        }
    }
}