namespace Tanner.Template.Base.Common.Extensions;

public static class ListExtensions
{
    public static List<TSource> ToDefaultList<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null)
            return new List<TSource>();
        else
            return source.ToList();
    }

    public static bool IsEmptyOrNull<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null)
            return true;
        else
            return !source.Any();
    }
}
