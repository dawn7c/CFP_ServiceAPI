using CfpService.Api.Mapping;
using CfpService.Application.Validation;
using CfpService.Domain.Models;
using Domain.Abstractions;
using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;

namespace CFP_Tests.WebTests
{
    public class ApplicationControllerTests
    {
        [Fact]
        public async Task SendBidAsyncWhenBidExistsAndNotSentTest()
        {
            var id = Guid.NewGuid();
            var mockApplicationRepository = new Mock<IApplication>();
            var mockActivityRepository = new Mock<IActivity>();
            var mockContext = new Mock<ApplicationContext>();
            mockApplicationRepository.Setup(repo => repo.ApplicationByIdAsync(id)).ReturnsAsync((Application)null);
            var validator = new ApplicationValidator(); 
            var mappingService = new ApplicationMappingService(); 
            var controller = new ApplicationController(mockContext.Object, mockApplicationRepository.Object, mockActivityRepository.Object);
            var result = await controller.PostApplicationAsync(id);
            Assert.IsType<NotFoundObjectResult>(result);
        }

    }
}
