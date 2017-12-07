using System;
using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicLover.WebApp.Server.Controllers.APIs;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;
using Xunit;

namespace Server.Tests.Controller.Tests
{
    public class FolloweeControllerTests
    {
        private readonly FolloweesController _controller;
        private readonly Mock<IFolloweeRepository> _mockRepository;
        private readonly string _userId;
        public FolloweeControllerTests()
        {
            _mockRepository = new Mock<IFolloweeRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.FolloweeRepository).Returns(_mockRepository.Object);

            var mockMapper = new Mock<IMapper>();
            _controller = new FolloweesController(mockUnitOfWork.Object, mockMapper.Object);
            _userId = "1";
            _controller.MockUser(_userId, "test@domain.com");
        }

        [Fact]
        public async void GetFollowees_HappyPath_ShouldReturnOkObject()
        {
            _mockRepository.Setup(g => g.GetAllFollowees(_userId)).ReturnsAsync(new List<ApplicationUser>());

            var result = await _controller.GetFollowees();

            result.Should().BeOfType<OkObjectResult>();
        }

      

    }
}
