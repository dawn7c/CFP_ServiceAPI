using CfpService.Domain.Models;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CFP_Tests.InfrastructureTests
{
    public class ApplicationRepositoryTests
    {
        [Fact]
        public async Task CreateBidAsyncTest()
        {

            var mockDbSet = new Mock<DbSet<Application>>();
            var mockContext = new Mock<ApplicationContext>();

            mockContext.Setup(c => c.Set<Application>()).Returns(mockDbSet.Object);

            var repository = new ApplicationRepository(mockContext.Object);

            var application = new Application {
                Id = Guid.NewGuid(),
                Author = Guid.NewGuid(),
                Activity = Activity.Masterclass,
                Name = "Example Application",
                Description = "Example Description",
                Outline = "Example Outline",
            };
            
            await repository.CreateApplicationAsync(application);

            mockDbSet.Verify(m => m.Add(It.IsAny<Application>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}
