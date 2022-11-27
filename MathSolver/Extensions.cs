using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathSolver
{
    public static class Extensions
    {
        public static string[] SplitWithSeparators(this string str, string[] separators)
        {
            if (str == null || separators == null)
                throw new ArgumentNullException();

            var parts = new List<string>();

            var token = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                if (separators.Contains(str[i].ToString()))
                {
                    if (token.Length > 0)
                    {
                        parts.Add(token.ToString());
                        token.Clear();
                    }

                    parts.Add(str[i].ToString());
                }
                else
                {
                    token.Append(str[i]);
                }
            }

            if (token.Length > 0)
            {
                parts.Add(token.ToString());
            }

            return parts.ToArray();
        }
    }
}