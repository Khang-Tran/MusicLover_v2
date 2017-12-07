using System.Collections.Generic;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Models;

namespace MusicLover.WebApp.Server.Persistent.Repositories.Contracts
{
    public interface IFolloweeRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllFollowees(string userId);
    }
}