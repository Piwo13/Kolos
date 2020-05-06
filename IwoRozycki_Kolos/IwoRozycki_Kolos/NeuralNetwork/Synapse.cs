using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwoRozycki_Kolos.NeuralNetwork
{
    class Synapse
    {
        //Właściwości synapsy, z neuronem wejściowym i wyjściowym, wagą i informacją wyjściową
        private Neuron fromNeuron;
        private Neuron toNeuron;

        public double Weight { get; set; }

        public double Output { get; set; }
        
        //Waga jest losowo generowana
        private static readonly Random random = new Random();

        public Synapse()
        {
        }
        //Konstruktor synapsy z losowo generowana wagą
        public Synapse(Neuron fromNeuron, Neuron toNeuron)
        {
            this.fromNeuron = fromNeuron;
            this.toNeuron = toNeuron;
            Weight = random.NextDouble() -0.5;
        }
        //Waga jest modifykowana zależnie od delty(która wyliczana jest na podstawie błędu) oraz szybkości nauczania
        public void UpdateWeight(double learningRate, double delta)
        {
            Weight += learningRate * delta;
        }
    }
}
