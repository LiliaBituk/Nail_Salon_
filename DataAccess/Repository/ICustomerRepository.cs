namespace DataAccess
{
    public interface ICustomerRepository
    {
        Task<bool> RecordCustomerAsync();
        bool IsRecordingSuccessful { get; }
    }
}
