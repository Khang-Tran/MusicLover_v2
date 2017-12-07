using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Core.Resources;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/gigs/")]
    public class GigsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGig([FromBody] SavedGigResource savedGigResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var gig = _mapper.Map<SavedGigResource, Gig>(savedGigResource);

           _unitOfWork.GigRepository.AddAsync(gig);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Gig, SavedGigResource>(gig);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGig(int id, [FromBody] SavedGigResource savedGigResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var gig = await _unitOfWork.GigRepository.GetGigWithId(id);
            if (gig == null)
                return NotFound(id);
            _mapper.Map<SavedGigResource, Gig>(savedGigResource, gig);

            await _unitOfWork.CompleteAsync();

            gig = await _unitOfWork.GigRepository.GetGigWithId(gig.Id);
            var result = _mapper.Map<Gig, SavedGigResource>(gig);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = User.GetUserId();
            var gig = await _unitOfWork.GigRepository.GetGigWithId(id);

            if (gig == null || gig.IsCancel)
                return NotFound(id + " not found");
            gig.Cancel();

            // Need to map here
            await _unitOfWork.CompleteAsync();
            return Ok(gig);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGig(int id)
        {
            var gig = await _unitOfWork.GigRepository.GetGigWithId(id);
            if (gig == null)
                return NotFound(id);

            var gigResource = _mapper.Map<Gig, GigResource>(gig);
            return Ok(gigResource);
        }

        [HttpGet]
        public async Task<IEnumerable<GigResource>> GetGigs()
        {
            var gigs = await _unitOfWork.GigRepository.GetAllUpcomingGigs();

            return _mapper.Map<IEnumerable<Gig>, IEnumerable<GigResource>>(gigs);
        }



    }

}
