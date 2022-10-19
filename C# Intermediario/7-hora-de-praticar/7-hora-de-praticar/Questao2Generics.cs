using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HoraDePraticar
{
    public class Questao2Generics<TTipo>
    {
        public delegate void DelegateGenerico(TTipo parametro);
        public void Print(TTipo parametro)
        {
            Console.WriteLine(Convert.ToString(parametro));
        } 
        public void MetodoGenerico(DelegateGenerico delegateGenerico, List<TTipo> lista)
        {
            DelegateGenerico delegateGenerics = new DelegateGenerico(delegateGenerico);
            foreach(TTipo item in lista)
            {
                delegateGenerics(item);
            }
        }
    }
}
