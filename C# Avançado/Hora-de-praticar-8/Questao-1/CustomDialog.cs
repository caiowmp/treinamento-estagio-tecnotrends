using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questao_1
{
    public class CustomDialog : Form
    {
        //public delegate void ButtonOkClickEventHandler(object sender, EventArgs e);
        public event EventHandler ButtonOkClickEvent;

        private Label lblRotulo;
        private TextBox txtMensagem;
        private Button btnOk;

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public string Rotulo
        {
            get { return lblRotulo.Text; }
            set { lblRotulo.Text = value; }
        }

        public string Mensagem
        {
            get { return txtMensagem.Text; }
            set { txtMensagem.Text = value; }
        }

        public string RotuloBotao
        {
            get { return btnOk.Text; }
            set { btnOk.Text = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.Controls != null))
                foreach (Control control in this.Controls)
                    control.Dispose();

            base.Dispose(disposing);
        }

        public virtual void OnButtonOkClick(EventArgs e)
        {
            this.Hide();

            ButtonOkClickEvent?.Invoke(this, e);
        }


        public CustomDialog()
        {
            this.lblRotulo = new Label();
            this.txtMensagem = new TextBox();
            this.btnOk = new Button();
            this.SuspendLayout();

            this.lblRotulo.AutoSize = true;
            this.lblRotulo.Location = new Point(13, 13);
            this.lblRotulo.Name = "lblRotulo";
            this.lblRotulo.Size = new Size(35, 13);
            this.lblRotulo.TabIndex = 0;

            this.txtMensagem.Location = new Point(13, 30);
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new Size(436, 20);
            this.txtMensagem.TabIndex = 1;


            this.btnOk.Location = new Point(373, 67);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;

            this.Click += (sender, e) =>
            {
                OnButtonOkClick(e);
            };


            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(461, 106);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtMensagem);
            this.Controls.Add(this.lblRotulo);
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Text = "Dialog";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }
    }
}
