using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management.Models
{
    internal class TipoLivro
    {
        public decimal til_id_tipo_livro { get; set; }
        public string til_ds_resumo { get; set; }

        public TipoLivro(decimal til_id_tipo_livro, string til_ds_resumo)
        {
            this.til_id_tipo_livro = til_id_tipo_livro;
            this.til_ds_resumo = til_ds_resumo;
        }
    }
}
