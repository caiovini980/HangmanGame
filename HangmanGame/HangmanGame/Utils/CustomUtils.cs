namespace HangmanGame.Utils;

public static class GeneralUtils
{
    public static int[] FindAllIndexesOf<T>(this IEnumerable<T> values, T item)
    {
        return values.Select((type, i) => Equals(type, item) ? i : -1).Where(i => i != -1).ToArray();
    }
}

public static class FileUtils
{
    public static string[]? GetSeparatedStrings(string separator, string path)
    {
        if (!File.Exists(path)) return null;
        
        string figures = File.ReadAllText(path);
        return figures.Split(separator);
    }
}