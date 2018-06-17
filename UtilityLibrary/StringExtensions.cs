using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLibrary
{
    public static class StringExtensions
    {
        public static string JoinWithLastSeparator(this string[] sender, string delimitor, string lastDelimitor)
        {
            string result = string.Join(delimitor + " ",
                                sender.Take(sender.Length - 1)) +
                            ((((sender.Length <= 1)
                                 ? ""
                                 : lastDelimitor)) +
                             sender.LastOrDefault());
            return result;
        }

        public static string JoinCondition(this string[] sender)
        {
            return sender.JoinWithLastSeparator(" AND ", " AND ");
        }

    }
}
