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
        public void CreatesAnInstanceWithNoParams()
        {
            var subject = (A)Container.GetInstance(typeof(A));
            Assert.AreSame(subject.GetType(), typeof(A));
        }

        [Test]
        public void CreatesAnInstanceWithParams()
        {
            var subject = (B)Container.GetInstance(typeof(B));
            Assert.AreSame(subject.A.GetType(), typeof(A));
        }

        [Test]
        public void ItAllowsAParameterlessConstructor()
        {
            var subject = (C)Container.GetInstance(typeof(C));
            Assert.AreEqual(true, subject.Invoked);
        }

        [Test]
        public void ItAllowsGenericInitialization()
        {
            var subject = Container.GetInstance<A>();
            Assert.AreSame(subject.GetType(), typeof(A));
        }

        [Test]
        public void ItAllowsFunctinalnitialization()
        {
            Container.Register<D>(() => new D(5));
            var subject = Container.GetInstance<D>();
            Assert.AreEqual(5, subject.X);
        }

        class A
        { }

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

        class C
        {
            public bool? Invoked { get; set; }

            public C()
            {
                Invoked = true;
            }
        }


        class D
        {
            public int X { get; set; }

            public D(int x)
            {
                X = x;
            }
        }
    }

    [TestFixture]
    public class Container_Register : ContainerTestBase
    {
        [Test]
        public void RegisterATypeFromAnInterface()
        {
            Container.Register<IMaterial, Plastic>();
            var subject = Container.GetInstance<IMaterial>();
            Assert.AreSame(subject.GetType(), typeof(Plastic));
        }

        [Test]
        public void InitializeObjectWithDependencies()
        {
            Container.Register<IMaterial, Toy>();
            var subject = (Toy)Container.GetInstance<IMaterial>();
            Assert.AreSame(subject.Material.GetType(), typeof(Plastic));
        }

        interface IMaterial
        {
            int Weight { get; }
        }

        class Plastic : IMaterial
        {
            public int Weight => 42;
        }

        class Metal : IMaterial
        {
            public int Weight => 84;
        }

        class Toy : IMaterial
        {
            public int Weight => 100;
            public Plastic Material { get; } = null;

            public Toy(Plastic material)
            {
                Material = material;
            }
        }
    }

    [TestFixture]
    public class Container_RegisterSingleton : ContainerTestBase
    {
        [Test]
        public void ItReturnsASingleInstance()
        {
            var pet = new Pet();
            Container.RegisterSingleton(pet);
            var subject = Container.GetInstance<Pet>();
            Assert.IsTrue(pet == subject);
        }

        class Pet
        { }
    }
}