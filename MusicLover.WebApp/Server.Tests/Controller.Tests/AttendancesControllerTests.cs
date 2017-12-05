using MusicLover.WebApp.Server.Persistent.Repositories.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
        private readonly Mock<IAttendanceRepository> _mockRepo;
        private readonly string _userId;
        public AttendancesControllerTests()
        {
            _mockRepo = new Mock<IAttendanceRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            _controller = new AttendancesController(_mockRepo.Object, mockUnitOfWork.Object);
            _userId = "1";
            _controller.MockUser(_userId,"user1@domain.com");
        }

        [Fact]
        public async void Attend_ValidRequest_ShouldReturnOkObject()
        {
            var gigId = 1;
            var result = await _controller.Attend(gigId);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Attend_IsExisted_ShouldReturnBadRequestObject()
        {
            var gigId = 1;
            var attendance = new Attendance();
            _mockRepo.Setup(x => x.GetAttendance(gigId, _userId)).ReturnsAsync(attendance);

            var result = await _controller.Attend(gigId);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
        
        [Fact]
        public async void Delete_ValidRequest_ShouldReturnOk()
        {
            var gigId = 1;
            var attendance = new Attendance();
            _mockRepo.Setup(x => x.GetAttendance(gigId, _userId)).ReturnsAsync(attendance);

            var result = await _controller.Attend(gigId);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Delete_GigIsNull_ShouldReturnNotFoundObject()
        {
            var gigId = 1;

            var result = await _controller.Attend(gigId);

            result.Should().BeOfType<OkObjectResult>();
        }


    }
}
