using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IwoRozycki_Kolos.NeuralNetwork;
using IwoRozycki_Kolos.NeuralNetwork.ActivationFunctions;
using IwoRozycki_Kolos.NeuralNetwork.InputFunctions;

namespace IwoRozycki_Kolos
{
    class Program
    {
        static void Main(string[] args)
        {   //Wczytanie danych z bazy Zad1.
            double[][]dane=WczytDanych.Info("diabetes.csv");
            //Normalizacja i tasowanie dnaych
            dane.Normalize();
            dane.Shuffle();

            //Zad2
            //Wczytanie danych z bazy do wykorzystania z klasyfikatorem zbiorów miękkich
            double[][] dane2=ZbioryMiekkie.Dane("diabetes.csv");
            //Normalizacja i zaokrąglenie do "0" lub "1"
            dane2.Normnround();
            //Po wprowadzeniu wag, klasyfikator wybiera z bazy danych osoby które najbardziej pasują, w przypadku wagi2 dwie osoby z listy miały ten sam wynik
            double[] wagi1 = new double[] { 0, 0, 0, 0.6, 0, 0.3, 0, 0.1, 0 };
            double[] wagi2 = new double[] { 0.8, 0, 0, 0.1, 0, 0, 0.7, 0, 0.4 };
            ZbioryMiekkie.Zmiekkie(dane2, wagi1);
            Console.WriteLine();
            ZbioryMiekkie.Zmiekkie(dane2, wagi2);
            Console.WriteLine();

            //Zad3
            //Podział danych na 70% w split[1] i pozostałe 30% w split[0]
            double[][][] split = dane.Splitter();
            //Dane teningowe i oczekiwane wyniki z 70% danych
            double[][] TrainData = WczytDanych.Getinputs(split[1]);
            double[] ExpectedData = WczytDanych.Getoutputs(split[1]);
            //Dane teningowe i oczekiwane wyniki z 30% danych, użyte w celu ustalenie dokładności sieci
            double[][] TestData = WczytDanych.Getinputs(split[0]);
            double[] TestExpected = WczytDanych.Getoutputs(split[0]);
            //Utworzenie sieci, obecna kofiguracja neuronów w 6 warstwach.
            Network network = new Network(0.01, new WeightedSumFunction(), new SigmoidActivationFunction(), 8, 5, 5, 5, 3, 1);
            //Tutaj wczytujemy wagi, jeśli plik istnieje i zapisujemy je po trenowaniu sieci
            network.LoadWeights();
            network.Train(TrainData, ExpectedData, 10000);
            network.SaveWeights();
            //Tutaj testujemy dokładność naszej sieci na zbiorze walidacyjnym(30% całych danych), w obecnej konfiguracji neuronów oscyluje ona w granicach 65%
            Console.WriteLine("Dokładność: "+(network.Test(TestData, TestExpected))*100 + "% ");
            Console.WriteLine();
            //Tutaj tworzymy drugą sieć z tym samym zestawem wag, dokładność dla zbioru walidacyjnego powinna być identyczna do tej powyżej
            Network network2= new Network(0.01, new WeightedSumFunction(), new SigmoidActivationFunction(), 8, 5, 5, 5, 3, 1);
            network2.LoadWeights();
            Console.WriteLine("Dokładność: " + (network2.Test(TestData, TestExpected)) * 100 + "% ");
            Console.WriteLine();
            Console.WriteLine("Finished!");



            Console.ReadKey();

        }
    }
}
