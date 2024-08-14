using System;

namespace CrudVenda.Helpers;

public static class PadHelper
{
    public static string MediumPad(string value)
    {
        return (value.Length > 20 ? value[..17] + "..." : value).PadRight(20);
    }

    public static string MediumPad(string value, char character)
    {
        return (value.Length > 20 ? value[..17] + "..." : value).PadRight(20, character);
    }



    public static string ShortPad(string value)
    {
        return (value.Length > 13 ? value[..10] + "..." : value).PadRight(13);
    }

    public static string ShortPad(string value, char character)
    {
        return (value.Length > 13 ? value[..10] + "..." : value).PadRight(13, character);
    }
}
