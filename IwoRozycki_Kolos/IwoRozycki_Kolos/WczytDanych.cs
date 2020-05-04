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
    }
}
