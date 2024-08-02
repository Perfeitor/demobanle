using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MUDTEMPLATE.Shared.Extensions
{
    public static class DataTableExtensions
    {
        public static T AsObject<T>(this DataRow row)
        {
            try
            {
                Type type = typeof(T);
                T obj = Activator.CreateInstance<T>();

                string data = JsonConvert.SerializeObject(row);

                obj = JsonConvert.DeserializeObject<T>(data) ?? default(T)!;

                return obj;
            }
            catch (Exception)
            {
                return default(T)!;
            }
        }

        public static List<T>? AsListObject<T>(this DataTable table)
        {
            if (table == null)
                return null;

            var results = new List<T>();

            string data= JsonConvert.SerializeObject(table);

            results = JsonConvert.DeserializeObject<List<T>>(data);

            return results;
        }
    }
}
