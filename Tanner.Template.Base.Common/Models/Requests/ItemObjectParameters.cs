namespace Tanner.Template.Base.Common.Models.Requests;

public class ItemObjectParameters : RequestParameters
{
    public ItemObjectParameters() => Sort = "Name";
    public uint MinYear { get; set; }
    public uint MaxYear { get; set; } = int.MaxValue;
    public bool ValidAgeRange => MaxYear > MinYear;
    public string? SearchTerm { get; set; }
}
