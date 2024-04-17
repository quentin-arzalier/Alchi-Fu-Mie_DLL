using AFM_DLL;
namespace AFM_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        [TestCase(5, false)]
        [TestCase(6, false)]
        [TestCase(7, false)]
        public void Return3Test(int test, bool expectedResult)
        {
            Assert.That(expectedResult, Is.EqualTo(TestClass.Return3() == test));
        }
    }
}