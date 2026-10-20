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
            ComplexNumber firstNumber = new ComplexNumber { Real = 2, Imaginary = 3 };
            ComplexNumber secondNumber = new ComplexNumber { Real = -1, Imaginary = 5 };

            ComplexNumber sum = firstNumber.Add(secondNumber);
            ComplexNumber difference = firstNumber.Subtract(secondNumber);

            Assert.AreEqual(1.0, sum.Real, 0.0001);
            Assert.AreEqual(8.0, sum.Imaginary, 0.0001);

            Assert.AreEqual(3.0, difference.Real, 0.0001);
            Assert.AreEqual(-2.0, difference.Imaginary, 0.0001);
        }

        // Overeni nasobeni komplexnich cisel
        [TestMethod]
        public void MultiplyTest()
        {
            ComplexNumber firstNumber = new ComplexNumber { Real = 2, Imaginary = 3 };
            ComplexNumber secondNumber = new ComplexNumber { Real = 1, Imaginary = -2 };

            ComplexNumber result = firstNumber.Multiply(secondNumber);

            Assert.AreEqual(8.0, result.Real, 0.0001);
            Assert.AreEqual(-1.0, result.Imaginary, 0.0001);
        }

        // Overeni absolutni hodnoty komplexniho cisla
        [TestMethod]
        public void MagnitudeTest()
        {
            ComplexNumber number = new ComplexNumber { Real = 3, Imaginary = 4 };

            Assert.AreEqual(5.0, number.GetMagnitude(), 0.0001);
            Assert.AreEqual(5.0, number.GetAbS(), 0.0001);
        }

        // Overeni vypoctu uhlu ve stupnich
        [TestMethod]
        public void AngleTest()
        {
            ComplexNumber number = new ComplexNumber { Real = 1, Imaginary = 1 };

            Assert.AreEqual(45.0,
                number.GetAngleInDegrees(), 0.0001);
        }

        // Stejna cisla musi mit stejny hash kod
        [TestMethod]
        public void EqualsAndHashCodeTest()
        {
            ComplexNumber firstNumber = new ComplexNumber { Real = 2, Imaginary = 3 };
            ComplexNumber secondNumber = new ComplexNumber { Real = 2, Imaginary = 3 };

            Assert.AreEqual(firstNumber, secondNumber);
            Assert.AreEqual(firstNumber.GetHashCode(), secondNumber.GetHashCode());
        }

        // Overeni vypoctu hodnoty polynomu
        [TestMethod]
        public void PolynomialEvaluationTest()
        {
            Polynomial polynomial = new Polynomial();

            polynomial.Add(new ComplexNumber { Real = 1 });
            polynomial.Add(new ComplexNumber { Real = 2 });
            polynomial.Add(new ComplexNumber { Real = 3 });

            // f(x) = 1 + 2x + 3x^2
            // f(2) = 17
            ComplexNumber result = polynomial.Eval(2);

            Assert.AreEqual(17.0, result.Real, 0.0001);
        }

        // Overeni derivace polynomu
        [TestMethod]
        public void PolynomialDerivativeTest()
        {
            Polynomial polynomial = new Polynomial();

            polynomial.Add(new ComplexNumber { Real = 1 });
            polynomial.Add(new ComplexNumber { Real = 2 });
            polynomial.Add(new ComplexNumber { Real = 3 });

            // Derivace 1 + 2x + 3x^2 je 2 + 6x
            Polynomial derivative = polynomial.Derive();

            Assert.AreEqual(2, derivative.Coefficients.Count);
            Assert.AreEqual(2.0, derivative.Coefficients[0].Real, 0.0001);
            Assert.AreEqual(6.0, derivative.Coefficients[1].Real, 0.0001);
        }

        // Prazdny polynom musi vratit nulu
        [TestMethod]
        public void EmptyPolynomialTest()
        {
            Polynomial polynomial = new Polynomial();

            ComplexNumber result = polynomial.Eval(2);

            Assert.AreEqual(0.0, result.Real, 0.0001);
            Assert.AreEqual(0.0, result.Imaginary, 0.0001);
        }
    }
}
