using System.Text;

namespace MathSolver;

public static class EnumerableExtensions
{
    public static void PrintList<T>(this List<T> list)
    {
        var sb = new StringBuilder("{   ");
        foreach (var item in list)
        {
            sb.Append($"{item}   ");
        }

        sb.Append("}");
        Console.WriteLine(sb);
    }

    public static void PrintArray<T>(this T[] array)
    {
        var sb = new StringBuilder("{   ");
        foreach (var item in array)
        {
            sb.Append($"{item}   ");
        }

        sb.Append("}");
        Console.WriteLine(sb);
    }
}