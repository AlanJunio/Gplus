using Gplus.Dao;
using Gplus.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gplus.Controler
{

    public class ContBancodeDados
    {

        Banco objBanco;
        Log log;
        public ContBancodeDados(Object banco,Object cliente)
        {
            objBanco = (Banco)banco;


          Task.WaitAny(realizarBackupbancodeDados());
          
        }


        public async Task  realizarBackupbancodeDados()
        {
           

            Arquivo objArquivo = new Arquivo();

           //Criar Diretorio para salvar o arquivo;
            objBanco.CaminhoSalvarBackup = objArquivo.CriarDiretorioPadraoBackup(objBanco.NomeBanco);

            //Identificar existencia dos arquivos na raiz do aplicativo
            objArquivo.IdentificarFullouDifBackup(objBanco);


            //conexao com SQL
            ConexaoSql conexaosql = new ConexaoSql(objBanco.InstanciaBanco, objBanco.LoginBanco, objBanco.NomeBanco, objBanco.SenhaBanco);

                ////Fazer o backup, esperar terminar essa função usando await
                await objBanco.FazerBackupBancodeDados(conexaosql, Application.StartupPath, objBanco.TipoBackup);

                if (objBanco.MensagemRetornoErro != "")
                {
                    Log objLog = new Log();
                    objLog.ErroLog(objBanco.NomeBanco, objBanco.MensagemRetornoErro);

                }
                else
                {
                    Console.WriteLine("Backup Realizado com sucesso do banco  " + objBanco.NomeBanco);
                    //Compactar o arquivo de backup
                    await objArquivo.CompactarArquivoBackup(objBanco);

                if (objArquivo.MensagemErroArquivo != "")
                {
                    log = new Log();
                    log.ErroLog(objBanco.NomeBanco, objArquivo.MensagemErroArquivo);
                }
                else
                {
                    Console.WriteLine("Backup Compactado  " + objBanco.NomeBanco);

                }

                //Verificar se tem o arquivo full ou diff, se tiver deletar para sobrar espaço em HD se não tier não faz nada.

                objArquivo.VerificarArquivoBakouDifeDeletar(objBanco.NomeBanco, objBanco.CaminhoSalvarBackup, objBanco.TipoBackup);
            }

        }
    }
}
