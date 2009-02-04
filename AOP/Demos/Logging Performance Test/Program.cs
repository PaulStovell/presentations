using System;
using Castle.Windsor;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using System.Diagnostics;

namespace Logging_Performance_Test
{
    class Program
    {
        private const int _testRuns = 400;

        static void Main(string[] args)
        {
            var normal = new Demo0.ContactSearchService();
            var postSharp = new Demo1.ContactSearchService();
            var entlib = PolicyInjection.Wrap<Demo2.IContactSearchService>(new Demo2.ContactSearchService());
            var castle = new WindsorContainer().AddComponent<Demo1.Interceptors.LogInterceptor>().AddComponent<Demo1.IContactSearchService, Demo1.ContactSearchService>().Resolve<Demo1.IContactSearchService>();

            Test("Warmup   ", () => normal.FindByName("Paul"));
            Test("Normal   ", () => normal.FindByName("Paul"));
            Test("Castle   ", () => castle.FindByName("Paul"));
            Test("PostSharp", () => postSharp.FindByName("Paul"));
            Test("Ent Lib  ", () => entlib.FindByName("Paul"));
            
            //Test("Warmup   ", () =>
            //                      {
            //                          var normal = new Demo0.ContactSearchService();
            //                          normal.FindByName("Paul");
            //                      });

            //Test("Normal   ", () =>
            //                      {
            //                          var normal = new Demo0.ContactSearchService();
            //                          normal.FindByName("Paul");
            //                      });
            //Test("Castle   ", () =>
            //                      {
            //                          var castle = new WindsorContainer().AddComponent<Demo1.Interceptors.LogInterceptor>().AddComponent<Demo1.IContactSearchService, Demo1.ContactSearchService>().Resolve<Demo1.IContactSearchService>();
            //                          castle.FindByName("Paul");
            //                      });
            //Test("PostSharp", () =>
            //                      {
            //                          var postSharp = new Demo1.ContactSearchService();
            //                          postSharp.FindByName("Paul");
            //                      });
            //Test("Ent Lib  ", () =>
            //                      {
            //                          var entlib = PolicyInjection.Wrap<Demo2.IContactSearchService>(new Demo2.ContactSearchService());
            //                          entlib.FindByName("Paul");
            //                      });

            Console.ReadKey();
        }

        static void Test(string name, Action callback)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < _testRuns; i++)
            {
                callback();
            }
            stopwatch.Stop();
            Console.WriteLine(name + " " + stopwatch.ElapsedMilliseconds);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
