using System.Collections.Generic;
using System.Linq;
using Castle.Core.Interceptor;
using System;
using System.Threading;

namespace Demo.Interceptors
{
    public class CircuitBreakerInterceptor : IInterceptor
    {
        private readonly Dictionary<string, Circuit> _circuits = new Dictionary<string, Circuit>();

        public void Intercept(IInvocation invocation)
        {
            var circuitName = invocation.Method.DeclaringType.Name;

            // Setup a new circuit if needed
            if (!_circuits.ContainsKey(circuitName))
            {
                _circuits.Add(circuitName, new Circuit());
            }

            var circuit = _circuits[circuitName];

            // If the circuit breaker has been tripped, fail fast 
            if (circuit.IsOffline)
            {
                throw new Exception("This service is misbehaving and has been taken offline");
            }

            // The circuit breaker hasn't been tripped - allow it
            try
            {
                var autoReset = new AutoResetEvent(false);
                var exception = null as Exception;
                ThreadPool.QueueUserWorkItem(
                    thread =>
                        {
                            try
                            {

                                invocation.Proceed();
                                autoReset.Set();
                            }
                            catch (Exception innerException)
                            {
                                exception = innerException;
                                autoReset.Set();
                            }
                        });
                var success = autoReset.WaitOne(3000);
                if (!success)
                {
                    // It timed out
                    circuit.RecordTimeout();
                    throw new TimeoutException("The call timed out");
                }
                if (exception != null)
                {
                    throw exception;
                }
            }
            catch (TimeoutException) 
            {
                throw;
            }
            catch (Exception ex)
            {
                circuit.RecordException(ex);
                throw;
            }
        }
    }

    public class Circuit
    {
        private readonly List<DateTime> _exceptions = new List<DateTime>();
        private readonly List<DateTime> _timeouts = new List<DateTime>();
        private DateTime _offlineUntil;

        public void RecordException(Exception ex)
        {
            _exceptions.Add(DateTime.Now);
        }

        public void RecordTimeout()
        {
            _timeouts.Add(DateTime.Now);
        }

        public bool IsOffline
        {
            get
            {
                // More than 3 exceptions within 5 seconds
                if (_exceptions.Where(dt => DateTime.Now.Subtract(dt).TotalMilliseconds <= 10000).Count() >= 3)
                {
                    _offlineUntil = DateTime.Now.AddSeconds(10);
                }
                // More than 3 timeouts within 5 seconds
                if (_timeouts.Where(dt => DateTime.Now.Subtract(dt).TotalMilliseconds <= 10000).Count() >= 3)
                {
                    _offlineUntil = DateTime.Now.AddSeconds(10);
                }

                // Has the offline flag expired?
                return DateTime.Now < _offlineUntil;
            }
        }
    }
}