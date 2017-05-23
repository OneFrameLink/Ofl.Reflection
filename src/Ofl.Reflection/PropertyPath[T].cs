using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Ofl.Reflection
{
    public class PropertyPath<T> : PropertyPath
    {
        #region Constructors

        internal PropertyPath() : base(null)
        { }

        internal PropertyPath(PropertyPath root) : base(root)
        { }

        #endregion

        #region Helpers.

        public PropertyPath<TResult> ThenEnumerable<TResult>(Expression<Func<T, IEnumerable<TResult>>> expression)
        {
            // Validate parameters.
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            // Get the member info.
            var propertyInfo = (expression.Body as MemberExpression)?.Member as PropertyInfo;

            // If null, throw.
            if (propertyInfo == null)
                throw new InvalidOperationException($"The {nameof(expression)} parameter must be an expression backed by a PropertyInfo.");

            // Push.
            Append(propertyInfo);

            // Return the new expression.
            return new PropertyPath<TResult>(Root);
        }

        public PropertyPath<TResult> Then<TResult>(Expression<Func<T, TResult>> expression)
        {
            // Validate parameters.
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            // Get the member info.
            var propertyInfo = (expression.Body as MemberExpression)?.Member as PropertyInfo;

            // If null, throw.
            if (propertyInfo == null)
                throw new InvalidOperationException($"The {nameof(expression)} parameter must be an expression backed by a PropertyInfo.");

            // Push.
            Append(propertyInfo);

            // Return the new expression.
            return new PropertyPath<TResult>(Root);
        }

        #endregion
    }
}
