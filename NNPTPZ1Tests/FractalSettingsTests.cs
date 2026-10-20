using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class FractalSettingsTests
    {
        // Overeni spravnych vstupnich argumentu
        [TestMethod]
        public void ValidArgumentsTest()
        {
            string[] args = {
                "200", "100", "-2", "2", "-2", "2", "fractal.png"
            };

            bool result = FractalSettings.TryParse(
                args, out FractalSettings settings, out string error);

            Assert.IsTrue(result);
            Assert.IsNotNull(settings);
            Assert.IsNull(error);
            Assert.AreEqual(200, settings.Width);
            Assert.AreEqual(100, settings.Height);
            Assert.AreEqual("fractal.png", settings.OutputPath);
        }

        // Overeni nespravneho poctu argumentu
        [TestMethod]
        public void InvalidArgumentCountTest()
        {
            string[] args = { "200", "100" };

            bool result = FractalSettings.TryParse(
                args, out FractalSettings settings, out string error);

            Assert.IsFalse(result);
            Assert.IsNull(settings);
            Assert.AreEqual("Nespravny pocet argumentu.", error);
        }

        // Obrazek nesmi mit nulovou nebo zapornou sirku
        [TestMethod]
        public void InvalidDimensionsTest()
        {
            string[] args = {
                "0", "100", "-2", "2", "-2", "2", "fractal.png"
            };

            bool result = FractalSettings.TryParse(
                args, out FractalSettings settings, out string error);

            Assert.IsFalse(result);
            Assert.AreEqual(
                "Sirka a vyska musi byt kladna cela cisla.", error);
        }

        // Souradnice musi byt platna cisla
        [TestMethod]
        public void InvalidCoordinatesTest()
        {
            string[] args = {
                "200", "100", "abc", "2", "-2", "2", "fractal.png"
            };

            bool result = FractalSettings.TryParse(
                args, out FractalSettings settings, out string error);

            Assert.IsFalse(result);
            Assert.AreEqual("Souradnice musi byt platna cisla.", error);
        }

        // Minimalni souradnice musi byt mensi nez maximalni
        [TestMethod]
        public void InvalidRangeTest()
        {
            string[] args = {
                "200", "100", "2", "-2", "-2", "2", "fractal.png"
            };

            bool result = FractalSettings.TryParse(
                args, out FractalSettings settings, out string error);

            Assert.IsFalse(result);
            Assert.AreEqual("Neplatny rozsah souradnic.", error);
        }

        // Vystupni soubor nesmi mit prazdny nazev
        [TestMethod]
        public void MissingOutputPathTest()
        {
            string[] args = {
                "200", "100", "-2", "2", "-2", "2", " "
            };

            bool result = FractalSettings.TryParse(
                args, out FractalSettings settings, out string error);

            Assert.IsFalse(result);
            Assert.AreEqual(
                "Chybi nazev vystupniho souboru.", error);
        }
    }
}
