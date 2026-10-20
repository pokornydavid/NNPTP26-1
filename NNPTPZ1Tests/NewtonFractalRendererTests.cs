using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1;
using NNPTPZ1.Mathematics;
using System.Drawing;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class NewtonFractalRendererTests
    {
        // Vytvori polynom 1 + x^3 pro testovani.
        private Polynomial CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();

            polynomial.Add(new ComplexNumber { Real = 1 });
            polynomial.Add(new ComplexNumber { Real = 0 });
            polynomial.Add(new ComplexNumber { Real = 0 });
            polynomial.Add(new ComplexNumber { Real = 1 });

            return polynomial;
        }

        // Overi vykresleni obrazku s ruznou sirkou a vyskou.
        [TestMethod]
        public void RenderNonSquareImageTest()
        {
            using (Bitmap bitmap = new Bitmap(60, 40))
            {
                NewtonFractalRenderer renderer =
                    new NewtonFractalRenderer();

                renderer.Render(bitmap, CreatePolynomial(),
                    -2, 2, -2, 2);

                Assert.AreEqual(60, bitmap.Width);
                Assert.AreEqual(40, bitmap.Height);

                // Overeni vykresleni krajnich pixelu.
                Assert.AreEqual(255, (int)bitmap.GetPixel(0, 0).A);
                Assert.AreEqual(255, (int)bitmap.GetPixel(59, 39).A);
            }
        }

        // Overi, ze fraktal obsahuje ruzne barvy.
        [TestMethod]
        public void RenderMultipleColorsTest()
        {
            using (Bitmap bitmap = new Bitmap(60, 40))
            {
                NewtonFractalRenderer renderer =
                    new NewtonFractalRenderer();

                renderer.Render(bitmap, CreatePolynomial(),
                    -2, 2, -2, 2);

                Color topLeft = bitmap.GetPixel(0, 0);
                Color topRight = bitmap.GetPixel(59, 0);
                Color bottomRight = bitmap.GetPixel(59, 39);

                bool differentColors =
                    topLeft.ToArgb() != topRight.ToArgb() ||
                    topRight.ToArgb() != bottomRight.ToArgb();

                Assert.IsTrue(differentColors);
            }
        }

        // Overi chovani pri nulove derivaci polynomu
        [TestMethod]
        public void RenderConstantPolynomialTest()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber { Real = 1 });

            using (Bitmap bitmap = new Bitmap(10, 10))
            {
                NewtonFractalRenderer renderer =
                    new NewtonFractalRenderer();

                renderer.Render(bitmap, polynomial, -2, 2, -2, 2);

                Assert.AreEqual(
                    Color.Black.ToArgb(),
                    bitmap.GetPixel(0, 0).ToArgb());
            }
        }
    }
}
