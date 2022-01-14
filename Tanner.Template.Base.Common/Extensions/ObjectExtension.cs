namespace Tanner.Template.Base.Common.Extensions;

public static class ObjetExtensions
{
    public static string Serialize<T>(this T source)
    {
        var output = JsonSerializer.Serialize<T>(source);
        return output;
    }

    public static T Deserialize<T>(this string source)
    {
        var output = JsonSerializer.Deserialize<T>(source);
        return output;
    }

}