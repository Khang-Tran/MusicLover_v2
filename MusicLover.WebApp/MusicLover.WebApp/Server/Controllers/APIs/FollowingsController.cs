using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/followings/")]
    public class FollowingsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFollowingRepository _followingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IMapper mapper, IFollowingRepository followingRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _followingRepository = followingRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] string followeeId)
        {
            var userId = "1";
            var existed = await _followingRepository.IsExist(userId, followeeId);

            if (existed)
                return BadRequest(followeeId + " existed");

            var follow = new Following()
            {
                FolloweeId = followeeId,
                FollowerId = userId
            };

            _followingRepository.Add(follow);
            await _unitOfWork.CompleteAsync();
            return Ok(followeeId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = "1";
            var following = await _followingRepository.GetFollowing(id, userId);
            if (following == null)
                return NotFound(id);
            _followingRepository.Remove(following);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }
    }
}
