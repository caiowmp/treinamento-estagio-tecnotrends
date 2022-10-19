using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraDePraticar
{
    public class Questao1Generics<TTipo>
    {
        public Questao1Generics()
        {
        }
        public int MetodoGenerico(List<TTipo> lista, TTipo menor)
        {
            int cont = 0;
            foreach(TTipo item in lista)
            {
                if(Convert.ToByte(item) > Convert.ToByte(menor))
                {
                    cont++;
                }
            }
            return cont;
        }

    }
}
