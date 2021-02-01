using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace ParityFactory.Weather.Data.Extensions
{
    public static class ListExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            for (int i = 0, count = props.Count; i < count; i++)
            {
                var prop = props[i];
                var column = new DataColumn(prop.Name);
                if (prop.PropertyType.IsGenericType &&
                    prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    column.AllowDBNull = true;
                    column.DataType = Nullable.GetUnderlyingType(prop.PropertyType);
                }
                else
                    column.DataType = prop.PropertyType;
                table.Columns.Add(column);
            }
            var values = new object[props.Count];
            foreach (var item in data)
            {
                for (int i = 0, len = values.Length; i < len; i++)
                {
                    var value = props[i].GetValue(item);
                    values[i] = table.Columns[i].AllowDBNull && value == null ? DBNull.Value : value;
                }
                table.Rows.Add(values);
            }
            return table;
        }

    }
}
