using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwoRozycki_Kolos.NeuralNetwork.ActivationFunctions
{//Interfejs dla funckji aktywacyjnych
    interface IActivationFunction
    {
        double CalculateOutput(double input);
        double Derivative(double input);
    }
}
