using System.Threading.Tasks;

namespace MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}