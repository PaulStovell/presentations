using Demo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoggingTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var searcher = new ContactSearchService();
            searcher.FindByName("Paul");
        }
    }
}
