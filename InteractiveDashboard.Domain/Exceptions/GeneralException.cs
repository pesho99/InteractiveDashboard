namespace InteractiveDashboard.Domain.Exceptions
{
    public class GeneralException : Exception
    {
        public GeneralException(string error)
        {
            Error = error;
        }

        public string Error { get; }
    }
}
