using Microsoft.EntityFrameworkCore;
using TV_Domain;
using static TV_Domain.Rating;

namespace TV_Infrastructure
{
    public class TVDBContext : DbContext
    {
        public DbSet<TVShowLanguages> TVShowLanguages { get; set; }
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Languages> Languages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Homework_MVC_TV");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TVShow>()
                        .HasOne(tv => tv.Attachment)
                        .WithOne(a => a.TVShow)
                        .HasForeignKey<Attachment>(a => a.TVShowId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TVShowLanguages>()
                        .HasOne(tl => tl.TVShow)
                        .WithMany(tv => tv.TVShowLanguages)
                        .HasForeignKey(tl => tl.TVShowID)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TVShowLanguages>()
                        .HasOne(tl => tl.Languages)
                        .WithMany(l => l.TVShowLanguages)
                        .HasForeignKey(tl => tl.LanguageID)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Languages>().HasData(
            new Languages
            {
                Id = Guid.Parse("a25aeca6-cfc9-43d1-96b9-e4f3530ef7fb"),
                Name = "English",
                IsDeleted = false
            },
            new Languages
            {
                Id = Guid.Parse("79be201e-37d4-4c65-814b-e726d61ac82c"), // تعيين معرف فريد
                Name = "Arabic",
                IsDeleted = false
            });

            modelBuilder.Entity<TVShow>()
                        .Property(tv => tv.Rating)
                        .HasConversion
                        (
                            // تحويل
                            // enum
                            // إلى
                            // string
                            // عند التخزين
                            v => v.ToString(),

                            // تحويل
                            // string
                            // إلى
                            // enum
                            // عند الاسترجاع
                            v => (ERating)Enum.Parse(typeof(ERating), v)
                        );

            base.OnModelCreating(modelBuilder);
        }

        public static void CreateInitialTestingDataBase(TVDBContext TV_DBContext)
        {
            TV_DBContext.Database.EnsureDeleted();
            TV_DBContext.Database.Migrate();
            TV_DBContext.TVShows.AddRange(new List<TVShow>
                    {
                        new TVShow
                        {
                            Id = Guid.Parse("e99f4b48-f6c5-4c0b-91a5-a2d6f7e7c392"),
                            Title = "Politics news in Syria",
                            ReleaseDate = DateTime.UtcNow,
                            Rating = (ERating)3,
                            URL = "https://www.aljazeera.net/where/arab/syria/"
                        },
                        new TVShow
                        {
                            Id = Guid.Parse("0f11bbca-c9b2-4bfb-8acb-20192869ce38"),
                            Title = "Sports news in Syria",
                            ReleaseDate = DateTime.UtcNow,
                            Rating = (ERating)2,
                            URL = "https://www.kooora.com/?y=sy"
                        }
                    });
            TV_DBContext.Attachments.AddRange(new List<Attachment>
                        {
                            new Attachment
                            {
                                Id = Guid.Parse("5bffec57-562c-4646-a9ac-20bb61419b1d"),
                                Name = "Politics",
                                Path = "/imgs/TVShow/e99f4b48-f6c5-4c0b-91a5-a2d6f7e7c392.jpg",
                                FileType = "jpg",
                                TVShowId = Guid.Parse("e99f4b48-f6c5-4c0b-91a5-a2d6f7e7c392")
                            },
                            new Attachment
                            {
                                Id = Guid.Parse("624b6e49-4e58-4f54-b871-6f7962b1886c"),
                                Name = "Sports",
                                Path = "/imgs/TVShow/0f11bbca-c9b2-4bfb-8acb-20192869ce38.jpg",
                                FileType = "jpg",
                                TVShowId = Guid.Parse("0f11bbca-c9b2-4bfb-8acb-20192869ce38")
                            }
                        });

            TV_DBContext.SaveChanges();
        }
    }
}