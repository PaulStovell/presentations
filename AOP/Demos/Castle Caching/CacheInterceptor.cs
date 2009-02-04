using System;
using System.Collections.Generic;
using Castle.Core.Interceptor;

namespace Castle_Caching
{
    public class CacheInterceptor : IInterceptor
    {
        private class CacheEntry
        {
            public object ReturnValue { get; set; }
            public DateTime Expiry { get; set; }
        }

        private readonly Dictionary<string, CacheEntry> _cache = new Dictionary<string, CacheEntry>();

        public void Intercept(IInvocation invocation)
        {
            var cacheKey = string.Format("{0}||{1}", invocation.Method.Name, invocation.Method.DeclaringType.Name);

            if (_cache.ContainsKey(cacheKey))
            {
                var entry = _cache[cacheKey];
                if (DateTime.Now < entry.Expiry)
                {
                    invocation.ReturnValue = entry.ReturnValue;
                    return;
                }
            }

            invocation.Proceed();
         
            // Can we cache this entry?
            if (invocation.MethodInvocationTarget.GetCustomAttributes(typeof (CacheAttribute), true).Length == 0) 
                return;
            
            var newEntry = new CacheEntry()
                               {
                                   Expiry = DateTime.Now.AddSeconds(5), 
                                   ReturnValue = invocation.ReturnValue
                               };
            _cache[cacheKey] = newEntry;
        }
    }
}