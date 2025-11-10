using System;
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
                sb.Append('<');
                foreach (var type in t.GetGenericArguments())
                {
                    sb.Append(type.BeautifulName());
                    sb.Append(',');
                }
                // Change the last ',' appended by the loop to the closing angle bracket so we do not have to track the first or last index in the loop, and since the closing bracket is appended anyways this also costs no capacity or extra allocation
                // Use .AppendJoin method for the loop when .netstandard2.1 is available
                sb.Replace(oldChar: ',', newChar: '>', startIndex: sb.Length - 1, count: 1);

                return sb.ToString();
            }
            catch (Exception)
            {
                return t.Name;
            }
        }
    }
}