using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Polynomial
        {
            /// <summary>
            /// Koeficienty polynomu.
            /// </summary>
            public List<ComplexNumber> Coefficients { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public Polynomial() => Coefficients = new List<ComplexNumber>();

            public void Add(ComplexNumber coefficient) =>
                Coefficients.Add(coefficient);

            /// <summary>
            /// Vypocita derivaci aktualniho polynomu.
            /// Vysledkem je novy polynom.
            /// </summary>
            public Polynomial Derive()
            {
                Polynomial derivative = new Polynomial();

                for (int exponent = 1; exponent < Coefficients.Count; exponent++)
                {
                    ComplexNumber coefficient = Coefficients[exponent];

                    ComplexNumber derivedCoefficient = coefficient.Multiply(
                        new ComplexNumber { Real = exponent });

                    derivative.Add(derivedCoefficient);
                }

                return derivative;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(double x)
            {
                ComplexNumber result = Eval(new ComplexNumber() { Real = x, Imaginary = 0 });
                return result;
            }

            /// <summary>
            /// Vypocita hodnotu polynomu pro zadane komplexni cislo.
            /// Pouziva Hornerovo schema pro jednodussi vypocet.
            /// </summary>
            public ComplexNumber Eval(ComplexNumber x)
            {
                ComplexNumber result = ComplexNumber.Zero;

                for (int coefficientIndex = Coefficients.Count - 1;
                    coefficientIndex >= 0; coefficientIndex--)
                {
                    result = result.Multiply(x).Add(Coefficients[coefficientIndex]);
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

                for (int exponent = 0; exponent < Coefficients.Count; exponent++)
                {
                    if (exponent > 0)
                    {
                        result.Append(" + ");
                    }

                    result.Append(Coefficients[exponent]);
                    result.Append('x', exponent);
                }

                return result.ToString();
            }
        }
    }
}
