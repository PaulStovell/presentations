using System;
using System.Diagnostics;
using System.Reflection;
using PostSharp.Laos;

namespace Demo.Aspects
{
    /// <summary>
    /// Must be applied to a method if the <see>NotNullAttribute</see> attribute is on a parameter
    /// This is temporary until PostSharp gets a OnParameterBoundaryAspect.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = true), Serializable]
    public class NotifyChangeAttribute : OnMethodBoundaryAspect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyChangeAttribute"/> class.
        /// </summary>
        public NotifyChangeAttribute()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyChangeAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public NotifyChangeAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        /// <summary>
        /// Gets or sets the name of the property which will be raised when this attribute is applied.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Evaluates the aspect at compile time and validates that the aspect has been applied correctly.
        /// </summary>
        /// <param name="method">The method the attribute has been applied to.</param>
        public override void CompileTimeInitialize(MethodBase method)
        {
            base.CompileTimeInitialize(method);

            if (!typeof(ICanRaisePropertyChangedEvents).IsAssignableFrom(method.DeclaringType))
            {
                throw new Exception(string.Format("Type '{0}' does not implement ICanRaisePropertyChangedEvents, but the property '{1}' has the NotifyChange attribute applied. Ensure the type '{0}' implements ICanRaisePropertyChangedEvents (preferably explicitly).", method.DeclaringType, method.Name));
            }

            if (PropertyName == null)
            {
                PropertyName = method.Name.Replace("set_", "");
            }
        }

        /// <summary>
        /// Executed at runtime at the end of the property setter to raise the PropertyChanged event.
        /// </summary>
        /// <param name="eventArgs">The <see cref="PostSharp.Laos.MethodExecutionEventArgs"/> instance containing the event data.</param>
        [DebuggerNonUserCode]
        public override void OnExit(MethodExecutionEventArgs eventArgs)
        {
            var raiser = eventArgs.Instance as ICanRaisePropertyChangedEvents;
            if (raiser != null) raiser.RaisePropertyChangedEvent(PropertyName);
        }
    }
}