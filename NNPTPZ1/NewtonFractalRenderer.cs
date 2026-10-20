using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal class NewtonFractalRenderer
    {
        private const int MaxIterations = 30;
        private const double ZeroReplacement = 0.0001;
        private const double ConvergenceThreshold = 0.0001;
        private const double RootTolerance = 0.01;
        private const double DerivativeThreshold = 0.000000000001;

        private readonly Color[] _colors =
        {
            Color.Red, Color.Blue, Color.Green,
            Color.Yellow, Color.Orange, Color.Fuchsia,
            Color.Gold, Color.Cyan, Color.Magenta
        };

        public void Render(Bitmap bitmap, Polynomial polynomial,
            double xMin, double xMax, double yMin, double yMax)
        {
            double xStep = (xMax - xMin) / bitmap.Width;
            double yStep = (yMax - yMin) / bitmap.Height;

            Polynomial derivative = polynomial.Derive();
            List<ComplexNumber> roots = new List<ComplexNumber>();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    ComplexNumber point = CreatePoint(x, y, xMin, yMin, xStep, yStep);

                    int iterations;
                    ComplexNumber root = FindRoot(polynomial, derivative, point, out iterations);

                    // Pokud se koren nepodarilo najit, pixel bude cerny
                    if (root == null)
                    {
                        bitmap.SetPixel(x, y, Color.Black);
                        continue;
                    }

                    int rootIndex = GetRootIndex(roots, root);
                    Color pixelColor = GetPixelColor(rootIndex, iterations);

                    bitmap.SetPixel(x, y, pixelColor);
                }
            }
        }

        /// <summary>
        /// Vytvori komplexni cislo podle souradnic pixelu.
        /// </summary>
        private ComplexNumber CreatePoint(int x, int y, double xMin, double yMin,
            double xStep, double yStep)
        {
            ComplexNumber point = new ComplexNumber
            {
                Real = xMin + x * xStep,
                Imaginary = yMin + y * yStep
            };

            if (point.Real == 0)
            {
                point.Real = ZeroReplacement;
            }

            if (point.Imaginary == 0)
            {
                point.Imaginary = ZeroReplacement;
            }

            return point;
        }

        /// <summary>
        /// Pomoci Newtonovy metody hleda priblizny koren polynomu.
        /// Zaroven vraci pocet iteraci.
        /// Pokud vypocet selze, vrati null.
        /// </summary>
        private ComplexNumber FindRoot(Polynomial polynomial, Polynomial derivative,
            ComplexNumber point, out int iterations)
        {
            iterations = 0;

            for (int i = 0; i < MaxIterations; i++)
            {
                ComplexNumber derivativeValue = derivative.Eval(point);

                // Kontrola derivace pred delenim
                if (!IsFinite(derivativeValue) ||
                    derivativeValue.GetMagnitude() < DerivativeThreshold)
                {
                    return null;
                }

                ComplexNumber difference = polynomial.Eval(point)
                    .Divide(derivativeValue);

                if (!IsFinite(difference))
                {
                    return null;
                }

                point = point.Subtract(difference);
                iterations++;

                if (!IsFinite(point))
                {
                    return null;
                }

                double differenceSize =
                    difference.Real * difference.Real +
                    difference.Imaginary * difference.Imaginary;

                if (differenceSize < ConvergenceThreshold)
                {
                    return point;
                }
            }

            // Koren nebyl nalezen v povolenem poctu iteraci
            return null;
        }

        /// <summary>
        /// Overi, ze komplexni cislo obsahuje platne hodnoty.
        /// </summary>
        private bool IsFinite(ComplexNumber number)
        {
            return !double.IsNaN(number.Real) &&
                   !double.IsInfinity(number.Real) &&
                   !double.IsNaN(number.Imaginary) &&
                   !double.IsInfinity(number.Imaginary);
        }

        /// <summary>
        /// Zjisti, jestli uz je koren znamy.
        /// Kdyz neni, prida ho do seznamu.
        /// </summary>
        private int GetRootIndex(List<ComplexNumber> roots, ComplexNumber root)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                double distance =
                    Math.Pow(root.Real - roots[i].Real, 2) +
                    Math.Pow(root.Imaginary - roots[i].Imaginary, 2);

                if (distance <= RootTolerance)
                {
                    return i;
                }
            }

            roots.Add(root);
            return roots.Count - 1;
        }

        /// <summary>
        /// Vrati barvu pixelu podle cisla korene a poctu iteraci.
        /// </summary>
        private Color GetPixelColor(int rootIndex, int iterations)
        {
            Color baseColor = _colors[rootIndex % _colors.Length];

            int red = Math.Max(0, baseColor.R - iterations * 2);
            int green = Math.Max(0, baseColor.G - iterations * 2);
            int blue = Math.Max(0, baseColor.B - iterations * 2);

            return Color.FromArgb(red, green, blue);
        }
    }
}
