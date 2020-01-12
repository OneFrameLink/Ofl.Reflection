using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Ofl.Reflection
{
    public static class ExpressionExtensions
    {
        public static PropertyInfo GetPropertyInfo<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            // Validate parameters.
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            // The member expression.
            MemberExpression? member = expression.Body as MemberExpression;

            // Used to generate the exception, if necessary.
            ArgumentException CreateExpressionNotPropertyException() =>
                new ArgumentException($"The expression parameter ({ nameof(expression) }) is not a property expression.");

            // If it's a convert, then get the expression in the convert.
            if (member == null && expression.Body.NodeType == ExpressionType.Convert)
                // Get the convert.
                member = (expression.Body as UnaryExpression)?.Operand as MemberExpression;

            // If not a member expression, throw an exception.
            if (member == null)
                throw CreateExpressionNotPropertyException();

            // Get the property info.
            // If it is null, throw an exception.
            if (!(member.Member is PropertyInfo propertyInfo))
                throw CreateExpressionNotPropertyException();

            // Return the property info.
            return propertyInfo;
        }

        public static PropertyInfo GetPropertyInfo<T, TProperty>(
#pragma warning disable IDE0060 // Remove unused parameter
            this T source,
#pragma warning restore IDE0060 // Remove unused parameter
            Expression<Func<T, TProperty>> expression
        ) => expression.GetPropertyInfo();

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
