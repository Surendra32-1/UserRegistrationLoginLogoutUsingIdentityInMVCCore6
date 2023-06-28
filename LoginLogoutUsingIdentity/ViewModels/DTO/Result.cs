namespace LoginLogoutUsingIdentity.ViewModels.DTO
{
    public class Result
    {
        public Status status { get; set; }
        public string Message { get; set; }
    }
    public enum Status
    {
        Success,
        Failure
    }
}
