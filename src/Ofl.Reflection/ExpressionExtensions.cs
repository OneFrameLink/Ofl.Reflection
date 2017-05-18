using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Ofl.Reflection
{
    public static class ExpressionExtensions
    {
        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, object>> expression)
        {
            // Validate parameters.
            if (expression == null) throw new ArgumentNullException(nameof(expression));

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
            return propertyInfo;
        }

        public static PropertyInfo GetPropertyInfo<T>(this T source, Expression<Func<T, object>> expression) =>
            expression.GetPropertyInfo();

        public static IEnumerable<PropertyInfo> GetPropertyInfos<T>(this T source,
            params Expression<Func<T, object>>[] expressions) =>
                source.GetPropertyInfos((IEnumerable<Expression<Func<T, object>>>) expressions);

        public static IEnumerable<PropertyInfo> GetPropertyInfos<T>(this T source,
            IEnumerable<Expression<Func<T, object>>> expressions)
        {
            // Validate parameters.
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (expressions == null) throw new ArgumentNullException(nameof(expressions));

            // Call the implementation.
            return expressions.GetPropertyInfos();
        }

        public static IEnumerable<PropertyInfo> GetPropertyInfos<TSource>(this IEnumerable<Expression<Func<TSource, object>>> expressions) =>
            expressions.Select(e => e.GetPropertyInfo());
    }
}
