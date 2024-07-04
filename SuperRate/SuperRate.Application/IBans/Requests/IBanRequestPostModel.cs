namespace SuperRate.Application.IBans.Requests;

public class IBanRequestPostModel
{
    public string IBanNumber { get; set; } = default!;
    public int UserId { get; set; }
}