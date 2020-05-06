using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwoRozycki_Kolos.NeuralNetwork
{
    class Layer
    {
        //Każda warstwa zawiera liste neuronów znajdujących się w tej warstwie
        public List<Neuron> Neurons { get; set; } = new List<Neuron>();
    }
}
