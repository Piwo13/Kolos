using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IwoRozycki_Kolos
{
    static class ZbioryMiekkie
    {   //Wczytanie danych w podobny sposób jak było to zrobione w WczytDanych.cs
        public static double[][] Dane(string path)
        {
            string[] linie = File.ReadAllLines(path);
            //Pomijamy pierwszą linijkę która zawiera tytuły kolumn
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
                    //Zaokrąglenie wartości do 1 lub 0 aby można było użyc zbioru miękkiego
                    tab[j][i] = Math.Round(tab[j][i], MidpointRounding.AwayFromZero);
                }
            }
        }
        //Funkcja wykonująca działania w zbiorach miękkich
        public static void Zmiekkie(double[][] tab, double[] wagi)
        {
            double[] sumy = new double[tab.Length];
            for (int i = 0; i < tab.Length; i++)
            {//tutaj wykonujemy mnożenie wszystkich wierszy z tabeli danych przez podane przez użytkownika wagi i zbieramy sumy dla każdego wiersza
                double x = 0;

                for (int j = 0; j < wagi.Length; j++)
                {
                    x += wagi[j] * tab[i][j];
                }
                //zapisujemy te sumy do tabeli
                sumy[i] = x;
            }
            //Wyszukujemy największej sumy
            double max = sumy.Max();

            for (int i = 0; i < sumy.Length; i++)
            {//iterujemy po tablicy sum i jak znajdziemy tą największą to wypisujemy numer o jeden większy gdyż przedmioty w tablicy numerowane są od zera
                if (max == sumy[i])
                    Console.WriteLine($"Najbliżej podanym wartością pasuje osoba nr {i + 1}");
            }
        }
       

    }
}
