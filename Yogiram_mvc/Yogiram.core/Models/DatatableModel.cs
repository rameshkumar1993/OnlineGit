using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace Yogiram.core.Models
{
    public class DtParams
    {
        public int draw { get; set; }
        public Column[] columns { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public Search search { get; set; }
        public OrderBy[] order { get; set; }

        public bool ColumnFilter { get; set; }

        public IEnumerable<dynamic> Order(IEnumerable<dynamic> query)
        {
            IEnumerable<dynamic> ordered = query;

            if (order != null)
            {
                String orderBy = "";

                bool first = true;

                foreach (var item in order)
                {
                    var columnName = columns[item.column].data;

                    if (String.IsNullOrWhiteSpace(columnName))
                        continue;

                    if (first)
                        first = false;
                    else
                        orderBy += ", ";

                    if ("asc".Equals(item.dir, StringComparison.InvariantCultureIgnoreCase))
                    {
                        orderBy += columnName + " asc";
                    }
                    else
                    {
                        orderBy += columnName + " desc";
                    }
                }

                ordered = ordered.OrderBy(orderBy);
            }

            return ordered;
        }

        public IEnumerable<dynamic> Filter(IEnumerable<dynamic> query)
        {
            var filtered = query;

            if (ColumnFilter)
            {
                foreach (var item in columns)
                {
                    if (!String.IsNullOrWhiteSpace(item.searchColumn) && item.searchable)
                    {
                        filtered = item.Filter(filtered);
                    }
                }
            }

            return filtered;
        }

        public Column GetColumn(String ColumnName)
        {
            var column = (from a in columns where ColumnName.Equals(a.data, StringComparison.InvariantCultureIgnoreCase) select a).FirstOrDefault();
            return column;
        }

        public IEnumerable<dynamic> Take(IEnumerable<dynamic> query)
        {
            if (length <= 0)
                return query;
            query = query.Skip(start).Take(length);
            return query;
        }

    }

    public class Column
    {
        public String data { get; set; }
        public String name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
        public SearchOption condition { get; set; }

        public FilterAction CustomFilter { get; set; }

        public delegate IEnumerable<dynamic> FilterAction(IEnumerable<dynamic> query, String value);

        private String _searchColumn;

        public String searchColumn
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_searchColumn))
                    _searchColumn = data;

                return _searchColumn;
            }
            set
            {
                _searchColumn = value;
            }
        }

        public Column()
        {
            condition = SearchOption.CONTAINS;
        }

        public IEnumerable<dynamic> Filter(IEnumerable<dynamic> query)
        {
            if (!String.IsNullOrWhiteSpace(searchColumn) && search == null || !String.IsNullOrWhiteSpace(search.value))
            {
                var type = query.GetType().GetGenericArguments()[0];

                var property = type.GetProperty(searchColumn);

                var propertyType = property.PropertyType;

                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = propertyType.GetGenericArguments()[0];
                }

                try
                {
                    var value = condition == SearchOption.CUSTOM ? null : TypeDescriptor.GetConverter(propertyType).ConvertFromInvariantString(search.value);

                    object value2 = null;
                    var useValue2 = false;

                    var exp = String.Empty;

                    switch (condition)
                    {
                        case SearchOption.LESS_THAN:
                            exp = searchColumn + " < @0";
                            break;
                        case SearchOption.LESS_THAN_EQUAL_TO:
                            exp = searchColumn + " <= @0";
                            break;
                        case SearchOption.GREATER_THAN:
                            exp = searchColumn + " > @0";
                            break;
                        case SearchOption.GREATER_THAN_EQUAL_TO:
                            exp = searchColumn + " >= @0";
                            break;
                        case SearchOption.EQUAL_TO:
                            exp = searchColumn + " = @0";
                            break;
                        case SearchOption.NOT_EQUAL_TO:
                            exp = searchColumn + " <> @0";
                            break;
                        case SearchOption.DATE_EQUAL_TO:
                            if (value is DateTime? || value is DateTime)
                            {
                                var d = value as DateTime?;
                                d = d.Value.Date;
                                value = d.Value;
                                useValue2 = true;
                                value2 = d.Value.AddDays(1).AddMilliseconds(-1);
                                exp = "(" + searchColumn + " >= @0 AND " + searchColumn + " <= @1)";
                                break;
                            }
                            exp = searchColumn + " = @0";
                            break;
                        case SearchOption.MONTH_EQUAL_TO:
                            if (value is DateTime? || value is DateTime)
                            {
                                var d = value as DateTime?;
                                d = d.Value.Date;
                                value = new DateTime(d.Value.Year, d.Value.Month, 1);
                                useValue2 = true;
                                value2 = d.Value.AddMonths(1).AddMilliseconds(-1);
                                exp = "(" + searchColumn + " >= @0 AND " + searchColumn + " <= @1)";
                                break;
                            }
                            exp = searchColumn + " = @0";
                            break;
                        case SearchOption.DATE_NOT_EQUAL_TO:
                            if (value is DateTime || value is DateTime)
                            {
                                var d = value as DateTime?;
                                d = d.Value.Date;
                                value = d.Value;
                                useValue2 = true;
                                value2 = d.Value.AddDays(1).AddMilliseconds(-1);
                                exp = "(" + searchColumn + " < @0 OR " + searchColumn + " > @1)";
                                break;
                            }
                            exp = searchColumn + " <> @0";
                            break;
                        case SearchOption.CONTAINS:
                            exp = searchColumn + ".Contains(@0)";
                            break;
                        case SearchOption.START_WITH:
                            exp = searchColumn + ".StartsWith(@0)";
                            break;
                        case SearchOption.END_WITH:
                            exp = searchColumn + ".EndsWith(@0)";
                            break;
                        default:
                            break;
                    }

                    if (condition == SearchOption.CUSTOM)
                        return CustomFilter(query, search.value);
                    else if (useValue2)
                        return query.Where(exp, value, value2);
                    else
                        return query.Where(exp, value);
                }
                catch (Exception)
                {

                }
            }

            return query;
        }

    }

    public class OrderBy
    {
        public int column { get; set; }
        public String dir { get; set; }
    }

    public class Response
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public virtual IEnumerable<dynamic> data { get; set; }
    }


    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public enum SearchOption
    {
        LESS_THAN,
        LESS_THAN_EQUAL_TO,
        GREATER_THAN,
        GREATER_THAN_EQUAL_TO,
        EQUAL_TO,
        NOT_EQUAL_TO,
        DATE_EQUAL_TO,
        DATE_NOT_EQUAL_TO,
        MONTH_EQUAL_TO,
        CONTAINS,
        START_WITH,
        END_WITH,
        CUSTOM
    }

    

}
