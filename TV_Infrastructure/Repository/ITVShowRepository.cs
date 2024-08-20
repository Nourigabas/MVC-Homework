using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV_Domain;

namespace TV_Infrastructure.Repository
{
    public interface ITVShowRepository:IRepository<TVShow>
    {
        TVShow Create(TVShow? TVShow);
        TVShow Update(TVShow? TVShow,Guid? TVShowId);
        TVShow GetItemWithAttachment(Guid TVShowId);
    }
}
