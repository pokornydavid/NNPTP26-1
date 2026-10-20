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

        private readonly Color[] colors =
        {
            Color.Red, Color.Blue, Color.Green,
            Color.Yellow, Color.Orange, Color.Fuchsia,
            Color.Gold, Color.Cyan, Color.Magenta
        };

        public void Render(Bitmap bitmap, Poly polynomial,
            double xMin, double xMax, double yMin, double yMax)
        {
            double xStep = (xMax - xMin) / bitmap.Width;
            double yStep = (yMax - yMin) / bitmap.Height;

            Poly derivative = polynomial.Derive();
            List<Cplx> roots = new List<Cplx>();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Cplx point = CreatePoint(x, y, xMin, yMin, xStep, yStep);

                    int iterations;
                    Cplx root = FindRoot(polynomial, derivative, point, out iterations);

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
        private Cplx CreatePoint(int x, int y, double xMin, double yMin,
            double xStep, double yStep)
        {
            Cplx point = new Cplx
            {
                Re = xMin + x * xStep,
                Imaginari = (float)(yMin + y * yStep)
            };

            if (point.Re == 0)
            {
                point.Re = ZeroReplacement;
            }

            if (point.Imaginari == 0)
            {
                point.Imaginari = (float)ZeroReplacement;
            }

            return point;
        }

        /// <summary>
        /// Pomoci Newtonovy metody najde priblizny koren.
        /// Zaroven vraci pocet iteraci.
        /// </summary>
        /// <summary>
        /// Pomoci Newtonovy metody hleda koren polynomu.
        /// Pokud vypocet selze, vrati null.
        /// </summary>
        private Cplx FindRoot(Poly polynomial, Poly derivative,
            Cplx point, out int iterations)
        {
            iterations = 0;

            for (int i = 0; i < MaxIterations; i++)
            {
                Cplx derivativeValue = derivative.Eval(point);

                // Kontrola derivace pred delenim
                if (!IsFinite(derivativeValue) ||
                    derivativeValue.GetMagnitude() < DerivativeThreshold)
                {
                    return null;
                }

                Cplx difference = polynomial.Eval(point)
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
                    difference.Re * difference.Re +
                    difference.Imaginari * difference.Imaginari;

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
        private bool IsFinite(Cplx number)
        {
            return !double.IsNaN(number.Re) &&
                   !double.IsInfinity(number.Re) &&
                   !float.IsNaN(number.Imaginari) &&
                   !float.IsInfinity(number.Imaginari);
        }

        /// <summary>
        /// Zjisti, jestli uz je koren znamy.
        /// Kdyz neni, prida ho do seznamu.
        /// </summary>
        private int GetRootIndex(List<Cplx> roots, Cplx root)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                double distance =
                    Math.Pow(root.Re - roots[i].Re, 2) +
                    Math.Pow(root.Imaginari - roots[i].Imaginari, 2);

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
            Color baseColor = colors[rootIndex % colors.Length];

            int red = Math.Max(0, baseColor.R - iterations * 2);
            int green = Math.Max(0, baseColor.G - iterations * 2);
            int blue = Math.Max(0, baseColor.B - iterations * 2);

            return Color.FromArgb(red, green, blue);
        }
    }
}