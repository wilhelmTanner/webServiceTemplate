namespace Tanner.Template.Base.Common.Models.Requests;

public class ExampleObjectParameters : RequestParameters
{
    public ExampleObjectParameters() => Sort = "Descripcion";
    public uint MinValue { get; set; }
    public uint MaxValue { get; set; } = int.MaxValue;
    public bool ValidValueRange => MaxValue > MinValue;
    public string? SearchTerm { get; set; }
}
