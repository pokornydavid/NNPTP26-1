using System;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Nacteni a kontrola vstupnich parametru
            if (!FractalSettings.TryParse(args,
                out FractalSettings settings, out string error))
            {
                Console.WriteLine(error);
                return;
            }

            // Vytvoreni polynomu
            Polynomial polynomial = new Polynomial();

            polynomial.Add(new ComplexNumber { Real = 1 });
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(new ComplexNumber { Real = 1 });

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomial.Derive());

            // Vykresleni a ulozeni fraktalu
            using (Bitmap bitmap = new Bitmap(
                settings.Width, settings.Height))
            {
                NewtonFractalRenderer renderer =
                    new NewtonFractalRenderer();

                renderer.Render(bitmap, polynomial,
                    settings.XMin, settings.XMax,
                    settings.YMin, settings.YMax);

                bitmap.Save(settings.OutputPath);
            }
        }
    }
}
