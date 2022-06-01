using NPOI.SS.UserModel;
using System.Reflection;
using System.Text.Json;

namespace Utils
{
    public static class Extentions
    {
        public static Dictionary<string, object> ObjectToDictionaries<T>(this T obj) => obj.GetType()
                                                                                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                                                        .ToDictionary(p => StringToCamelCase(p.Name), p => p.GetValue(obj));

        public static string StringToCamelCase(string inputString) => JsonNamingPolicy.CamelCase.ConvertName(inputString);

        public static string TrimToUpperCase(this string str)
        {
            return str is not null ? str.Trim().ToUpper() : null;
        }

        public static void SetCellStyle(IRow row, int columnCount, params ICellStyle[] styles)
        {
            for (var i = 0; i < columnCount; i++)
            {
                foreach (var style in styles)
                {
                    row.Cells[i].CellStyle = style;
                }
            }
        }
    }
}