using System.ComponentModel.DataAnnotations;

namespace Homework_TV_MVC.Models
{
    public class CreateOrUpdateTVShow
    {
        public TVShowModel TVShow { get; set; }
        public LanguagesModel Languges { get; set; }
        //ممكن يكون 
        //null
        //عند
        //Update
        public IFormFile? file { get; set; }

    }
}
