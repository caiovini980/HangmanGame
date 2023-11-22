namespace HangmanGame.Utils;

public static class CustomUtils
{
    public static int[] FindAllIndexesOf<T>(this IEnumerable<T> values, T item)
    {
        return values.Select((type, i) => Equals(type, item) ? i : -1).Where(i => i != -1).ToArray();
    }
}