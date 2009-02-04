using System;
using System.Diagnostics;
using System.Reflection;

namespace Demo.Aspects
{
    /// <summary>
    /// The parameter must be less than the given value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class LessThanOrEqualAttribute : AbstractParameterValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanOrEqualAttribute"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public LessThanOrEqualAttribute(object value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; set; }

        /// <summary>
        /// Validates the parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="value">The value.</param>
        [DebuggerNonUserCode]
        public override void ValidateParameter(ParameterInfo parameter, object value)
        {
            var parameterValue = Convert.ToDecimal(value);
            var baselineValue = Convert.ToDecimal(Value);
            if (!(parameterValue <= baselineValue))
            {
                throw new ArgumentException(string.Format("The parameter '{0}' to method '{1}' is required to be less than or equal to '{2}', but was instead '{3}'.", parameter.Name, parameter.Member.Name, Value, value), parameter.Name);
            }
        }
    }
}