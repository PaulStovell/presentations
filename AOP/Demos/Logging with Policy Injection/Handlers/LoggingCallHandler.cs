using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection.Configuration;
using Microsoft.Practices.ObjectBuilder2;

namespace Demo2.Handlers
{
    public class LogCallHandlerAssembler : IAssembler<ICallHandler, CallHandlerData>
    {
        public ICallHandler Assemble(IBuilderContext context, CallHandlerData objectConfiguration, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            var callHandler = new LoggingCallHandler();
            
            return callHandler;
        }
    }

    [Assembler(typeof(LogCallHandlerAssembler))]
    public class LoggingCallHandlerData : CallHandlerData
    {
        public LoggingCallHandlerData()
        {
        }

        public LoggingCallHandlerData(string handlerName)
        {
        }
        public LoggingCallHandlerData(string handlerName, int handlerOrder)
        {
        }
    }

    [ConfigurationElementType(typeof(LoggingCallHandlerData))]
    public class LoggingCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var logMessage = new StringBuilder()
                .Append("Begin ")
                .Append(input.MethodBase.Name)
                .Append("(");

            var arguments = input.Arguments;
            for (var i = 0; i < arguments.Count; i++)
            {
                logMessage.Append(input.MethodBase.GetParameters()[i].Name);
                logMessage.Append(" = ");
                logMessage.Append(arguments[i]);
                if (i < arguments.Count - 1)
                {
                    logMessage.Append(", ");
                }
            }
            logMessage.Append(")");


            Trace.WriteLine(logMessage);
            Trace.Indent();
            try
            {
                var v = getNext();
                return v(input, getNext);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                throw;
            }
            finally
            {
                Trace.Unindent();
                Trace.WriteLine(string.Format("End {0}", input.MethodBase.Name));
            }
        }

        public int Order
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }
    }
}