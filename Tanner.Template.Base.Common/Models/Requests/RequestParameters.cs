namespace Tanner.Template.Base.Common.Models.Requests;

public abstract class RequestParameters
{
    const int maxLimit = 100;
    public int Offset { get; set; } = 0;
    private int _limit = 10;
    public int Limit
    {
        get => _limit;
        set => _limit = (value > maxLimit) ? maxLimit : value;
    }
    public string? Fields { get; set; }
    public string? Sort { get; set; }
}
