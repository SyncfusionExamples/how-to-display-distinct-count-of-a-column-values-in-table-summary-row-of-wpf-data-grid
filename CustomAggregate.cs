using Syncfusion.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SfDataGrid_MVVM
{
    public class CustomAggregate : ISummaryAggregate
    {
        public CustomAggregate()
        {
        }

        public double DistictCount { get; set; }

        public Action<System.Collections.IEnumerable, string, System.ComponentModel.PropertyDescriptor> CalculateAggregateFunc()
        {
            return (items, property, pd) =>
            {
                var enumerableItems = items as IEnumerable<OrderInfo>;

                if (pd.Name == "DistictCount")
                {
                    this.DistictCount = enumerableItems.DistictCount<OrderInfo>(q => q.CustomerID);
                }
            };
        }
    }

    public static class LinqExtensions
    {
        static List<string> list = new List<string>();

        public static double DistictCount<T>(this IEnumerable<T> values, Func<T, string> selector)
        {
            double ret = 0;
            var count = values.Count();
            foreach (var value in values)
            {
                if (!list.Contains((value as OrderInfo).CustomerID))
                {
                    list.Add((value as OrderInfo).CustomerID);
                }
            }
            ret = list.Count;
            return ret;
        }
    }
}
