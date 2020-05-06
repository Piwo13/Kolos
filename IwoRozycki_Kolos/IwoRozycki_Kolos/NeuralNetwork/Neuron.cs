using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IwoRozycki_Kolos.NeuralNetwork.InputFunctions;
using IwoRozycki_Kolos.NeuralNetwork.ActivationFunctions;


namespace IwoRozycki_Kolos.NeuralNetwork
{
    class Neuron
    {
        //Właściwości neurona, listy synaps zawierjące wejścia i wyjścia, funkcje, oraz informacja wejściowa i wyjściowa 
        public List<Synapse> Inputs { get; set; } = new List<Synapse>();
        public List<Synapse> Outputs { get; set; } = new List<Synapse>();

        public IActivationFunction ActivationFunction { get; set; }
        public IInputFunction InputFunction { get; set; }

        public double Gradient { get; set; }
        public double OutputValue { get; set; }
        public double InputValue { get; set; }

        public Neuron(IActivationFunction activationFunction, IInputFunction inputFunction)
        {
            //Konstruktor Neurona z podanymi funkcjami aktywacyjnimi i wejściowymi
            ActivationFunction = activationFunction;
            InputFunction = inputFunction;
        }
        public double CalculateOutPut()
        {
            //Pierwsza warstwa neuronow nie ma informacji na wejsciu, wiec nie liczymy informacji na wyjsciu
            if (Inputs.Count == 0) return InputValue;
            //wejscie liczymy używajać danych wyjściowych z poprzedniego neurona i funkcji Input
            InputValue = InputFunction.CalculateInput(Inputs);
            //wyjście liczymy używajać danych wejściowych i funkcji Aktywacyjnej
            OutputValue = ActivationFunction.CalculateOutput(InputValue);
            return OutputValue;
        }

        public void PushValueOnOutput(double outputValue)
        {
            //Nowe wartosci dla synaps wyjściowych
            Outputs.ForEach(output => output.Output = outputValue);
        }
    }
}
