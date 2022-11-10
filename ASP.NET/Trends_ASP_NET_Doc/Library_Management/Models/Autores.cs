using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management.Models
{
    [Serializable]
    public class Autores
    {
        public decimal aut_id_autor { get; set; }
        public string aut_nm_name { get; set; }
        public string aut_nm_sobrenome { get; set; }
        public string aut_ds_email { get; set; }

        public Autores(decimal aut_id_autor, string aut_nm_name, string aut_nm_sobrenome, string aut_ds_email)
        {
            this.aut_id_autor = aut_id_autor;
            this.aut_nm_name = aut_nm_name;
            this.aut_nm_sobrenome = aut_nm_sobrenome;
            this.aut_ds_email = aut_ds_email;
        }
    }
}
