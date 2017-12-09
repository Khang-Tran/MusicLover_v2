using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/followings/")]
    public class FollowingsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] string followeeId)
        {
            var userId = "1";
            var existed = await _unitOfWork.FollowingRepository.IsExist(userId, followeeId);

            if (existed)
                return BadRequest(followeeId + " existed");

            var follow = new Following()
            {
                FolloweeId = followeeId,
                FollowerId = userId
            };

            _unitOfWork.FollowingRepository.AddAsync(follow);
            await _unitOfWork.CompleteAsync();
            return Ok(followeeId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = "1";
            var following = await _unitOfWork.FollowingRepository.GetFollowing(id, userId);
            if (following == null)
                return NotFound(id);
            _unitOfWork.FollowingRepository.Remove(following);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }
    }
}
