using System.Collections.Generic;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Models;

namespace MusicLover.WebApp.Server.Persistent.Repositories.Contracts
{
    public interface IFollowingRepository
    {
        Task<Following> GetFollowing(string artistId, string followerId);
        Task<bool> IsExist(string followerId, string followeeId);
        Task<IEnumerable<ApplicationUser>> GetAllFollowee(string followerId);
        void Add(Following follow);
        void Remove(Following follow);
    }
}