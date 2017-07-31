using System;

namespace Bulka.Helpers
{
    public static class DecimalExtentions
    {
        public static string ToString(this Decimal amount, bool withSeperation)
        {
            return amount.ToString("N0");
        }
    }
}