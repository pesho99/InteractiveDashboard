namespace InteractiveDashboard.Domain.Dtos
{
    public record ErrorResponse(IEnumerable<string> Errors, int ErrorCode);

}
