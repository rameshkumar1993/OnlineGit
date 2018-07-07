using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yogiram.core.Context;

namespace Yogiram.core.Handler
{
    public class TerminologyProcessor
    {
        public ContextContainer Context { get; private set; }
        public static Regex Regex = new Regex(@"\$(\w+)\$", RegexOptions.Compiled);
        private Dictionary<String, String> Cache;

        public TerminologyProcessor(ContextContainer Context)
        {
            this.Context = Context;
            Cache = new Dictionary<string, string>();
        }

        public String Process(String Text)
        {
            return Process(Text, null);
        }

        public String Process(String Text, CultureInfo Culture)
        {
            if (Text == null || Text.IndexOf('$') == -1)
                return Text;

            MatchCollection matches = Regex.Matches(Text);

            List<String> terms = new List<string>();

            foreach (Match match in matches)
            {
                terms.Add(Text.Substring(match.Index + 1, match.Length - 2));
            }

            var values = GetTerminology(terms, Culture);

            StringBuilder builder = new StringBuilder();
            int lastIndex = 0;

            foreach (Match match in matches)
            {
                String code = Text.Substring(match.Index + 1, match.Length - 2);

                String value = values.ContainsKey(code) ? values[code] : String.Empty;

                // Before
                builder.Append(Text.Substring(lastIndex, match.Index - lastIndex));

                // Term
                builder.Append(value);

                lastIndex = match.Index + match.Length;
            }

            builder.Append(Text.Substring(lastIndex, Text.Length - lastIndex));

            return builder.ToString();
        }

        private Dictionary<String, String> GetTerminology(List<string> terms, CultureInfo Culture)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            HashSet<string> missing = new HashSet<string>();

            if (Culture == null)
            {
                if (Cache.Count >= terms.Count)
                {
                    foreach (var item in terms)
                    {
                        if (Cache.ContainsKey(item))
                            values[item] = Cache[item];
                        else
                            missing.Add(item);
                    }
                }
                else
                {
                    foreach (var item in terms)
                    {
                        missing.Add(item);
                    }
                }
            }

            //if (missing.Count > 0)
            //{
            //    try
            //    {
            //        String CurrentCulture = (Culture ?? Context.CurrentCulture).Name;

            //        var fromDB = (from a in Context.CoreEntities.OM_Terminology
            //                      where missing.Contains(a.TerminologyCode)
            //                      select
            //                          new
            //                          {
            //                              Code = a.TerminologyCode,
            //                              ModuleId = a.ModuleId,
            //                              CurrentCulture = (from b in a.OM_Terminology_Culture where b.CultureCode == CurrentCulture select b.Value).FirstOrDefault(),
            //                              DefaultCulture = (from b in a.OM_Terminology_Culture where b.CultureCode == String.Empty select b.Value).FirstOrDefault()
            //                          }).ToList();

            //        foreach (var item in fromDB)
            //        {
            //            String value = String.Empty;

            //            if (!String.IsNullOrWhiteSpace(item.CurrentCulture))
            //                value = item.CurrentCulture;
            //            else if (!String.IsNullOrWhiteSpace(item.DefaultCulture))
            //                value = item.DefaultCulture;
            //            else if (item.ModuleId == null)
            //                value = Context.G(item.Code);
            //            else
            //                value = Context.M(item.ModuleId.Value, item.Code);

            //            if (value == null)
            //                value = string.Empty;

            //            values[item.Code] = value;

            //            if (Culture == null && !Cache.ContainsKey(item.Code))
            //                Cache.Add(item.Code, value);
            //        }
            //    }
            //    catch (Exception)
            //    {

            //    }
            //}

            return values;
        }
    }
}
