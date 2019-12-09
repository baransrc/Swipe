using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Utilities
{
    public static class EnumExtensions
    { 
        public static TEnum GetRandomValue<TEnum>()
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var enumType = typeof(TEnum);
            
            if (!enumType.IsEnum)
            {
                throw new ArgumentException();
            }

            var values = Enum.GetValues(enumType);
            var index = Random.Range(0, values.Length);

            return (TEnum) values.GetValue(index);
        }
        
        public static ICollection<TEnum> GetValues<TEnum>()
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var enumType = typeof(TEnum);
            
            if (!enumType.IsEnum)
            {
                throw new ArgumentException();
            }

            var values = Enum.GetValues(enumType);

            return (ICollection<TEnum>) values;
        }
    }
}