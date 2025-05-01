using System.Reflection;
using UnityEngine;

namespace ChatTweaksPlugin
{
    public class DataUtils
    {
        public static bool TryParseColor(string input, out Color color)
        {
            if (ColorUtility.TryParseHtmlString(input, out color))
                return true;
            var colorType = typeof(Color);
            var property = colorType.GetProperty(input, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
            if (property != null && property.PropertyType == typeof(Color))
            {
                color = (Color)property.GetValue(null, null);
                return true;
            }
            color = default;
            return false;
        }
    }
}