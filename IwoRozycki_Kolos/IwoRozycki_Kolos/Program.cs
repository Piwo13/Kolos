using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwoRozycki_Kolos
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][]dane=WczytDanych.Info("diabetes.csv");
            dane.Normalize();
            dane.Shuffle();
            double[][] dane2=ZbioryMiekkie.Dane("diabetes.csv");
            double[] wejscie = new double[] { 6, 148, 72, 35, 0, 33.6, 0.627, 50, 1 };
            double[] wagi = new double[] { 0.2, 0.3, 0.5, 0, 0.5, 0, 0.2, 0.3, 0 };
            ZbioryMiekkie.Zmiekkie(wejscie, dane2, wagi);

            ZbioryMiekkie.Dane("diabetes.csv");
            Console.ReadKey();

        }
    }
}
