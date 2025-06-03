using System;
using System.Linq;

namespace Serein
{
    public static class TypeExtensions
    {
        public static string GetCorrectGenericName(this Type type)
        {
            if (type.IsGenericType == false)
                return type.Name;

            string typeName = type.Name.Split('`')[0];
            string genericArgs = string.Join(", ", type.GetGenericArguments().Select(t => t.Name));

            return $"{typeName}<{genericArgs}>";
        }
    }
}
