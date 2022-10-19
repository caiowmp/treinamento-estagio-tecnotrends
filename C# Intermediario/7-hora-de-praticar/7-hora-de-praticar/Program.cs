
namespace HoraDePraticar
{
    public class Program
    {
        delegate void Acao<T>(T[] array);
        static void Main()
        {
            /*Qestão 01
            Questao1Generics<int> questao1GenericsInt = new Questao1Generics<int>();
            Questao1Generics<double> questao1GenericsDouble = new Questao1Generics<double>();
            Questao1Generics<string> questao1GenericsString = new Questao1Generics<string>();
            List<int> listaInt = new List<int>();
            listaInt.Add(1); listaInt.Add(2); listaInt.Add(3); listaInt.Add(4); listaInt.Add(5);
            int menorInt = 2;
            Console.WriteLine(questao1GenericsInt.MetodoGenerico(listaInt, menorInt));
            List<double> listaDouble = new List<double>();
            listaDouble.Add(1.5); listaDouble.Add(2.5); listaDouble.Add(3.5); listaDouble.Add(4); listaDouble.Add(5);
            double menorDouble = 2.5;
            Console.WriteLine(questao1GenericsDouble.MetodoGenerico(listaDouble, menorDouble));
            List<string> listaString = new List<string>();
            listaString.Add("1"); listaString.Add("2"); listaString.Add("3"); listaString.Add("4"); listaString.Add("5");
            string menorString = "2";
            Console.WriteLine(questao1GenericsString.MetodoGenerico(listaString, menorString));
            Console.ReadKey();*/

            /* Questçao 02
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //mostrando o array original
            Console.WriteLine("Lista origianal!");
            foreach (var item in array)
                Console.Write("{0}, ", item);

            //aplicando o delegate
            var del = new Acao<int>(DuplicandoElementos);
            del(array);

            Console.WriteLine("\n\n");

            //mostrado o array transformado pelo delegate
            Console.WriteLine("Lista transformada pelo delegate!");
            foreach (var item in array)
                Console.Write("{0}, ", item);

            Console.ReadKey();*/
            
        }
        public static void DuplicandoElementos(int[] array)
        {
            //duplicando os elementos do array
            for (int i = 0; i < array.Count(); i++)
                array[i] *= 2;
        }
    }
}