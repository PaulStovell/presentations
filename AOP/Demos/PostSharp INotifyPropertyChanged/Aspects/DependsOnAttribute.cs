using System;

namespace Demo.Aspects
{
    /// <summary>
    /// Indicates properties that this property depends on. Changes to the properies listed here will cause a PropertyChanged event for this property to also be raised.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = false), Serializable]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependsOnAttribute"/> class.
        /// </summary>
        /// <param name="propertyNames">The property names.</param>
        public DependsOnAttribute(params string[] propertyNames)
        {
            DependencyPropertyNames = propertyNames;
        }

        /// <summary>
        /// Gets or sets the dependency property names.
        /// </summary>
        /// <value>The dependency property names.</value>
        public string[] DependencyPropertyNames { get; set; }
    }
}