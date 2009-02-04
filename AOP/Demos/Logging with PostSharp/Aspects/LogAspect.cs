using System;
using System.Reflection;
using PostSharp.Laos;
using System.Diagnostics;
using System.Text;

namespace Demo3.Aspects
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Assembly)]
    [Serializable]
    public class LogAspect : OnMethodBoundaryAspect
    {
        public override void CompileTimeInitialize(MethodBase method)
        {
            base.CompileTimeInitialize(method);

            // We can put code here to throw an exception if the method doesn't 
            // meet our expectations
            if (method.Name == "Foobar")
            {
                throw new Exception(string.Format("Class '{0}' must not have a method named 'Foobar'", method.DeclaringType.FullName));
            }
        }

        public override void OnEntry(MethodExecutionEventArgs eventArgs)
        {
            base.OnEntry(eventArgs);

            var logMessage = new StringBuilder()
                .Append("Begin ")
                .Append(eventArgs.Method.Name)
                .Append("(");

            var arguments = eventArgs.GetReadOnlyArgumentArray();
            for (var i = 0; i < arguments.Length; i++)
            {
                logMessage.Append(eventArgs.Method.GetParameters()[i].Name);
                logMessage.Append(" = ");
                logMessage.Append(arguments[i]);
                if (i < arguments.Length - 1)
                {
                    logMessage.Append(", ");
                }
            }
            logMessage.Append(")");
            Trace.WriteLine(logMessage);
            Trace.Indent();
        }

        public override void OnExit(MethodExecutionEventArgs eventArgs)
        {
            base.OnExit(eventArgs);

            Trace.Unindent();
            Trace.WriteLine("End " + eventArgs.Method.Name);
        }

        public override void OnException(MethodExecutionEventArgs eventArgs)
        {
            base.OnException(eventArgs);

            Trace.TraceError(eventArgs.Exception.ToString());
        }
    }
}
