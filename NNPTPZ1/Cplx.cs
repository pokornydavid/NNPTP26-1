
using System;

namespace NNPTPZ1.Mathematics
{
    public class Cplx
    {
        // Realna a imaginarni cast komplexniho cisla
        public double Re { get; set; }
        public float Imaginari { get; set; }

        public static readonly Cplx Zero = new Cplx
        {
            Re = 0,
            Imaginari = 0
        };

        /// <summary>
        /// Porovna dve komplexni cisla podle jejich hodnot.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is Cplx other))
            {
                return false;
            }

            return Re == other.Re &&
                   Imaginari == other.Imaginari;
        }

        /// <summary>
        /// Vytvori hash kod podle realne a imaginarni casti.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Re.GetHashCode();
                hash = hash * 31 + Imaginari.GetHashCode();

                return hash;
            }
        }

        /// <summary>
        /// Secte dve komplexni cisla.
        /// </summary>
        public Cplx Add(Cplx other)
        {
            return new Cplx
            {
                Re = Re + other.Re,
                Imaginari = Imaginari + other.Imaginari
            };
        }

        /// <summary>
        /// Odecte druhe komplexni cislo od aktualniho.
        /// </summary>
        public Cplx Subtract(Cplx other)
        {
            return new Cplx
            {
                Re = Re - other.Re,
                Imaginari = Imaginari - other.Imaginari
            };
        }

        /// <summary>
        /// Vynasobi dve komplexni cisla.
        /// </summary>
        public Cplx Multiply(Cplx other)
        {
            double realPart =
                Re * other.Re - Imaginari * other.Imaginari;

            float imaginaryPart = (float)(
                Re * other.Imaginari + Imaginari * other.Re);

            return new Cplx
            {
                Re = realPart,
                Imaginari = imaginaryPart
            };
        }

        /// <summary>
        /// Vydeli aktualni komplexni cislo druhym.
        /// </summary>
        internal Cplx Divide(Cplx other)
        {
            double denominator =
                other.Re * other.Re +
                other.Imaginari * other.Imaginari;

            Cplx conjugate = new Cplx
            {
                Re = other.Re,
                Imaginari = -other.Imaginari
            };

            Cplx numerator = Multiply(conjugate);

            return new Cplx
            {
                Re = numerator.Re / denominator,
                Imaginari = (float)(numerator.Imaginari / denominator)
            };
        }

        /// <summary>
        /// Vypocita velikost komplexniho cisla.
        /// </summary>
        public double GetMagnitude()
        {
            return Math.Sqrt(Re * Re + Imaginari * Imaginari);
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
            return Math.Atan2(Imaginari, Re) * 180.0 / Math.PI;
        }

        /// <summary>
        /// Prevede komplexni cislo na textovy zapis.
        /// </summary>
        public override string ToString()
        {
            return $"({Re} + {Imaginari}i)";
        }
    }
}
