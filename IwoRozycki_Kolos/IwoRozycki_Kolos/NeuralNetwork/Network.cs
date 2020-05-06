using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IwoRozycki_Kolos.NeuralNetwork.InputFunctions;
using IwoRozycki_Kolos.NeuralNetwork.ActivationFunctions;
using System.IO;

namespace IwoRozycki_Kolos.NeuralNetwork
{
    class Network
    {
        //Lista warstw dla danej sieci
        public List<Layer> Layers { get; set; } = new List<Layer>();
        //Jak szybko dana sieć ma się uczyć
        public double LearningRate { get; set; }
        //Konstruktor sieci, gdzie generowane są warsty zależnie od tego jaką ilośc neuronów podamy w int[] neuronsCount
        public Network(double learningRate,IInputFunction inputFunction,IActivationFunction activationFunction,params int[] neuronsCount)
        {
            LearningRate = learningRate;

            foreach(int n in neuronsCount)
            {
                Layer layer = new Layer();
                Layers.Add(layer);

                for(int i = 0; i < n; i++)
                {
                    //Każdy wygenerowany neuron ma tą samą funkcje aktywacyjną i wejściową podaną w konstruktorze sieci
                    layer.Neurons.Add(new Neuron(activationFunction, inputFunction));
                }
            }
            //Łączenie warst opisane niżej
            ConnectLayers();
        }

        public void Train(double[][] inputValues, double[] expectedValues,int epochs)
        {//Trenowanie sieci, na podstawie informacji wejściowych i oczekiwanych obliczany jest błąd i wagi dla synaps są modyfikowane na podstawie tego błędu, int epochs to ilość powtórzeń

            for(int i = 0; i < epochs; i++)
            {
                for(int j = 0; j < inputValues.Length; j++)
                {
                    double[] outputs = Calculate(inputValues[j]);
                    CalculateErrors(outputs, expectedValues);
                    UpdateWeights();
                }
            }
        }
        //Funkcja zwraca końcowy output
        public double[] Calculate(double[] input)
        {
            PushInputValues(input);

            double[] outputs = new double[Layers.Last().Neurons.Count];

            for(int i = 0; i < outputs.Length; i++)
            {
                Neuron current = Layers.Last().Neurons[i];
                outputs[i] = current.OutputValue;
            }
            return outputs;
        }
        //Funkcja wprowadza input z pierwszej warstwy i wylicza output z wszystkich neuronów w ukrytych warstwach
        public void PushInputValues(double[] inputs)
        {
            for(int i = 0; i < inputs.Length; i++)
            {
                Neuron current = Layers.First().Neurons[i];
                current.OutputValue=current.InputValue = inputs[i];
            }

            for(int i = 0; i < Layers.Count; i++)
            {
                foreach(Neuron n in Layers[i].Neurons)
                {
                    n.PushValueOnOutput(n.CalculateOutPut());
                }
            }
        }
        //Łączy wszystkie neurony z poprzedniej warstwy pokolei z każdym neuronem z kolejnej warstwy za pomocą synaps
        private void ConnectLayers()
        {
            for(int i = 1; i < Layers.Count; i++)
            {
                foreach(Neuron n in Layers[i].Neurons)
                {
                    foreach(Neuron prev in Layers[i - 1].Neurons)
                    {
                        Synapse synapse = new Synapse(prev, n);
                        n.Inputs.Add(synapse);
                        prev.Outputs.Add(synapse);
                    }
                }
            }
        }

        private void CalculateErrors(double[] outputs,double[] expectedValues)
        {   //Błąd sieci dla ostatniej warstwy
            for(int i = 0; i < outputs.Length; i++)
            {
                //Błąd obliczany na podstawie pochodnej funkcji aktywacyjnej, zapisany jest jako gradient dla każdego neurona(w tym przypdaku z ostaniej warstwy)
                Neuron current = Layers.Last().Neurons[i];
                current.Gradient = current.ActivationFunction.Derivative(current.InputValue) * (expectedValues[i] - outputs[i]);
            }
            //tutaj wyliczamy błąd dla reszty warstw pomijając pierwszą i ostanią, na podstawie danych wyjściowych z neurona z warstwy niżej
            for(int i = Layers.Count - 2; i > 0; i--)
            {
                for(int j = 0; j < Layers[i].Neurons.Count; j++)
                {
                    double x = 0;

                    for(int k = 0; k < Layers[i + 1].Neurons.Count; k++)
                    {
                        x += Layers[i + 1].Neurons[k].Gradient * Layers[i].Neurons[j].Outputs[k].Weight;
                    }
                    Neuron current = Layers[i].Neurons[j];
                    current.Gradient = x * current.ActivationFunction.Derivative(current.InputValue);
                }
            }
        }
        //Funckja modyfikująca wagi na podstawie wyliczonej delty, która jest zależna od błędu i wspólczynika nauczania
        private void UpdateWeights()
        {
            for(int i = Layers.Count - 1; i > 0; i--)
            {
                for(int j = 0; j < Layers[i].Neurons.Count; j++)
                {
                    for(int k = 0; k < Layers[i - 1].Neurons.Count; k++)
                    {
                        double delta = Layers[i].Neurons[j].Gradient * Layers[i - 1].Neurons[k].OutputValue;
                        Layers[i].Neurons[j].Inputs[k].UpdateWeight(LearningRate, delta);
                    }
                }
            }
        }
        //Funckja która sprawdza poprawność sieci, 0.95 i 0.05 zostały wybrane jako granice po zaobserwowaniu danych wyjściowych
        public double Test(double[][] testIn, double[] testExpected)
        {
            double acc = 0;
            for (int i = 0; i < 1; i++)
            {
                double[] output = new double[1];
                for (int j = 0; j < testIn.Length; j++)
                {//Jeśli sieć poprawnie odgadnie wynik zwiększamy wartość dokładności acc którą dzielimy potem przez ilość prób
                    output = Calculate(testIn[j]);
                    if (testExpected[j] == 1 && output[i] > 0.95)
                        acc += 1;
                    else if (testExpected[j] == 0 && output[i] < 0.05)
                        acc += 1;
                }

            }
            return acc / testIn.Length;
        }
        //Dwie kolejne funkcje służą do zapisania wag z synaps do pliku i ponownego odczytu tych wag, tak jak opisane w zadaniu
        public void SaveWeights()
        {
            List<string> weights = new List<string>();

            foreach(Layer layer in Layers)
            {
                foreach(Neuron n in layer.Neurons)
                {
                    foreach(Synapse s in n.Inputs)
                    {
                        weights.Add(s.Weight.ToString());
                    }
                }
            }
            File.WriteAllLines("weights.txt", weights);
        }

        public void LoadWeights()
        {
            if (File.Exists("weights.txt"))
            {
                int i = 0;
                string[] linie = File.ReadAllLines("weights.txt");

                foreach(Layer layer in Layers)
                {
                    foreach(Neuron n in layer.Neurons)
                    {
                        foreach(Synapse s in n.Inputs)
                        {
                            s.Weight = Double.Parse(linie[i++]);
                        }
                    }
                }
            }
        }
    }
}
