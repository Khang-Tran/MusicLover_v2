using MusicLover.WebApp.Server.Persistent.Repositories.Commons;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicLover.WebApp.Controllers;
using MusicLover.WebApp.Server.Controllers.APIs;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;
using Xunit;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;

namespace Server.Tests.Controller.Tests
{
    public class AttendancesControllerTests
    {
        private readonly AttendancesController _controller;
        private readonly Mock<IAttendanceRepository> _mockRepository;
        private readonly string _userId;
        public AttendancesControllerTests()
        {
            _mockRepository = new Mock<IAttendanceRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.AttendanceRepository).Returns(_mockRepository.Object);
          
            _controller = new AttendancesController(mockUnitOfWork.Object);
            _userId = "1";
            _controller.MockUser(_userId, "test@domain.com");
        }

        [Fact]
        public async void Attend_HappyPath_ShouldReturnOkObject()
        {
            var gigId = 1;

            _mockRepository.Setup(g => g.IsExisted(gigId, _userId)).ReturnsAsync(false);

            var result = await _controller.Attend(gigId);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Attend_ExistedGig_ShouldReturnBadRequestObject()
        {
            var gigId = 1;

            _mockRepository.Setup(g => g.IsExisted(gigId, _userId)).ReturnsAsync(true);

            var result = await _controller.Attend(gigId);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void Delete_HappyPath_ShouldReturnOkObject()
        {
            var gigId = 1;
            var attendance = new Attendance();

            _mockRepository.Setup(g => g.GetAttendance(gigId, _userId)).ReturnsAsync(attendance);

            var result = await _controller.Delete(gigId);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Delete_AttendanceNotExisted_ReturnNotFoundObject()
        {
            var gigId = 1;
            _mockRepository.Setup(g => g.GetAttendance(gigId, _userId)).ReturnsAsync((Attendance)null);
            var result = await _controller.Delete(gigId);

            result.Should().BeOfType<NotFoundObjectResult>();
        }


        
    }
  
}
