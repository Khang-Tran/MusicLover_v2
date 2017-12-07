using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using MusicLover.WebApp.Server.Core.Contracts;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace Server.Tests.Controller.Tests.Commons
{
    public class ControllerTestBase<TController, URepository, IURepository>
        where TController: Microsoft.AspNetCore.Mvc.Controller
        where IURepository: class, IDataRepository
        where URepository: IURepository
    {
        private readonly TController _controller;
        private readonly Mock<IURepository> _mockRepository;
        private readonly string _userId;
        public ControllerTestBase()
        {
            _mockRepository = new Mock<IURepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.URepository).Returns(_mockRepository.Object);

            _controller = new TController(mockUnitOfWork.Object);
            _userId = "1";
            _controller.MockUser(_userId, "test@domain.com");
        }

    }
}
