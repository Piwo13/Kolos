using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IwoRozycki_Kolos
{
    static class ZbioryMiekkie
    {
        public static double[][] Dane(string path)
        {
            string[] linie = File.ReadAllLines(path);
            linie = linie.Skip(1).ToArray();
            double[][] tab = new double[linie.Length][];

            for (int i = 0; i < linie.Length; i++)
            {
                string[] temp = linie[i].Split(',');
                tab[i] = new double[temp.Length];

                for (int j = 0; j < temp.Length; j++)
                {
                    tab[i][j] = Convert.ToDouble(temp[j].Replace('.', ','));
                }
            }
            return tab;
        }

        public static double Max(double[] tab)
        {
            double max = tab[0];

            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] > max)
                    max = tab[i];
            }
            return max;
        }

        public static void Normnround(this double[][] tab)
        {
            for (int i = 0; i < tab[0].Length - 1; i++)
            {
                //Wybieramy wartość max i min jako pierwsza wartość w danej kolumnie
                double max = tab[0][i];
                double min = tab[0][i];
                for (int j = 0; j < tab.Length; j++)
                {   //Przechodzimy po kolumnach i szukamy wartości większej niż wyznaczone wyżej max i mniejszej niż min
                    if (tab[j][i] > max)
                        max = tab[j][i];
                    else if (tab[j][i] < min)
                        min = tab[j][i];
                }
                for (int j = 0; j < tab.Length; j++)
                {//działanie normalizacji na wszystkich danych pomijając ostatnią kolumnę
                    tab[j][i] = (tab[j][i] - min) / (max - min);
                    tab[j][i] = Math.Round(tab[j][i], MidpointRounding.AwayFromZero);
                }
            }
        }

        public static void Zmiekkie(double[][] tab, double[] wagi)
        {
            double[] sumy = new double[tab.Length];
            for (int i = 0; i < tab.Length; i++)
            {
                double x = 0;

                for (int j = 0; j < wagi.Length; j++)
                {
                    x += wagi[j] * tab[i][j];
                }
                sumy[i] = x;
            }
            double max = ZbioryMiekkie.Max(sumy);

            for (int i = 0; i < sumy.Length; i++)
            {
                if (max == sumy[i])
                    Console.WriteLine($"Najbliżej podanym wartością pasuje osoba nr {i + 1}");
            }
        }
    }
}
