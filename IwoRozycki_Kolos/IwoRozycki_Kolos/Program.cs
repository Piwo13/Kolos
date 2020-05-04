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
            //dane.Shuffle();
            double[][] dane2=ZbioryMiekkie.Dane("diabetes.csv");
            dane2.Normnround();
            double[] wagi1 = new double[] { 0, 0, 0, 0.6, 0, 0.3, 0, 0.1, 0 };
            double[] wagi2 = new double[] { 0.8, 0, 0, 0.1, 0, 0, 0.7, 0, 0.4 };
            //ZbioryMiekkie.Zmiekkie(dane2, wagi1);
            ZbioryMiekkie.Zmiekkie(dane2, wagi2);
            //for(int i = 0; i < dane2.Length; i++)
            //{
            //    for(int j = 0; j < dane2[0].Length; j++)
            //    {
            //        Console.WriteLine("{0}->{1}",dane[i][j],dane2[i][j]);
                    
            //    }
            //}


            Console.ReadKey();

        }
    }
}
