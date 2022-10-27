using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Questao_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var lista1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var lista2 = new List<int>() { 8, 9, 10, 11, 12, 13, 14, 15 };

            Console.WriteLine("Valores da lista1:");
            PrintList(lista1);

            Console.WriteLine("Valores da lista2:");
            PrintList(lista2);

            Console.WriteLine("Valores presentes em ambas as listas:");
            ShowEquals(lista1, lista2);

            Console.WriteLine("Valores que só aparecem em uma das listas:");
            ShowNotEquals(lista1, lista2);

            Console.ReadKey();
        }

        private static void ShowEquals(List<int> lista1, List<int> lista2)
        {
            var lista = new List<int>();

            foreach (var item1 in lista1)
                foreach (var item2 in lista2)
                    if (item1 == item2)
                        lista.Add(item2);

            PrintList(lista);
        }

        private static void ShowNotEquals(List<int> lista1, List<int> lista2)
        {
            var lista = new List<int>();

            foreach (var item in lista1)
                if (!lista2.Contains(item))
                    lista.Add(item);

            foreach (var item in lista2)
                if (!lista1.Contains(item))
                    lista.Add(item);

            PrintList(lista);
        }

        private static void PrintList<T>(List<T> lista)
        {
            foreach (T item in lista)
                Console.WriteLine(item);
        }
    }
}
