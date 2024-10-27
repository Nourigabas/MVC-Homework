using Microsoft.AspNetCore.Http;
using TV_Domain;

namespace TV_Infrastructure.Repository
{
    public interface IAttachmentRepository : IRepository<Attachment>
    {
        Guid LoudAndSaveImg(IFormFile file, Guid TVShowId);

        void UpdateImg(IFormFile file, Guid TVShowId);
    }
}