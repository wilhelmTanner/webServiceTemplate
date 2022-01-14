namespace Tanner.Template.Base.API.Filters;

[ExcludeFromCodeCoverage]
public class HealthRequestFilter : ITelemetryProcessor
{
    private ITelemetryProcessor Next { get; set; }
    public HealthRequestFilter(ITelemetryProcessor next)
    {
        Next = next;
    }
    public void Process(ITelemetry item)
    {
        if (item is RequestTelemetry)
        {

            var request = item as RequestTelemetry;
            string absoluteHealthPath = $"/health";

            if ((bool)(request?.Url?.AbsolutePath.Contains(absoluteHealthPath)))
            {
                return;
            }
        }

        Next.Process(item);
    }
}
