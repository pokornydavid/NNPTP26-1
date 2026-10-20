using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNPTPZ1;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber firstNumber = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber secondNumber = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 2
            };

            ComplexNumber actual = firstNumber.Add(secondNumber);
            ComplexNumber expected = new ComplexNumber()
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(expected, actual);

            var expectedText = "(10 + 20i)";
            var actualText = firstNumber.ToString();
            Assert.AreEqual(expectedText, actualText);
            expectedText = "(1 + 2i)";
            actualText = secondNumber.ToString();
            Assert.AreEqual(expectedText, actualText);

            firstNumber = new ComplexNumber()
            {
                Real = 1,
                Imaginary = -1
            };
            secondNumber = new ComplexNumber() { Real = 0, Imaginary = 0 };
            expected = new ComplexNumber() { Real = 1, Imaginary = -1 };
            actual = firstNumber.Add(secondNumber);
            Assert.AreEqual(expected, actual);

            expectedText = "(1 + -1i)";
            actualText = firstNumber.ToString();
            Assert.AreEqual(expectedText, actualText);

            expectedText = "(0 + 0i)";
            actualText = secondNumber.ToString();
            Assert.AreEqual(expectedText, actualText);
        }

        [TestMethod()]
        public void PolynomialEvaluationTest()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 0, Imaginary = 0 });
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });
            ComplexNumber result = polynomial.Eval(new ComplexNumber() { Real = 0, Imaginary = 0 });
            var expected = new ComplexNumber() { Real = 1, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Eval(new ComplexNumber() { Real = 1, Imaginary = 0 });
            expected = new ComplexNumber() { Real = 2, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Eval(new ComplexNumber() { Real = 2, Imaginary = 0 });
            expected = new ComplexNumber() { Real = 5.0000000000, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            var actualText = polynomial.ToString();
            var expectedText = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expectedText, actualText);
        }
    }
}
