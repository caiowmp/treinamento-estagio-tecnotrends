using Questao_1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Questao_1
{
    public partial class frmInicial : Form
    {
        public frmInicial()
        {
            InitializeComponent();
        }

        private void FormInicial_Shown(object sender, EventArgs e)
        {

        }

        private void btnChamar_Click(object sender, EventArgs e)
        {
            var dialog = new CustomDialog();
            dialog.Title = "Caixa de diálogo!";
            dialog.Rotulo = "Informe um texto abaixo:";
            dialog.RotuloBotao = "OK!";

            dialog.ButtonOkClickEvent += (obj, args) =>
            {
                label1.Text = String.Format("Você informou a mensagem: \n{0}", ((CustomDialog)obj).Mensagem);
            };

            dialog.Show();
            
            

        }
    }
}