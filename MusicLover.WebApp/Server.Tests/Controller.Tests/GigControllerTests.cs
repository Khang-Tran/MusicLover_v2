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

      
    }
}
