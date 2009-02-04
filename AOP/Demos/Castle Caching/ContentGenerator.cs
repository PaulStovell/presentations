using System;
using Castle.Core;

namespace Castle_Caching
{
    [Interceptor(typeof(CacheInterceptor))]
    public class ContentGenerator : IContentGenerator
    {
        [Cache]
        public string GenerateContent()
        {
            return Guid.NewGuid().ToString();
        }
    }
}