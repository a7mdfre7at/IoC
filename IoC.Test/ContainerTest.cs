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
        public void GetInstanceWithNoParams()
        {
            var subject = Container.GetInstance(typeof(A));
            Assert.AreSame(subject.GetType(), typeof(A));
        }

        [Test]
        public void GetInstanceWithParams()
        {
            var subject = (B)Container.GetInstance(typeof(B));
            Assert.AreSame(subject.GetType(), typeof(B));
            Assert.NotNull(subject.A);
        }
    }




    class A { }

    class B
    {
        public A A { get; }

        public B()
        {

        }

        public B(A a)
        {
            A = a;
        } 
    }
}