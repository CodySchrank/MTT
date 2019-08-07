using NUnit.Framework;
using MTTRunner;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            var dirs = new string[] {"./ts", "./csharp"};

            MTTRunner.Program.StartService(dirs);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}