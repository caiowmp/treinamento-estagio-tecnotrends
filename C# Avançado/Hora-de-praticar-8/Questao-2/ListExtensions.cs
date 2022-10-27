using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao_2
{
    public static class ListExtensions
    {
        public static List<T> ApplyModify<T>(this List<T> lista, Func<List<T>, List<T>> criterio)
        {
            return criterio(lista);
        }
    }
}
