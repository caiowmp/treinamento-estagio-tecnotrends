using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Questao_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //var lista = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var lista = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i' };

            Console.WriteLine("Valores originais da lista");
            PrintList(lista);

            var listaModificada = lista.ApplyModify((list) =>
            {
                var listMod = list;
                for (int i = 0; i < list.Count(); i++)
                {
                    if (i % 2 == 0)
                    {
                        listMod.Remove(list[i]);
                    }
                }

                return listMod;
            });

            Console.WriteLine("Valores da lista modificada");
            PrintList(listaModificada);

            Console.ReadKey();
        }

        private static void PrintList<T>(List<T> lista)
        {
            foreach (T item in lista)
            {
                Console.WriteLine(item);
            }
        }
    }
}