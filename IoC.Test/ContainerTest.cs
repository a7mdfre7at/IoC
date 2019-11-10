using IoC_FromScratch;
using NUnit.Framework;

namespace IoC.Test
{
    public class ContainerTestBase
    {
        protected Container Container;

        [SetUp]
        public void BeforeEach()
        {
            Container = new Container();
        }

        [TearDown]
        public void AfterEach()
        {
            Container = null;
        }
    }

    [TestFixture]
    public class Container_GetInstance : ContainerTestBase
    {
        [Test]
        public void GetInstanceWithParams()
        {
            var subject = Container.GetInstance(typeof(A));
            Assert.AreSame(subject.GetType(), typeof(A));
        }
    }




    class A { }
}