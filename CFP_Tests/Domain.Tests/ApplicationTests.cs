using CfpService.Domain.Models;
using Domain.Models;

namespace CFP_Tests.DomainTests
{
    public class ApplicationTests
    {
        [Fact]
        public void ApplicationConstructorSetsPropertiesCorrectly()
        {
            
            var author = Guid.NewGuid();
            var activity = Activity.Report;
            var name = "Test Application";
            var description = "Test Description";
            var outline = "Test Outline";
            var expectedCreateDateTime = DateTime.UtcNow;

            var application = new Application(author, activity, name, description, outline);

            Assert.Equal(author, application.Author);
            Assert.Equal(activity, application.Activity);
            Assert.Equal(name, application.Name);
            Assert.Equal(description, application.Description);
            Assert.Equal(outline, application.Outline);
            Assert.Equal(expectedCreateDateTime.Date, application.CreateDateTime.Date);
            Assert.False(application.IsSend);
            Assert.Null(application.SendDateTime);
        }
    }
}