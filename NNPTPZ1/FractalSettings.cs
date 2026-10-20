
using System;
using System.Globalization;

namespace NNPTPZ1
{
    internal class FractalSettings
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public double XMin { get; private set; }
        public double XMax { get; private set; }
        public double YMin { get; private set; }
        public double YMax { get; private set; }

        public string OutputPath { get; private set; }

        /// <summary>
        /// Nacte a zkontroluje argumenty programu.
        /// Pri chybe vrati false a chybovou zpravu.
        /// </summary>
        public static bool TryParse(string[] args,
            out FractalSettings settings, out string error)
        {
            settings = null;
            error = null;

            // Kontrola poctu argumentu
            if (args == null || args.Length != 7)
            {
                error = "Nespravny pocet argumentu.";
                return false;
            }

            // Kontrola rozmeru obrazku
            if (!int.TryParse(args[0], out int width) ||
                !int.TryParse(args[1], out int height) ||
                width <= 0 || height <= 0)
            {
                error = "Sirka a vyska musi byt kladna cela cisla.";
                return false;
            }

            // Kontrola souradnic
            if (!double.TryParse(args[2], NumberStyles.Float,
                    CultureInfo.InvariantCulture, out double xMin) ||
                !double.TryParse(args[3], NumberStyles.Float,
                    CultureInfo.InvariantCulture, out double xMax) ||
                !double.TryParse(args[4], NumberStyles.Float,
                    CultureInfo.InvariantCulture, out double yMin) ||
                !double.TryParse(args[5], NumberStyles.Float,
                    CultureInfo.InvariantCulture, out double yMax))
            {
                error = "Souradnice musi byt platna cisla.";
                return false;
            }

            // Kontrola rozsahu souradnic
            if (!IsFinite(xMin) || !IsFinite(xMax) ||
                !IsFinite(yMin) || !IsFinite(yMax) ||
                xMin >= xMax || yMin >= yMax)
            {
                error = "Neplatny rozsah souradnic.";
                return false;
            }

            // Kontrola vystupniho souboru
            if (string.IsNullOrWhiteSpace(args[6]))
            {
                error = "Chybi nazev vystupniho souboru.";
                return false;
            }

            // Ulozeni platnych hodnot
            settings = new FractalSettings
            {
                Width = width,
                Height = height,
                XMin = xMin,
                XMax = xMax,
                YMin = yMin,
                YMax = yMax,
                OutputPath = args[6]
            };

            return true;
        }

        /// <summary>
        /// Overi, ze cislo neni NaN ani nekonecno.
        /// </summary>
        private static bool IsFinite(double value)
        {
            return !double.IsNaN(value) &&
                   !double.IsInfinity(value);
        }
    }
}
