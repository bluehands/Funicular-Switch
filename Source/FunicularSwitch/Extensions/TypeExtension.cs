using System;
using System.Linq;
using System.Text;

namespace FunicularSwitch.Extensions
{
    public static class TypeExtension
    {
        public static string BeautifulName(this Type t)
        {
            if (!t.IsGenericType)
                return t.Name;
            try
            {
                var sb = new StringBuilder();

                var index = t.Name.LastIndexOf("`", StringComparison.Ordinal);
                if (index < 0)
                    return t.Name;


                sb.Append(t.Name.Substring(0, index));
                var i = 0;
                t.GetGenericArguments().Aggregate(sb, (a, type) => a.Append(i++ == 0 ? "<" : ",").Append(BeautifulName(type)));

                sb.Append(">");

                return sb.ToString();
            }
            catch (Exception)
            {
                return t.Name;
            }
        }
    }
}