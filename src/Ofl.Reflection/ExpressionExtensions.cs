using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Ofl.Reflection
{
    //////////////////////////////////////////////////
    ///
    /// <author>Nicholas Paldino</author>
    /// <created>2014-02-15</created>
    /// <summary>Extensions for working with <see cref="Expression{TDelegate}"/>
    /// instances.</summary>
    ///
    //////////////////////////////////////////////////
    public static class ExpressionExtensions
    {
        public static PropertyInfo GetPropertyInfo<TSource>(this TSource source,
            Expression<Func<TSource, object>> expression) => source.GetPropertyInfos(expression).Single();

        public static IEnumerable<PropertyInfo> GetPropertyInfos<TSource>(this TSource source,
            params Expression<Func<TSource, object>>[] expressions) =>
                source.GetPropertyInfos((IEnumerable<Expression<Func<TSource, object>>>) expressions);

        public static IEnumerable<PropertyInfo> GetPropertyInfos<TSource>(this TSource source,
            IEnumerable<Expression<Func<TSource, object>>> expressions)
        {
            // Validate parameters.
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (expressions == null) throw new ArgumentNullException(nameof(expressions));

            // Call the implementation.
            return expressions.GetPropertyInfos();
        }

        public static IEnumerable<PropertyInfo> GetPropertyInfos<TSource>(this IEnumerable<Expression<Func<TSource, object>>> expressions)
        {
            // Validate parameters.
            if (expressions == null) throw new ArgumentNullException(nameof(expressions));

            // Call the implementation.
            return expressions.GetPropertyInfosImplementation();
        }

        private static IEnumerable<PropertyInfo> GetPropertyInfosImplementation<TSource>(this IEnumerable<Expression<Func<TSource, object>>> expressions)
        {
            // Validate parameters.
            Debug.Assert(expressions != null);

            // Cycle through the expressions.
            foreach (Expression<Func<TSource, object>> expression in expressions)
            {
                // The member expression.
                var member = expression.Body as MemberExpression;

                // Used to generate the exception, if necessary.
                ArgumentException CreateExpressionNotPropertyException() =>
                    new ArgumentException($"The expression parameter ({ nameof(expression) }) is not a property expression.");

                // If it's a convert, then get the expression in the convert.
                if (member == null && expression.Body.NodeType == ExpressionType.Convert)
                    // Get the convert.
                    member = (expression.Body as UnaryExpression).Operand as MemberExpression;

                // If not a member expression, throw an exception.
                if (member == null)
                    throw CreateExpressionNotPropertyException();

                // Get the property info.
                var propertyInfo = member.Member as PropertyInfo;

                // If it is null, throw an exception.
                if (propertyInfo == null)
                    throw CreateExpressionNotPropertyException();

                // Return the property info.
                yield return propertyInfo;
            }
        }
    }
}
