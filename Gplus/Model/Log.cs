using System;
using System.IO;
namespace Gplus.Model
{
    internal class Log
    {


        public Log()
        {
        }


        public void ErroLog(String nomeBaco, String erroLog)
        {

            Arquivo arquivo = new Arquivo();

            var caminhoarquivotxtLog = arquivo.CriarDiretorioPadraoLog() + "\\" + nomeBaco + "_log.txt";

            //o metodo ( appendtext é para quando existe o arquvio e será adicionado mais informacao)
            using (StreamWriter gravadorTexto = File.AppendText(caminhoarquivotxtLog))
            {
                gravadorTexto.Write("\r\nErros Log : ");
                gravadorTexto.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                gravadorTexto.WriteLine("  :");
                gravadorTexto.WriteLine($"  :{erroLog}");
                gravadorTexto.WriteLine("-------------------------------");

            }


        }
  
    }
    
}
