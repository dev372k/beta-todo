namespace Shared.Extensions;

public static class EnumExtensions
{
    public static string GetString<T>(this T value)
    {
		try
		{
            if (value != null)
                return value.ToString()!.Replace("_", " ");
            return value.ToString();
        }
		catch (Exception)
		{
            return value.ToString();
		}
    }
}
