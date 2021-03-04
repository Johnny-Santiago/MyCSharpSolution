using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public enum SearchOption { Equals, StartsWith, Contains, EndsWith }

    public static class SearchOptions
    {
        public static SearchOption ToSearchOptionEnum(this string value)
        {
            return (SearchOption)Enum.Parse(typeof(SearchOption), value, true);
        }

        public static SearchOption ToSearchOptionEnum(this string value, bool ignoreCase)
        {
            return (SearchOption)Enum.Parse(typeof(SearchOption), value, ignoreCase);
        }
    }
}
