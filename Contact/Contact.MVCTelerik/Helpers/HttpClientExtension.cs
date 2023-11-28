using System;
using System.Reflection;

namespace Contact.MVC.Helpers
{
    public static class HttpClientExtension
    {
        public static string ToQueryString(this object obj)
        {
            if (obj == null)
                return "";

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var keyValuePairs = new List<string>();

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                if (value != null)
                {
                    keyValuePairs.Add($"{property.Name}={Uri.EscapeDataString(value.ToString())}");
                }
            }

            return string.Join("&", keyValuePairs);
        }
    }
}

