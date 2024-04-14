using CfpService.Domain.Models;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace CFP_Tests.InfrastructureTests
{
    //public class ApplicationRepositoryTests
    //{
    //    [Fact]
    //    public async Task CreateBidAsyncTest()
    //    {
            
    //        var applications = new List<Application>();
    //        var mockDbSet = new Mock<DbSet<Application>>();
    //        mockDbSet.As<IQueryable<Application>>().Setup(m => m.Provider).Returns(applications.AsQueryable().Provider);
    //        mockDbSet.As<IQueryable<Application>>().Setup(m => m.Expression).Returns(applications.AsQueryable().Expression);
    //        mockDbSet.As<IQueryable<Application>>().Setup(m => m.ElementType).Returns(applications.AsQueryable().ElementType);
    //        mockDbSet.As<IQueryable<Application>>().Setup(m => m.GetEnumerator()).Returns(applications.GetEnumerator());

    //        var mockContext = new Mock<ApplicationContext>();
    //        mockContext.Setup(c => c.Set<Application>()).Returns(mockDbSet.Object);

    //        var repository = new ApplicationRepository<Domain.Models.Application>(mockContext.Object);
    //        var bid = new Application();

            
    //        await repository.CreateBidAsync(bid);

            
    //        mockDbSet.Verify(m => m.Add(bid), Times.Once);
    //        mockContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
    //    }
    //}
}
