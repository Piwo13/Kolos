using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwoRozycki_Kolos.NeuralNetwork.ActivationFunctions
{//Funkcje aktywacyjne
    class SoftplusActivationFunction : IActivationFunction
    {
        public double CalculateOutput(double input)
        {
            return Math.Log(1 + Math.Pow(Math.E, input));
        }

        public double Derivative(double input)
        {
            return 1 / (1 + Math.Pow(Math.E, -input));
        }
    }
}
