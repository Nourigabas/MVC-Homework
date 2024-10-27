using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TV_Domain;
using TV_Infrastructure;
using TV_Infrastructure.Repository;

namespace Homework_TV_MVC.Components
{
    [ViewComponent(Name = "TVShowList")]
    public class TVShowListViewComponent : ViewComponent
    {
        private readonly ILogger<TVShowListViewComponent> Logger;
        private TVDBContext TVDBContext;
        private readonly IRepository<TVShow> TVShowRepository;

        public TVShowListViewComponent(ILogger<TVShowListViewComponent> Logger,
                                       IRepository<TVShow> TVShowRepository,
                                       TVDBContext TVDBContext)
        {
            this.Logger = Logger;
            this.TVDBContext = TVDBContext;
            this.TVShowRepository = TVShowRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var TVShows = TVDBContext.TVShows.Include(a => a.Attachment)
                                             .Where(x => x.IsDeleted == false)
                                             .ToList();
            Logger.LogInformation($"We have {TVShows.Count} TVShows");
            return View(TVShows);
        }
    }
}