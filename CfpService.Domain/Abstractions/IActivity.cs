namespace Domain.Abstractions
{
    public interface IActivity
    {
        Task<List<object>> ListOfActivityWithDescriptionAsync();
    }
}
