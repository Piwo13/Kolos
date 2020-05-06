using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwoRozycki_Kolos.NeuralNetwork.ActivationFunctions
{//Funkcje aktywacyjne
    class SigmoidActivationFunction : IActivationFunction
    {
        public double CalculateOutput(double input)
        {
            return 1 / (1 + Math.Exp(-input));

        }

        public double Derivative(double input)
        {
            return CalculateOutput(input) * (1 - CalculateOutput(input));
        }
    }
}
