using Homework_TV_MVC.Constraints;
using Homework_TV_MVC.Repositories;
using Homework_TV_MVC.Transformer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Globalization;
using TV_Domain;
using TV_Infrastructure;
using TV_Infrastructure.Repository;

namespace Homework_TV_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //اللغات
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddControllersWithViews()
                            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                            .AddDataAnnotationsLocalization();

            builder.Services.Configure<RequestLocalizationOptions>(option =>
            {
                var supportedcultures = new List<CultureInfo>
                {
                    new CultureInfo("ar-SY"),
                    new CultureInfo("en-US")
                };
                option.SupportedCultures = supportedcultures;
                option.SupportedUICultures = supportedcultures;
                option.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
                option.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                    //new AcceptLanguageHeaderRequestCultureProvider(),
                };
            });


            //authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(option =>
                            {
                                option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                                option.LoginPath = "/login";
                                option.SlidingExpiration = true;
                            });
            //slug(constraint and transformer)
            builder.Services.AddRouting(option =>
            {
                option.ConstraintMap["validateslug"] = typeof(SlugConstraint);
                option.ConstraintMap["ValidateTransformerSlug"] = typeof(SlugParameterTransformer);
            });


            //register repository
            builder.Services.AddScoped<ITVShowLanguagesRepository, TVShowLanguagesRepository>();
            builder.Services.AddScoped<ILanguagesRepository, LanguagesRepository>();
            builder.Services.AddScoped<IAttachmentRepository, AttachemntRepository>();
            builder.Services.AddScoped<ITVShowRepository, TVShowRepository>();
            builder.Services.AddScoped<IRepository<TVShow>, TVShowRepository>();
            builder.Services.AddScoped<IRepository<Languages>, LanguagesRepository>();
            builder.Services.AddScoped<IRepository<Attachment>, AttachemntRepository>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<TVDBContext>(ServiceLifetime.Scoped);
            builder.Services.AddTransient<ISessionRepository, SessionRepository>();

            //التعامل مع ال 
            //session 
            //في حفظ البيانات لفترة 
            builder.Services.AddSession();



            //التعامل مع 
            //logger
            builder.Services.AddLogging();


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            using (var scoped = app.Services.CreateScope())
            {
                var context = scoped.ServiceProvider.GetRequiredService<TVDBContext>();
                TVDBContext.CreateInitialTestingDataBase(context);
            }
            //جعلها 
            //sission 
            //كانها 
            //middleware
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            //جعل خاصية تبيدل اللغات ك 
            //middleware
            var option = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(option.Value);


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
