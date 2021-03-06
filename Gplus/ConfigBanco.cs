using FluentScheduler;
using Gplus.Model;
using Gplus.Tarefas;
using System.Windows.Forms;

namespace Gplus
{
    public partial class ConfigBanco : Form
    {
        Cliente objCliente;
        Banco objBanco;

        public ConfigBanco(object cliente, object banco)
        { 
            objCliente = (Cliente)cliente;
            objBanco = (Banco)banco;
           

            InitializeComponent();
        }

        private void ConfigBanco_Load(object sender, System.EventArgs e)
        {

            CarregarDadosCamposFormulario();

        }


        private async void btnSalvarConfigBanco_Click(object sender, System.EventArgs e)
        {

            if (ValidarCamposPreenchidos())
            {
                
                //Job para relaizar backup
                JobManager.Initialize(new AgendadordeTarefas(objBanco, objCliente,objBanco.HoraBackup));

                this.Close();
                
                
            }
            else
            {
                MessageBox.Show("Existe campos não preenchidos, por favor verificar");
            }


        }


        public bool ValidarCamposPreenchidos()
        {

            if (txtInstancia.Text == "" || txtLogin.Text == "" || txtSenha.Text == "" || txtEmail.Text == "" || txtEmailSenha.Text == "" || numTempoBackup.Value == 0 && (radioMega.Checked == false || radioGoogleDrive.Checked ==false) )
            {
                return false;
            }
            else
            {
                objBanco.InstanciaBanco = txtInstancia.Text;
                objBanco.LoginBanco = txtLogin.Text;
                objBanco.SenhaBanco = txtSenha.Text;
                objBanco.NomeBanco = txtNomeBanco.Text;
                objBanco.HoraBackup = (int)numTempoBackup.Value;

                objCliente.EmailUpload = txtEmail.Text;
                objCliente.SenhaUpload = txtEmailSenha.Text;

                if (radioMega.Checked)
                {
                    objCliente.ServicoUpload = radioMega.Text;
                }
                else
                {
                    objCliente.ServicoUpload = radioGoogleDrive.Text;
                }
                
                return true;
            }

         
        }

        public void CarregarDadosCamposFormulario()
        {
            if(objBanco.InstanciaBanco != null)
            {
                txtInstancia.Text = objBanco.InstanciaBanco;
                txtLogin.Text = objBanco.LoginBanco;
                txtSenha.Text = objBanco.SenhaBanco;
                txtNomeBanco.Text = objBanco.NomeBanco;
                numTempoBackup.Value = objBanco.HoraBackup;

                txtEmail.Text = objCliente.EmailUpload;
                txtEmailSenha.Text = objCliente.SenhaUpload;

                if (objCliente.ServicoUpload == "Mega")
                {
                    radioMega.Checked = true;
                  
                }
                else
                {
                   radioGoogleDrive.Checked = true;
      
                }
            }
            
            
        }

        private void guna2Button1_Click(object sender, System.EventArgs e)
        {
         

        }
    }
}
