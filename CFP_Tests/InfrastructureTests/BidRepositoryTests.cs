using Domain.Models;
using Infrastructure.DatabaseContext;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace CFP_Tests.InfrastructureTests
{
    public class BidRepositoryTests
    {
    //    [Fact]
    //    public async Task CreateBidAsyncTest()
    //    {
    //        // Arrange
    //        var bids = new List<Bid>(); 
    //        var mockDbSet = new Mock<DbSet<Bid>>(); 
    //        mockDbSet.As<IQueryable<Bid>>().Setup(m => m.Provider).Returns(bids.AsQueryable().Provider);
    //        mockDbSet.As<IQueryable<Bid>>().Setup(m => m.Expression).Returns(bids.AsQueryable().Expression);
    //        mockDbSet.As<IQueryable<Bid>>().Setup(m => m.ElementType).Returns(bids.AsQueryable().ElementType);
    //        mockDbSet.As<IQueryable<Bid>>().Setup(m => m.GetEnumerator()).Returns(bids.GetEnumerator());

    //        var mockContext = new Mock<ApplicationContext>(); 
    //        mockContext.Setup(c => c.Set<Bid>()).Returns(mockDbSet.Object); 

    //        var repository = new BidRepository<Bid>(mockContext.Object);
    //        var bid = new Bid();

    //        // Act
    //        await repository.CreateBidAsync(bid);

    //        // Assert
    //        mockDbSet.Verify(m => m.Add(bid), Times.Once); 
    //        mockContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
    //    } 
    }
}
