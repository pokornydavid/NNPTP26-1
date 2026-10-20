using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        // Realna a imaginarni cast komplexniho cisla
        public double Real { get; set; }
        public double Imaginary { get; set; }

        // Nulove komplexni cislo
        public static ComplexNumber Zero => new ComplexNumber
        {
            Real = 0,
            Imaginary = 0
        };

        /// <summary>
        /// Porovna dve komplexni cisla podle jejich hodnot.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is ComplexNumber other))
            {
                return false;
            }

            return Real == other.Real &&
                   Imaginary == other.Imaginary;
        }

        /// <summary>
        /// Vytvori hash kod podle realne a imaginarni casti.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Real.GetHashCode();
                hash = hash * 31 + Imaginary.GetHashCode();

                return hash;
            }
        }

        /// <summary>
        /// Secte dve komplexni cisla.
        /// </summary>
        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber
            {
                Real = Real + other.Real,
                Imaginary = Imaginary + other.Imaginary
            };
        }

        /// <summary>
        /// Odecte druhe komplexni cislo od aktualniho.
        /// </summary>
        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber
            {
                Real = Real - other.Real,
                Imaginary = Imaginary - other.Imaginary
            };
        }

        /// <summary>
        /// Vynasobi dve komplexni cisla.
        /// </summary>
        public ComplexNumber Multiply(ComplexNumber other)
        {
            double realPart =
                Real * other.Real - Imaginary * other.Imaginary;

            double imaginaryPart =
                Real * other.Imaginary + Imaginary * other.Real;

            return new ComplexNumber
            {
                Real = realPart,
                Imaginary = imaginaryPart
            };
        }

        /// <summary>
        /// Vydeli aktualni komplexni cislo druhym.
        /// </summary>
        internal ComplexNumber Divide(ComplexNumber other)
        {
            double denominator =
                other.Real * other.Real +
                other.Imaginary * other.Imaginary;

            if (denominator == 0)
            {
                throw new DivideByZeroException("Nelze delit nulovym komplexnim cislem.");
            }

            ComplexNumber conjugate = new ComplexNumber
            {
                Real = other.Real,
                Imaginary = -other.Imaginary
            };

            ComplexNumber numerator = Multiply(conjugate);

            return new ComplexNumber
            {
                Real = numerator.Real / denominator,
                Imaginary = numerator.Imaginary / denominator
            };
        }

        /// <summary>
        /// Vypocita velikost komplexniho cisla.
        /// </summary>
        public double GetMagnitude()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        /// <summary>
        /// Zachovani puvodni metody pro kompatibilitu.
        /// </summary>
        public double GetAbS()
        {
            return GetMagnitude();
        }

        /// <summary>
        /// Vypocita uhel komplexniho cisla ve stupnich.
        /// </summary>
        public double GetAngleInDegrees()
        {
            return Math.Atan2(Imaginary, Real) * 180.0 / Math.PI;
        }

        /// <summary>
        /// Prevede komplexni cislo na textovy zapis.
        /// </summary>
        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }
    }
}
