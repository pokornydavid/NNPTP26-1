using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Poly
        {
            /// <summary>
            /// Coe
            /// </summary>
            public List<Cplx> Coe { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public Poly() => Coe = new List<Cplx>();

            public void Add(Cplx coe) =>
                Coe.Add(coe);

            /// <summary>
            /// Vypocita derivaci aktualniho polynomu.
            /// Vysledkem je novy polynom.
            /// </summary>
            public Poly Derive()
            {
                Poly derivative = new Poly();

                for (int exponent = 1; exponent < Coe.Count; exponent++)
                {
                    Cplx coefficient = Coe[exponent];

                    Cplx derivedCoefficient = coefficient.Multiply(
                        new Cplx { Re = exponent });

                    derivative.Add(derivedCoefficient);
                }

                return derivative;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public Cplx Eval(double x)
            {
                var y = Eval(new Cplx() { Re = x, Imaginari = 0 });
                return y;
            }

            /// <summary>
            /// Vypocita hodnotu polynomu pro zadane komplexni cislo.
            /// Pouziva Hornerovo schema pro jednodussi vypocet.
            /// </summary>
            public Cplx Eval(Cplx x)
            {
                Cplx result = Cplx.Zero;

                for (int i = Coe.Count - 1; i >= 0; i--)
                {
                    result = result.Multiply(x).Add(Coe[i]);
                }

                return result;
            }


            /// <summary>
            /// Prevede polynom na textovy zapis.
            /// Jednotlive cleny spoji podle jejich exponentu.
            /// </summary>
            public override string ToString()
            {
                StringBuilder result = new StringBuilder();

                for (int exponent = 0; exponent < Coe.Count; exponent++)
                {
                    if (exponent > 0)
                    {
                        result.Append(" + ");
                    }

                    result.Append(Coe[exponent]);
                    result.Append('x', exponent);
                }

                return result.ToString();
            }
        }
    }
}
