using CfpService.Application.Validation;

namespace CfpService.Tests.CfpService.ApplicationTests
{
    public class ApplicationTests
    {
        [Fact]
        public void ValidateNullOrEmpty_Should_Return_True_For_Non_Empty_Value()
        {
            var repository = new ApplicationValidator();
            string nonEmptyValue = "example";

            var result = repository.ValidateNullOrEmpty(nonEmptyValue, "FieldName");

            Assert.True(result.IsValid);
        }
    }
}
