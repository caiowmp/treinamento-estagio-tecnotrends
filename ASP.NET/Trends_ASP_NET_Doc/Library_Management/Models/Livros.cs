﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management.Models
{
    [Serializable]
    public class Livros
    {
        public decimal liv_id_livro { get; set; }
        public decimal liv_id_tipo_livro { get; set; }
        public decimal liv_id_editor { get; set; }
        public string liv_nm_titulo { get; set; }
        public string liv_ds_resumo { get; set; }
        public int liv_nu_edicao { get; set; }
        public decimal liv_vl_preco { get; set; }
        public decimal liv_pc_royalty { get; set; }

        public Livros(decimal liv_id_livro, decimal liv_id_tipo_livro, decimal liv_id_editor, string liv_nm_titulo, decimal liv_vl_preco, decimal liv_pc_royalty, string liv_ds_resumo, int liv_nu_edicao)
        {
            this.liv_id_livro = liv_id_livro;
            this.liv_id_tipo_livro = liv_id_tipo_livro;
            this.liv_id_editor = liv_id_editor;
            this.liv_nm_titulo = liv_nm_titulo;
            this.liv_vl_preco = liv_vl_preco;
            this.liv_pc_royalty = liv_pc_royalty;
            this.liv_ds_resumo = liv_ds_resumo;
            this.liv_nu_edicao = liv_nu_edicao;
        }
    }
}
