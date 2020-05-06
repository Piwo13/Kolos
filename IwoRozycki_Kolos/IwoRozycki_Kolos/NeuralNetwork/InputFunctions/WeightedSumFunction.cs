using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwoRozycki_Kolos.NeuralNetwork.InputFunctions
{
    class WeightedSumFunction : IInputFunction
    {//Funckja sumująca wyjścia z synaps pomnożone przez ich wagi
        public double CalculateInput(List<Synapse> inputs)
        {
            return inputs.Select(x => x.Weight * x.Output).Sum();
        }
    }
}
