using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicLover.WebApp.Server.Controllers.APIs;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Core.Resources;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;
using Xunit;

namespace Server.Tests.Controller.Tests
{
    public class GigControllerTests
    {
        private readonly GigsController _controller;
        private readonly Mock<IGigRepository> _mockRepository;
        private readonly string _userId;

        public GigControllerTests()
        {
            _mockRepository = new Mock<IGigRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.GigRepository).Returns(_mockRepository.Object);

            var mockMapper = new Mock<IMapper>();
            _controller = new GigsController(mockUnitOfWork.Object, mockMapper.Object);
            _userId = "1";
            _controller.MockUser(_userId, "test@domain.com");
        }

        [Fact]
        public async void UpdateGig_HappyPath_ShouldReturnOkObject()
        {
            var gigId = 1;
            _mockRepository.Setup(r => r.GetGigWithId(gigId,true)).ReturnsAsync(new Gig());

            var result = await _controller.UpdateGig(gigId, new SavedGigResource());

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void UpdateGig_InvalidInput_ShouldReturnBadRequestObject()
        {
            var gigId = 1;
            
            _mockRepository.Setup(r => r.GetGigWithId(gigId, true)).ReturnsAsync(new Gig());

            _controller.ModelState.AddModelError("ArtistId", "Required");
            var result = await _controller.UpdateGig(gigId, new SavedGigResource());

            result.Should().BeOfType<BadRequestObjectResult>();
        }


        [Fact]
        public async void UpdateGig_GigNotExisted_ShouldReturnNotFoundObject()
        {
            var gigId = 1;
            _mockRepository.Setup(r => r.GetGigWithId(gigId, true)).ReturnsAsync((Gig)null);

            var result = await _controller.UpdateGig(gigId, new SavedGigResource());

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async void Cancel_HappyPath_ShouldReturnOkObject()
        {
            var gigId = 1;
            _mockRepository.Setup(r => r.GetGigWithId(gigId, true)).ReturnsAsync(new Gig());

            var result = await _controller.Cancel(gigId);

            result.Should().BeOfType<OkObjectResult>();
        }

        public async void Cancel_GigNotExisted_ShouldReturnNotFoundObject()
        {
            var gigId = 1;
            _mockRepository.Setup(r => r.GetGigWithId(gigId, true)).ReturnsAsync((Gig)null);

            var result = await _controller.Cancel(gigId);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        public async void Cancel_GigAlreadyBeenCanceled_ShouldReturnNotFoundObject()
        {
            var gigId = 1;
            _mockRepository.Setup(r => r.GetGigWithId(gigId, true)).ReturnsAsync(new Gig(){IsCancel = true});

            var result = await _controller.Cancel(gigId);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        public async void GetGig_HappyPath_ShouldReturnOkObject()
        {
            var gigId = 1;
            _mockRepository.Setup(r => r.GetGigWithId(gigId, true)).ReturnsAsync(new Gig());

            var result = await _controller.GetGig(gigId);

            result.Should().BeOfType<NotFoundObjectResult>();
        }
        public async void GetGig_GigNotExisted_ShouldReturnNotFoundObject()
        {
            var gigId = 1;
            _mockRepository.Setup(r => r.GetGigWithId(gigId, true)).ReturnsAsync((Gig)null);

            var result = await _controller.GetGig(gigId);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        public async void GetGigs_HappyPath_ShouldReturnOkObject()
        {
            _mockRepository.Setup(r => r.GetAllUpcomingGigs(true)).ReturnsAsync(new List<Gig>());

            var result = await _controller.GetGigs();

            result.Should().BeOfType<OkObjectResult>();
        }



    }
}
