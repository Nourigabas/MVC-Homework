using Microsoft.AspNetCore.Http;
using TV_Domain;

namespace TV_Infrastructure.Repository
{
    public class AttachemntRepository : GenericRepository<Attachment>, IAttachmentRepository
    {
        public AttachemntRepository(TVDBContext TV_DBContext) : base(TV_DBContext)
        {
        }

        public Guid LoudAndSaveImg(IFormFile file, Guid TVShowId)
        {
            if (file != null)
            {
                var OldName = file.FileName;
                //تحديد نوع - امتداد - الصورة
                string extension = Path.GetExtension(file.FileName);

                Guid IdForNameImgAndAttachmentid = Guid.NewGuid();
                //تغير اسم الصورة ل
                //TVShowId
                string newFileName = TVShowId.ToString() + extension;
                //تخزين المسار الذي سيتم حفظ فيه الصورة
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgs/TVShow", newFileName);

                //ان لم يكون المجلد موجود يتم انشاءه
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                //حفظ الصورة
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var attachment = new Attachment()
                {
                    Id = IdForNameImgAndAttachmentid,
                    Name = OldName,
                    Path = "/imgs/TVShow/" + newFileName,
                    FileType = extension.Substring(1),
                    TVShowId = TVShowId
                };
                Add(attachment);
                SaveChange();
                return attachment.Id;
            }
            return Guid.Empty;
        }

        public void UpdateImg(IFormFile file, Guid TVShowId)
        {
            if (file != null)
            {                //تحديد نوع - امتداد - الصورة
                var OldName = file.FileName;
                string extension = Path.GetExtension(file.FileName);
                string newFileName = TVShowId.ToString() + extension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgs/TVShow", newFileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var attachment = All().Where(x => x.TVShowId == TVShowId).FirstOrDefault();
                attachment.FileType = extension.Substring(1);
                attachment.Name = OldName;
                Update(attachment);
                SaveChange();
            }
        }
    }
}