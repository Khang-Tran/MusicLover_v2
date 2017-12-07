using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Contracts;
using MusicLover.WebApp.Server.Core.Models;

namespace MusicLover.WebApp.Server.Persistent.Repositories.Contracts
{
    public interface IPhotoRepository: IDataRepository<Photo>
    {
        Task<Photo> GetPhoto(string userId);
        Task<ApplicationUser> GetUser(string userId);
    }
}