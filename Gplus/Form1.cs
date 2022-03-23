using Gplus.Model;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Gplus
{
    public partial class Form1 : Form
    {
        ConfigBanco formularioConfigBanco;

        Banco banco1 = new Banco();
        Cliente cliente1 = new Cliente();
        Banco banco2 = new Banco();
        Cliente cliente2 = new Cliente();


        public Form1()
        {
            InitializeComponent();
          
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("Já existe um aplicativo aberto, esse será fechado");
                this.Close();
            }
        }

    
        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
          

        }

        private void btnBanco1_Click(object sender, EventArgs e)
        {
            banco1.InstanciaBanco = "LOCALHOST";
            banco1.NomeBanco = "INOVAFARMA_DROGARIADAMIAO";
            banco1.LoginBanco = "sa";
            banco1.SenhaBanco = "adrvsc";
            banco1.HoraBackup = 2;


            cliente1.EmailUpload = "aaaa";
            cliente1.SenhaUpload = "abcd";
            cliente1.ServicoUpload = "Mega";


            formularioConfigBanco = new ConfigBanco(cliente1, banco1);
            formularioConfigBanco.ShowDialog();

        }

        private void btnBanco2_Click(object sender, EventArgs e)
        {
            banco2.InstanciaBanco = "LOCALHOST";
            banco2.NomeBanco = "INOVAFARMA_SINCRONISMOPS_FILIAL7";
            banco2.LoginBanco = "sa";
            banco2.SenhaBanco = "adrvsc";
            banco2.HoraBackup = 3;


            cliente2.EmailUpload = "aaaa";
            cliente2.SenhaUpload = "abcd";
            cliente2.ServicoUpload = "Mega";


            formularioConfigBanco = new ConfigBanco(cliente2, banco2);
            formularioConfigBanco.ShowDialog();

        }

        private void btnBanco3_Click(object sender, EventArgs e)
        {
         
        }

        private void btnBanco4_Click(object sender, EventArgs e)
        {
          
        }

    }
}
