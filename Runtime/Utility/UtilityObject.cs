using UnityEngine;

namespace TurtleGames.Framework.Runtime.Utility
{
    public static class UtilityObject
    {
        
        public static object GetProperty(Object obj, string name)
        {
            var propertyInfo = obj.GetType().GetProperty(name);
            var value = propertyInfo.GetValue(obj, null);
            return value;
        }
        
        public static string SanitizeObjectName(string name){
            // Removes clone word
            return name.Substring(0, name.ToLower().IndexOf("(clone)"));
        }

    }
}