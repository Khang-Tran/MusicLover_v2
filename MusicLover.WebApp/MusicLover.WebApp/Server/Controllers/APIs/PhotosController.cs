using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Persistent;
using System;
using System.IO;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Resources;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/users/{userId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _host;
        private readonly IOptionsSnapshot<PhotoSettings> _options;
        private readonly IMapper _mapper;
        private readonly PhotoSettings _photoSettings;
        public PhotosController(IUnitOfWork unitOfWork, IHostingEnvironment host, IOptionsSnapshot<PhotoSettings> options, IMapper mapper)
        {
            _photoSettings = options.Value;
            _unitOfWork = unitOfWork;
            _host = host;
            _options = options;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string userId, IFormFile file)
        {
            var user = await _unitOfWork.PhotoRepository.GetUser(userId);
            if (user == null)
                return NotFound();

            if (file == null)
                return BadRequest("Not Found");
            if (file.Length == 0)
                return BadRequest(file.FileName + " is empty");
            if (file.Length > 10485760)
                return BadRequest(file.FileName + " is too long");
            if (!_photoSettings.IsAccepted(file.FileName))
                return BadRequest("Invalid file");

            var uploadFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            var photo = new Photo { FileName = fileName };
            user.ProfilePhoto = photo;
            await _unitOfWork.CompleteAsync();
            return Ok(photo);
        }
        [HttpGet]
        public async Task<PhotoResource> GetPhoto(string userId)
        {
            var photo = await _unitOfWork.PhotoRepository.GetPhoto(userId);
            return _mapper.Map<Photo, PhotoResource>(photo);
        }
    }
}
