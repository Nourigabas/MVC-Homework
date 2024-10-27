using Homework_TV_MVC.Attributes;
using Homework_TV_MVC.Models;
using Homework_TV_MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TV_Domain;
using TV_Infrastructure.Repository;

namespace Homework_TV_MVC.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class TVShowController : Controller
    {
        private readonly ISessionRepository SessionRepository;
        private readonly ITVShowRepository TVShowReposetory;
        private readonly ILanguagesRepository LanguagesRepository;
        private readonly IAttachmentRepository AttachemntRepository;
        private readonly ITVShowLanguagesRepository TVShowLanguagesRepository;
        private ILogger logger;

        public TVShowController(ITVShowRepository TVShowReposetory,
                                IAttachmentRepository AttachemntRepository,
                                ILanguagesRepository LanguagesRepository,
                                ISessionRepository SessionRepository,
                                ILogger<TVShowController> logger,
                                ITVShowLanguagesRepository TVShowLanguagesRepository)
        {
            this.SessionRepository = SessionRepository;
            this.TVShowReposetory = TVShowReposetory;
            this.LanguagesRepository = LanguagesRepository;
            this.AttachemntRepository = AttachemntRepository;
            this.TVShowLanguagesRepository = TVShowLanguagesRepository;
            this.logger = logger;
        }

        //تستخدم هذه ال
        //action
        //لعرض المحطات دون تسجيل الدخول
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            //الجزء المعلق لم يتبقى له عمل بعد استخدام
            //Component

            //var all = TVShowRepository.All();
            //var TVShows = all.Where(x => x.IsDeleted == false).ToList();
            //return View(TVShows);

            return View();
        }

        //تستخدم هذه ال
        //action
        //لعرض المحطات مع تسجيل الدخول
        //ومع بعض الاضافات
        //مثل امكانية اضافة جديد
        //عرض تفاصيل
        [HttpGet]
        [Route("HomeTVShow")]
        public IActionResult HomeTVShow()
        {
            return View();
        }

        //استخدمها لعرض تفاصيل المحطة ومع بعض الاضافات الاخرى
        //التعديل
        //الحذف
        [Authorize]
        [HttpGet]
        [Route("details/{TVShowId:Guid}/{slug:validateslug}")]
        public IActionResult DetailsTVShow(Guid TVShowId, string slug)
        {
            //الملف معروض في الواجهة لذلك لا داعي لمناقشة حالة
            //isdelete
            var TVShow = TVShowReposetory.GetItemWithAttachment(TVShowId);
            if (TVShow == null)
            {
                return NotFound();
            }
            return View(TVShow);
        }

        //تستخدم لعرض واجهة اضافة المنتج
        [Route("create")]
        public IActionResult AddTVShow()
        {
            return View("AddTVShow");
        }

        //تستخدم لاستقبال المعلومات من صفحة اضافة المنتج والعمل عليها لاضافتها على قاعدة البيانات
        [HttpGet]
        [TimerFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("add")]
        public IActionResult add(CreateOrUpdateTVShow CreateOrUpdateTVShow)
        {
            if (CreateOrUpdateTVShow.file == null)
                ModelState.AddModelError("Img", "You must upload the image");
            if (!ModelState.IsValid)
                return View("HomeTVShow");
            //تحويل المودل القادم من البارمتر الى الكيانات الاساسية
            //TVShow
            //Languages
            //Attachment
            //تخزين الصورة في ملف ضمن المشروع
            //كل ذلك يتم عبر بعض الدوال التي تم استخدامها
            var TVShow = new TVShow()
            {
                Title = CreateOrUpdateTVShow.TVShow.Title,
                ReleaseDate = CreateOrUpdateTVShow.TVShow.ReleaseDate,
                Rating = CreateOrUpdateTVShow.TVShow.Rating,
                URL = CreateOrUpdateTVShow.TVShow.URL
            };
            //Save&CreateTVShow
            var NewTVShow = TVShowReposetory.Create(TVShow);
            //saveimg
            var AttachmentId = AttachemntRepository.LoudAndSaveImg(CreateOrUpdateTVShow.file, TVShow.Id);
            var ListLangugesSelectedFromUser = CreateOrUpdateTVShow.Languges.Languages;
            List<Guid> ListLangugesSelectedFromUserId = new List<Guid>();

            foreach (var languageName in ListLangugesSelectedFromUser)
            {
                var language = LanguagesRepository.All().FirstOrDefault(x => x.Name == languageName);
                if (language != null)
                {
                    ListLangugesSelectedFromUserId.Add(language.Id);
                }
            }
            //save Languages For TVShow
            var ListTVShowLanguages = TVShowLanguagesRepository.ConnectingTVShowAndLanguages(TVShow.Id, ListLangugesSelectedFromUserId);
            TVShow.AttachmentId = AttachmentId;
            TVShow.TVShowLanguages = ListTVShowLanguages;
            TVShowReposetory.SaveChange();

            return View("HomeTVShow");
        }

        //تستخدم لعرض واجهة تعديل المنتج
        [HttpGet]
        [Route("UpdateTVShow/{TVShowId:Guid}/{slug:validateslug}")]
        public IActionResult UpdateTVShow(Guid TVShowId, string slug)
        {
            SessionRepository.SetValue("TVShowId", TVShowId.ToString());
            var TVShow = TVShowReposetory.Get(TVShowId);
            return View("UpdateTVShow", TVShow);
        }

        //تستخدم لاستقبال المعلومات من صفحة تعديل المنتج والعمل عليها لتحديثها في قاعدة البيانات
        [TimerFilter]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Update")]
        public IActionResult Update(CreateOrUpdateTVShow CreateOrUpdateTVShow)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateTVShow");
            }
            var TVShow = new TVShow()
            {
                Id = Guid.Parse(SessionRepository.GetValue("TVShowId")),
                Title = CreateOrUpdateTVShow.TVShow.Title,
                ReleaseDate = CreateOrUpdateTVShow.TVShow.ReleaseDate,
                Rating = CreateOrUpdateTVShow.TVShow.Rating,
                URL = CreateOrUpdateTVShow.TVShow.URL
            };
            var ListLangugesSelectedFromUser = CreateOrUpdateTVShow.Languges.Languages;
            List<Guid> ListLangugesSelectedFromUserId = new List<Guid>();

            foreach (var languageName in ListLangugesSelectedFromUser)
            {
                var language = LanguagesRepository.All().FirstOrDefault(x => x.Name == languageName);
                if (language != null)
                {
                    ListLangugesSelectedFromUserId.Add(language.Id);
                }
            }
            var ListTVShowLanguages = TVShowLanguagesRepository.UpdateTVShowAndLanguages(TVShow.Id, ListLangugesSelectedFromUserId);
            TVShow.TVShowLanguages = ListTVShowLanguages;
            TVShowReposetory.Update(TVShow);
            TVShowReposetory.SaveChange();

            if (CreateOrUpdateTVShow.file != null)
            {
                //تحديد نوع - امتداد - الصورة -القديمة
                string extension = Path.GetExtension(CreateOrUpdateTVShow.file.FileName);
                //لحذف الصورة القديمة واذافة الصورة الجدية مكانها وبنفس الاسم السابق
                string NameOldImg = TVShow.Id.ToString() + extension;

                string OldImgPath = "wwwroot/imgs/TVShow/" + NameOldImg;

                try
                {
                    Directory.Delete(OldImgPath, true);
                }
                catch (Exception ex)
                {
                    logger.LogInformation("Error : " + ex.Message);
                }
                AttachemntRepository.UpdateImg(CreateOrUpdateTVShow.file, TVShow.Id);
            }
            SessionRepository.Remove("TVShowId");
            return View("HomeTVShow");
        }

        //لحذف المحطة
        //عبر
        //isdeleted
        [HttpGet]
        [Route("delete")]
        public IActionResult DeletTVShow(Guid id)
        {
            var item = TVShowReposetory.Get(id);
            item.IsDeleted = true;
            TVShowReposetory.SaveChange();
            return RedirectToAction("HomeTVShow");
        }
    }
}