namespace InteractiveDashboard.Domain.Exceptions
{
    public class MultipleException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public MultipleException(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
