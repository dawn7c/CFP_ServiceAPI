using Domain.Abstractions;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;

namespace CFP_Tests.WebTests
{
    public class BidControllerTests
    {
        [Fact]
        public async Task SendBidAsyncWhenBidExistsAndNotSentTest()
        {
            // Arrange
            var bidId = Guid.NewGuid();
            var mockBidRepository = new Mock<IRepository<Bid>>();
            var mockActivityRepository = new Mock<IActivityRepository<Activity>>();
            var mockLogger = new Mock<ILogger<BidController>>();
            var mockContext = new Mock<ApplicationContext>();
            var controller = new BidController(mockContext.Object,mockBidRepository.Object, mockActivityRepository.Object, mockLogger.Object);
            var bid = new Bid { Id = bidId, IsSend = false };

            mockBidRepository.Setup(repo => repo.GetBidId(bidId)).ReturnsAsync(bid);

            // Act
            var result = await controller.SendBidAsync(bidId);

            // Assert
            Assert.IsType<OkResult>(result);
            Assert.True(bid.IsSend);
            Assert.NotNull(bid.SendDateTime);
            mockBidRepository.Verify(repo => repo.Update(bid), Times.Once);
        }

        [Fact]
        public async Task SendBidAsyncWhenBidExistsAndAlreadySentReturnsNotFoundTest()
        {
            // Arrange
            var bidId = Guid.NewGuid();
            var mockBidRepository = new Mock<IRepository<Bid>>();
            var mockActivityRepository = new Mock<IActivityRepository<Activity>>();
            var mockLogger = new Mock<ILogger<BidController>>();
            var mockContext = new Mock<ApplicationContext>();
            var controller = new BidController(mockContext.Object, mockBidRepository.Object, mockActivityRepository.Object, mockLogger.Object);
            var bid = new Bid { Id = bidId, IsSend = true };

            mockBidRepository.Setup(repo => repo.GetBidId(bidId)).ReturnsAsync(bid);

            // Act
            var result = await controller.SendBidAsync(bidId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Заявка была отправлена ранее", notFoundResult.Value);
            mockBidRepository.Verify(repo => repo.Update(bid), Times.Never);
        }
    }
}
