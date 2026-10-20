
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class MathRefactoringTests
    {
        // Overeni scitani a odcitani komplexnich cisel
        [TestMethod]
        public void AddAndSubtractTest()
        {
            Cplx a = new Cplx { Re = 2, Imaginari = 3 };
            Cplx b = new Cplx { Re = -1, Imaginari = 5 };

            Cplx sum = a.Add(b);
            Cplx difference = a.Subtract(b);

            Assert.AreEqual(1.0, sum.Re, 0.0001);
            Assert.AreEqual(8.0, sum.Imaginari, 0.0001);

            Assert.AreEqual(3.0, difference.Re, 0.0001);
            Assert.AreEqual(-2.0, difference.Imaginari, 0.0001);
        }

        // Overeni nasobeni komplexnich cisel
        [TestMethod]
        public void MultiplyTest()
        {
            Cplx a = new Cplx { Re = 2, Imaginari = 3 };
            Cplx b = new Cplx { Re = 1, Imaginari = -2 };

            Cplx result = a.Multiply(b);

            Assert.AreEqual(8.0, result.Re, 0.0001);
            Assert.AreEqual(-1.0, result.Imaginari, 0.0001);
        }

        // Overeni absolutni hodnoty komplexniho cisla
        [TestMethod]
        public void MagnitudeTest()
        {
            Cplx number = new Cplx { Re = 3, Imaginari = 4 };

            Assert.AreEqual(5.0, number.GetMagnitude(), 0.0001);
            Assert.AreEqual(5.0, number.GetAbS(), 0.0001);
        }

        // Overeni vypoctu uhlu ve stupnich
        [TestMethod]
        public void AngleTest()
        {
            Cplx number = new Cplx { Re = 1, Imaginari = 1 };

            Assert.AreEqual(45.0,
                number.GetAngleInDegrees(), 0.0001);
        }

        // Stejna cisla musi mit stejny hash kod
        [TestMethod]
        public void EqualsAndHashCodeTest()
        {
            Cplx a = new Cplx { Re = 2, Imaginari = 3 };
            Cplx b = new Cplx { Re = 2, Imaginari = 3 };

            Assert.AreEqual(a, b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        // Overeni vypoctu hodnoty polynomu
        [TestMethod]
        public void PolynomialEvaluationTest()
        {
            Poly polynomial = new Poly();

            polynomial.Add(new Cplx { Re = 1 });
            polynomial.Add(new Cplx { Re = 2 });
            polynomial.Add(new Cplx { Re = 3 });

            // f(x) = 1 + 2x + 3x^2
            // f(2) = 17
            Cplx result = polynomial.Eval(2);

            Assert.AreEqual(17.0, result.Re, 0.0001);
        }

        // Overeni derivace polynomu
        [TestMethod]
        public void PolynomialDerivativeTest()
        {
            Poly polynomial = new Poly();

            polynomial.Add(new Cplx { Re = 1 });
            polynomial.Add(new Cplx { Re = 2 });
            polynomial.Add(new Cplx { Re = 3 });

            // Derivace 1 + 2x + 3x^2 je 2 + 6x
            Poly derivative = polynomial.Derive();

            Assert.AreEqual(2, derivative.Coe.Count);
            Assert.AreEqual(2.0, derivative.Coe[0].Re, 0.0001);
            Assert.AreEqual(6.0, derivative.Coe[1].Re, 0.0001);
        }

        // Prazdny polynom musi vratit nulu
        [TestMethod]
        public void EmptyPolynomialTest()
        {
            Poly polynomial = new Poly();

            Cplx result = polynomial.Eval(2);

            Assert.AreEqual(0.0, result.Re, 0.0001);
            Assert.AreEqual(0.0, result.Imaginari, 0.0001);
        }
    }
}
