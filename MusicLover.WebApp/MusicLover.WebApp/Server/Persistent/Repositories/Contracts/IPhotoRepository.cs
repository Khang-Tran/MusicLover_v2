using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Models;

namespace MusicLover.WebApp.Server.Persistent.Repositories.Contracts
{
    public interface IPhotoRepository
    {
        Task<Photo> GetPhoto(string userId);
        Task<ApplicationUser> GetUser(string userId);
    }
}