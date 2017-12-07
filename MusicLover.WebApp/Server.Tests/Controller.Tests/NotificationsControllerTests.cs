using System;
using System.Collections.Generic;
using System.Text;
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
    public class NotificationsControllerTests
    {
        private readonly NotificationsController _controller;
        private readonly Mock<INotificationRepository> _mockRepository;
        private readonly string _userId;
        public NotificationsControllerTests()
        {
            _mockRepository = new Mock<INotificationRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.NotificationRepository).Returns(_mockRepository.Object);
            var mockMapper = new Mock<IMapper>();
            _controller = new NotificationsController(mockUnitOfWork.Object, mockMapper.Object);
            _userId = "1";
            _controller.MockUser(_userId, "test@domain.com");
        }

        [Fact]
        public async void GetNewNotifications_HappyPath_ShouldReturnOkObject()
        {

            _mockRepository.Setup(g => g.GetNewNotifications(_userId, true)).ReturnsAsync(new List<Notification>());

            var result = await _controller.GetNewNotifications();

            result.Should().BeOfType<OkObjectResult>();
        }
        public async void MarkAsRead_HappyPath_ShouldReturnOkObject()
        {

            _mockRepository.Setup(g => g.GetNewUserNotifications(_userId)).ReturnsAsync(new List<UserNotification>());

            var result = await _controller.MarkAsRead();

            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
