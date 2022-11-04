﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    public class TipoLivro
    {
        public decimal til_id_tipo_livro { get; set; }
        public string til_ds_descricao { get; set; }

        public TipoLivro(decimal til_id_tipo_livro, string til_ds_descricao)
        {
            this.til_id_tipo_livro = til_id_tipo_livro;
            this.til_ds_descricao = til_ds_descricao;
        }
    }
}