using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicLover.WebApp.Server.Controllers.APIs;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent.Repositories.Commons;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;
using Xunit;

namespace Server.Tests.Controller.Tests
{
    public class FollowingsControllerTests
    {
        private readonly FollowingsController _controller;
        private readonly Mock<IFollowingRepository> _mockRepository;
        private readonly string _userId;
        public FollowingsControllerTests()
        {
            _mockRepository = new Mock<IFollowingRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.FollowingRepository).Returns(_mockRepository.Object);

            _controller = new FollowingsController(mockUnitOfWork.Object);
            _userId = "1";
            _controller.MockUser(_userId, "test@domain.com");
        }

        [Fact]
        public async void Follow_HappyPath_ShouldReturnOkObject()
        {
            var followeeId = "1";

            _mockRepository.Setup(g => g.IsExist(followeeId, _userId)).ReturnsAsync(false);

            var result = await _controller.Follow(followeeId);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Follow_AlreadyFollowed_ShouldReturnBadRequestObject()
        {
            var followeeId = "1";

            _mockRepository.Setup(g => g.IsExist(followeeId, _userId)).ReturnsAsync(true);

            var result = await _controller.Follow(followeeId);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void Delete_HappyPath_ShouldReturnOkObject()
        {
            var followeeId = "1";
            var follow = new Following();

            _mockRepository.Setup(g => g.GetFollowing(followeeId, _userId)).ReturnsAsync(follow);

            var result = await _controller.Delete(followeeId);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Delete_AttendanceNotExisted_ReturnNotFoundObject()
        {
            var followeeId = "1";
            _mockRepository.Setup(g => g.GetFollowing(followeeId, _userId)).ReturnsAsync((Following)null);
            var result = await _controller.Delete(followeeId);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

    }
}
