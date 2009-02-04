using System;
using System.Threading;
using Castle.Windsor;

namespace Castle_Caching
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.AddComponent<CacheInterceptor>();
            container.AddComponent<IContentGenerator, ContentGenerator>();

            var generator = container.Resolve<IContentGenerator>();

            while(true)
            {
                var result = generator.GenerateContent();
                Console.WriteLine(result);

                Thread.Sleep(1000);
            }
        }
    }
}
