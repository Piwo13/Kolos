using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwoRozycki_Kolos.NeuralNetwork.InputFunctions
{//Interfejs dla funckji wejsciowych
    interface IInputFunction
    {
        double CalculateInput(List<Synapse> inputs);
    }
}
