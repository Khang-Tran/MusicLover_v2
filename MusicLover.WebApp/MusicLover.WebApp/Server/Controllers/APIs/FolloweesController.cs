using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Core.Resources;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/followees/")]
    public class FolloweesController:Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FolloweesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetFollowees()
        {
            var userId = User.GetUserId();

            var followees = await _unitOfWork.FolloweeRepository.GetAllFollowees(userId);
            return Ok(_mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserResource>>(followees));
        }
    }
}
