using System;
using System.Collections.Generic;
using System.Text;

namespace OrzhansJozve.Utilities
{
    public static class ConvertToK
    {
        public static string ToKNumber(this int value)
        {
            if (value >= 1000)
            {
                return $"{Math.Round((float)value / 1000, 2)}k".Replace(".", ",");
            }
            else if (value >= 1000000)
            {
                return $"{Math.Round((float)value / 1000000, 2)}M".Replace(".", ",");
            }
            return value.ToString();
        }
    }
}
