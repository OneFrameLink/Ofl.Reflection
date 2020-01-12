using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Ofl.Reflection
{
    public abstract class PropertyPath
    {
        #region Constructor

        private PropertyPath() : this(null)
        { }

        internal PropertyPath(PropertyPath? root)
        {
            // If the root is null, assign the path.
            if (root == null)
            {
                // Assign path.
                _path = new Queue<PropertyInfo>();

                // Set the root.
                Root = this;
            }
            else
            {
                // Set the path to null;
                // calls to the path will always be
                // not populated; either this is the
                // root and it is populated 👆 or
                // it is not the root, and
                // the call is delegated to the root.
                _path = null!;

                // Root is root.
                Root = root;
            }

            // Root must not be null.
            Debug.Assert(Root != null);
        }

        #endregion

        #region Instance, read-only state.

        private readonly Queue<PropertyInfo> _path;

        public PropertyPath Root { get; }

        public IEnumerable<PropertyInfo> Path => Root._path;

        #endregion

        #region Action methods.

        protected void Append(PropertyInfo property)
        {
            // Validate parameters.
            if (property == null) throw new ArgumentNullException(nameof(property));

            // Push.
            Root._path.Enqueue(property);
        }

        public static PropertyPath<T> Of<T>()
        {
            // Create a new instance.
            return new PropertyPath<T>();
        }

        #endregion
    }
}
