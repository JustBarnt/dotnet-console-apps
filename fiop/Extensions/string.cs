namespace FileOP.Extensions;

public static class StringExtensions
{
    public static string TrimSubstring(this string str, string start, string end = null)
    {
        end ??= start;
        
        if (str.StartsWith(start, StringComparison.Ordinal))
            str = str.Substring(start.Length);
        
        if (str.EndsWith(end, StringComparison.Ordinal))
            str = str.Substring(0, str.Length - end.Length);

        return str;
    }
}