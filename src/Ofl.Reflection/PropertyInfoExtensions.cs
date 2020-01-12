using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Ofl.Reflection
{
    public static class PropertyInfoExtensions
    {
        public static IEnumerable<PropertyInfo> Exclude<T>(
            this IEnumerable<PropertyInfo> properties, 
            T shape, 
            params Expression<Func<T, object>>[] expressions
        ) => properties.Exclude(shape, (IEnumerable<Expression<Func<T, object>>>) expressions);

        public static IEnumerable<PropertyInfo> Exclude<T>(
            this IEnumerable<PropertyInfo> properties,
#pragma warning disable IDE0060 // Remove unused parameter
            T shape,
#pragma warning restore IDE0060 // Remove unused parameter
            IEnumerable<Expression<Func<T, object>>> expressions
        ) => properties.Exclude(expressions);

        public static IEnumerable<PropertyInfo> Exclude<T>(
            this IEnumerable<PropertyInfo> properties, 
            params Expression<Func<T, object>>[] expressions
        ) => properties.Exclude((IEnumerable<Expression<Func<T, object>>>) expressions);

        public static IEnumerable<PropertyInfo> Exclude<T>(
            this IEnumerable<PropertyInfo> properties, 
            IEnumerable<Expression<Func<T, object>>> expressions
        )
        {
            // Validate parameters.
            if (properties == null) throw new ArgumentNullException(nameof(properties));
            if (expressions == null) throw new ArgumentNullException(nameof(expressions));

            // The sets of property infos to filter out.
            // Populate with the property infos in
            // the expressions.
            ISet<PropertyInfo> excluded = new HashSet<PropertyInfo>(expressions.GetPropertyInfos());

            // Filter and exclude the properties.
            return properties.Where(p => !excluded.Contains(p));
        }
    }
}
