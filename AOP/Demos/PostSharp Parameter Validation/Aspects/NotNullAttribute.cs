using System;
using System.Diagnostics;
using System.Reflection;

namespace Demo.Aspects
{
    /// <summary>
    /// Throws an error for all parameters it is applied to if the parameter is null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class NotNullAttribute : AbstractParameterValidationAttribute
    {
        [DebuggerNonUserCode]
        public override void ValidateParameter(ParameterInfo parameter, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameter.Name, string.Format("The parameter '{0}' passed to method '{1}' is required to be non-null.", parameter.Name, parameter.Member.Name));
            }
        }
    }
}