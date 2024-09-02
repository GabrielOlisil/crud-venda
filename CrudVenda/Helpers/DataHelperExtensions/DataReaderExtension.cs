using System;
using System.Data;

namespace CrudVenda.Helpers;

public static class DataReaderExtensions
{
    public static double? GetNullableDouble(this IDataReader reader, string columnName)
    {
        int ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? null : reader.GetDouble(ordinal);
    }

}
