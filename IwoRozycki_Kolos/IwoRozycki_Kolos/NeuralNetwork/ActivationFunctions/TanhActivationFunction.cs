using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwoRozycki_Kolos.NeuralNetwork.ActivationFunctions
{//Funkcje aktywacyjne
    class TanhActivationFunction : IActivationFunction
    {
        public double CalculateOutput(double input)
        {
            return (2 / (1 + Math.Exp(-2 * input))) - 1;
        }

        public double Derivative(double input)
        {
            return 1 - Math.Pow(CalculateOutput(input), 2);
        }
    }
}
