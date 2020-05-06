using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IwoRozycki_Kolos
{
    static class WczytDanych
    {
        //Tutaj wczytujemy dane pomijając pierwszą linijkę która zawiera tylko opisy danych w danej kolumnie
        public static double[][] Info(string path)
        {
            string[] linie = File.ReadAllLines(path);
            linie = linie.Skip(1).ToArray();
            double[][] data = new double[linie.Length][];
            for (int i = 0; i < linie.Length; i++)
            {
                string[] temp = linie[i].Split(',');
                data[i] = new double[temp.Length];
                for (int j = 0; j < temp.Length; j++)
                {
                    //Zamieniamy dane ze string na double z zamianą . na , gdyż wykonuję to na laptopie z polską wersją systemu
                    data[i][j] = Convert.ToDouble(temp[j].Replace('.', ','));
                }
            }
            return data;
        }

        //Tutaj wykonujemy normalizajcę całych danych
        public static void Normalize(this double[][] data)
        {
            for (int i = 0; i < data[0].Length-1; i++)
            {
                //Wybieramy wartość max i min jako pierwsza wartość w danej kolumnie
                double max = data[0][i];
                double min = data[0][i];
                for (int j = 0; j < data.Length; j++)
                {   //Przechodzimy po kolumnach i szukamy wartości większej niż wyznaczone wyżej max i mniejszej niż min
                    if (data[j][i] > max)
                        max = data[j][i];
                    else if (data[j][i] < min)
                        min = data[j][i];
                }
                for (int j = 0; j < data.Length; j++)
                {//działanie normalizacji na wszystkich danych pomijając ostatnią kolumnę
                    data[j][i] = (data[j][i] - min) / (max - min);
                }
            }
        }
        //Normalizacja danych dla pojedynczego wejścia
        public static void Normalize(this double[] data)
        {
            double max = data[0];
            double min = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > max)
                    max = data[i];
                else if (data[i] < min)
                    min = data[i];
            }
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (data[i] - min) / (max - min);
            }
        }
        //Tutaj dokonujemy tasowania danych, zapisujemy wiersz do nowej zmiennej temp, potem zapstępujemy i-ty wiersz losowym r-tym wierszem, do którego potem przypisujemy oryginalny wiersz

        public static void Shuffle(this double[][] data)
        {
            Random rnd = new Random();

            for (int i = 0; i < data.Length; i++)
            {
                double[] temp = data[i];
                int r = rnd.Next(i, data.Length);
                data[i] = data[r];
                data[r] = temp;
            }
        }

        //pobieramy informacje z tabeli poza ostatnią kolumną która zawiera informacje czy dana osoba ma cukrzycę czy nie
        public static double[][] Getinputs(this double[][] data)
        {
            //klonujemy oryginalna tabele 
            double[][] Outtab = (double[][])data.Clone();
            for (int i = 0; i < data.Length; i++)
            {
                //zmiejszamy ją o ta ostatnia kolumnę
                Array.Resize(ref Outtab[i], Outtab[i].Length -1);
            }
            return Outtab;
        }
        //pobieramy informacje z ostaniej kolumny, czyli oczekiwane wyniki
        public static double[] Getoutputs(this double[][] data)
        {
            double[] Outtab = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                
                Outtab[i] = data[i][8];

            }
            return Outtab;
        }
        public static double[][][] Splitter(this double[][] tab)
        {
            //Outtab dzieli sie na dwie wielowymiarowe tablice, jedna będzie zawierała 30% danych druga 70%
            double[][][] Outtab = new double[2][][];
            //wyznaczamy inta którego wykorzystamy do ustalenie długości tablic
            int split = (int)(tab.Length * 0.3);
            Outtab[0] = new double[split][];
            Outtab[1] = new double[tab.Length - split][];

            //Tutaj wyznaczamy 30% danych
            for (int i = 0; i < split; i++)
            {
                Outtab[0][i] = new double[tab[i].Length];
                for (int j = 0; j < tab[i].Length; j++)
                {
                    Outtab[0][i][j] = tab[i][j];
                }
            }
            //tutaj wyznaczamy pozostałe 70% danych
            for (int i = split; i < tab.Length; i++)
            {
                Outtab[1][i - split] = new double[tab[i].Length];
                for (int j = 0; j < tab[i].Length; j++)
                {
                    Outtab[1][i - split][j] = tab[i][j];
                }
            }

            return Outtab;
        }
    }
}
