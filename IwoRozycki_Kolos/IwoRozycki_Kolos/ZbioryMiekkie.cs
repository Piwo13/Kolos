using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IwoRozycki_Kolos
{
    class ZbioryMiekkie
    {
        public static double[][] Dane(string path)
        {
            string[] linie = File.ReadAllLines(path);
            linie = linie.Skip(1).ToArray();
            double[][] tab = new double[linie.Length][];

            for(int i = 0; i < linie.Length; i++)
            {
                string[] temp = linie[i].Split(',');
                tab[i] = new double[temp.Length];

                for(int j = 0; j < temp.Length; j++)
                {
                    tab[i][j] = Convert.ToDouble(temp[j].Replace('.', ','));
                }
            }
            return tab;
        }

        public static double Max(double[] tab)
        {
            double max = tab[0];
            
            for(int i = 0; i < tab.Length; i++)
            {
                if (tab[i] > max)
                    max = tab[i];
            }
            return max;
        }
        public static void Zmiekkie(double[] wejscie,double[][] tab,double[]wagi)
        {
            double[] sumy = new double[tab.Length];
            for(int i = 0; i < tab.Length; i++)
            {
                double x = 0;

                for(int j = 0; j < wejscie.Length; j++)
                {
                    if (wejscie[j] == tab[i][j])
                    {
                        x += wagi[j] * wejscie[j];
                    }
                }
                sumy[i] = x;
            }
            double max = ZbioryMiekkie.Max(sumy);

            for(int i = 0; i < sumy.Length; i++)
            {
                if (max == sumy[i])
                    Console.WriteLine($"Najbliżej podanym wartością pasuje osoba nr {i + 1}");
            }
        }



    }
}
