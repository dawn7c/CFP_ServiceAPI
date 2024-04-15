namespace CfpService.Domain.Abstractions
{
    public interface IActivity
    {
        Task<List<object>> ListOfActivityWithDescriptionAsync();
    }
}
