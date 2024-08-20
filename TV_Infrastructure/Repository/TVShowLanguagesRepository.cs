using TV_Domain;

namespace TV_Infrastructure.Repository
{
    public class TVShowLanguagesRepository : GenericRepository<TVShowLanguages>, ITVShowLanguagesRepository
    {
        private readonly ILanguagesRepository LanguagesRepository;
        private readonly TVDBContext TV_DBContext;
        private readonly ITVShowRepository TVShowRepository;

        public TVShowLanguagesRepository(TVDBContext TV_DBContext,
                                         ILanguagesRepository LanguagesRepository,
                                         ITVShowRepository TVShowRepository)
                                         : base(TV_DBContext)
        {
            this.LanguagesRepository = LanguagesRepository;
            this.TV_DBContext = TV_DBContext;
            this.TVShowRepository = TVShowRepository;
        }

        public List<TVShowLanguages> ConnectingTVShowAndLanguages(Guid TVShowId, List<Guid> LanguagesIds)
        {
            List<TVShowLanguages> ListTVShowLanguages = new List<TVShowLanguages>();

            foreach (var languageId in LanguagesIds)
            {
                var tvShowLanguage = new TVShowLanguages()
                {
                    LanguageID = languageId,
                    TVShowID = TVShowId
                };

                // إضافة TVShowLanguages إلى DbContext
                Add(tvShowLanguage);
                SaveChange();
                ListTVShowLanguages.Add(tvShowLanguage);

                // استرجاع لغة معينة وربطها بـ TVShowLanguages
                var language = LanguagesRepository.All().FirstOrDefault(x => x.Id == languageId);
                if (language != null)
                {
                    language.TVShowLanguages.Add(tvShowLanguage);
                }
            }

            // حفظ التغييرات مرة واحدة
            LanguagesRepository.SaveChange();
            TVShowRepository.SaveChange();

            return ListTVShowLanguages;
        }

        public List<TVShowLanguages> UpdateTVShowAndLanguages(Guid TVShowId, List<Guid> LanguagesIds)
        {
            List<TVShowLanguages> NewTVShowLanguages = new List<TVShowLanguages>();
            for (int i = 0; i < LanguagesIds.Count; i++)
            {
                var respone = All().Where(x => x.TVShowID == TVShowId & x.LanguageID == LanguagesIds[i]).FirstOrDefault();
                if (respone == null)
                {
                    var TVShowLanguages = new TVShowLanguages()
                    {
                        TVShowID = TVShowId,
                        LanguageID = LanguagesIds[i]
                    };
                    Add(TVShowLanguages);
                    SaveChange();
                    NewTVShowLanguages.Add(TVShowLanguages);
                }
                else
                {
                    respone.IsDeleted = true;
                }
            }
            return NewTVShowLanguages;
        }
    }
}
