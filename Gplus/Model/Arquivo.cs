using Ionic.Zip;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gplus.Model
{
    internal class Arquivo
    {

        String mensagemErroArquivo = "";

        public string MensagemErroArquivo { get => mensagemErroArquivo; set => mensagemErroArquivo = value; }

        public Arquivo()
        {


        }


        public void SalvarArquivoCaminhoAlternativo()
        {
            FolderBrowserDialog saveCaminho = new FolderBrowserDialog();
            //informar o nome do filtro e a extenção que queremos salvar
            //saveFile.Filter = "Arquivo Texto |*.RAR";
            //mostrar a caixa de dialogo
            saveCaminho.ShowDialog();
            //this.caminhoAlternativoBackup = saveCaminho.SelectedPath;


        }

        public String CriarDiretorioPadraoBackup(string NomeBanco)
        {
            CultureInfo culturaLinguagem = new CultureInfo("Pt-br");
            string diaSemana = culturaLinguagem.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

            String CaminhoPadraoBackup = Application.StartupPath + "\\" + NomeBanco + "\\"+diaSemana.Replace("ç","c");

            if (!Directory.Exists(CaminhoPadraoBackup))
            {
                Directory.CreateDirectory(CaminhoPadraoBackup);

            }

            return CaminhoPadraoBackup;
        }

        public String CriarDiretorioPadraoLog()
        {
            String caminhopadraolog = Application.StartupPath + "\\" + "LOG";

            if (!Directory.Exists(caminhopadraolog))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\" + "LOG");
               
            }
            return caminhopadraolog;
        }


        public void VerificarArquivoBakouDifeDeletar(String NomeBanco, String CaminhoBackup, String TipoBackup)
        {

            if (File.Exists(NomeBanco + "_FULL.BAK"))
            {
                File.Delete(NomeBanco + "_FULL.BAK");

            }

            if (File.Exists(NomeBanco + "_DIF.BAK"))
            {
                File.Delete(NomeBanco + "_DIF.BAK");
            }
        }


        public void IdentificarFullouDifBackup(object banco)
        {
            Banco objBanco = (Banco)banco;

            var dataHoje = DateTime.Now;

            if (!File.Exists(objBanco.NomeBanco + "_FULL.BAK"))
            {
                if (!File.Exists(objBanco.CaminhoSalvarBackup + "\\" + objBanco.NomeBanco + "_FULL.zip"))
                {
                    objBanco.DataBackupFull = dataHoje;
                    objBanco.TipoBackup = "_FULL";
                    Console.WriteLine("Não tem Full em nenhum lugar, terei que fazer do zero Banco" + objBanco.NomeBanco);
                }
                else
                { 
                    if (dataHoje.Date > objBanco.DataBackupFull.Date)
                    {
                        objBanco.DataBackupFull = dataHoje;
                        objBanco.TipoBackup = "_FULL";
                        Console.WriteLine("Terei que fazer um FULL, por que o arquivo que existe é de data menor " + objBanco.NomeBanco);
                    }
                    else
                    {
                        objBanco.DataBackupDif = dataHoje;
                        objBanco.TipoBackup = "_DIF";
                        Console.WriteLine("Já existe um Full de Hoje, irei fazer um DIF" + objBanco.NomeBanco);

                    }

                }

            }

        }

        public async Task CompactarArquivoBackup(Object banco)
        {
            Banco objBanco = (Banco)banco;

            try
            {
                //instanciar a classe de funções
                ZipFile compactador = new ZipFile();

                //compactar o arquivo com o melhor propriedade do compactador
                compactador.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
                compactador.UseZip64WhenSaving = Zip64Option.AsNecessary;

                if(objBanco.TipoBackup == "_FULL")
                {
                    compactador.Comment = "BACKUP FULL do dia "+objBanco.DataBackupFull;

                }
                else
                {
                    compactador.Comment = "DIFF DO DIA "+objBanco.DataBackupDif+" REFERENTE AO FULL DO DIA "+ objBanco.DataBackupFull;

                }

                //vai pegar o banco no diretorio padrao da aplicacao;
                compactador.AddFile(objBanco.NomeBanco + objBanco.TipoBackup + ".BAK");


                compactador.Save(objBanco.CaminhoSalvarBackup + "\\" + objBanco.NomeBanco + objBanco.TipoBackup + ".zip");

                //finalizar o processo da Lib zip
                compactador.Dispose();
            }
            catch (Exception ex)
            {
               
                MensagemErroArquivo = "Erro ao compactar arquivo de Backup "+ex.Message;

            }

        }


    }

}




