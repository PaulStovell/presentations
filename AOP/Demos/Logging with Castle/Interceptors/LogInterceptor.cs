using System;
using System.Diagnostics;
using System.Text;
using Castle.Core.Interceptor;

namespace Demo1.Interceptors
{
    public class LogInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var logMessage = new StringBuilder()
                .Append("Begin ")
                .Append(invocation.Method.Name)
                .Append("(");

            var arguments = invocation.Arguments;
            for (var i = 0; i < arguments.Length; i++)
            {
                logMessage.Append(invocation.Method.GetParameters()[i].Name);
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
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                throw;
            }
            finally
            {
                Trace.Unindent();
                Trace.WriteLine(string.Format("End {0}", invocation.Method.Name));
            }
        }
    }
}