using CfpService.Domain.Models;
using CfpService.DataAccess.DatabaseContext;
using CfpService.DataAccess.ApplicationRepository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CfpService.DataAccessTests
{
    public class ApplicationRepositoryTests
    {
        [Fact]
        public async Task CreateBidAsyncTest()
        {
            var mockDbSet = new Mock<DbSet<CfpService.Domain.Models.Application>>();
            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Set<CfpService.Domain.Models.Application>()).Returns(mockDbSet.Object);
            var repository = new ApplicationRepository(mockContext.Object);
            var application = new CfpService.Domain.Models.Application {
                Id = Guid.NewGuid(),
                Author = Guid.NewGuid(),
                Activity = Activity.Masterclass,
                Name = "Example Application",
                Description = "Example Description",
                Outline = "Example Outline",
            };
            
            await repository.CreateApplicationAsync(application);

            mockDbSet.Verify(m => m.Add(It.IsAny<CfpService.Domain.Models.Application>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}
