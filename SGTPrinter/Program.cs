using System;
using System.IO;
using System.Threading;
using DanfeSharp;
using DanfeSharp.Modelo;
using SGTPrinter.Functions;
using SGTPrinter.model;
using SGTPrinter.Telas;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SGTPrinter
{
    class MainClass
    {
        static string pathDanfe;
        static string pathApp;
        public static ParametrosIni parametrosIni;
        public static string ultPedido = "";
        public static string erroMessage = "";

        static readonly CancellationTokenSource Cts = new CancellationTokenSource(); 
         static async Task Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            pathApp = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                var parametros = Tools.ReadFile(pathApp, "parametros.ini");
                parametrosIni = JsonConvert.DeserializeObject<ParametrosIni>(parametros);
                pathDanfe = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                watchNFE();
                //watchSAT();
                if (parametrosIni.tipoTerminal == "ImpressaoEntrega")
                    if (String.IsNullOrEmpty(parametrosIni.impEntrega))
                        Console.WriteLine("Impressora de Entrega não Configurada");
                    else
                        FilaPrinter.Buscar(parametrosIni.impEntrega, "1;2", "3");

                if (parametrosIni.tipoTerminal == "ImpressaoRetira")
                    if (String.IsNullOrEmpty(parametrosIni.impRetira))
                        Console.WriteLine("Impressora de Retira não Configurada");
                    else
                        FilaPrinter.Buscar(parametrosIni.impRetira, parametrosIni.idLoja, "1;2;4;5");

                if (parametrosIni.tipoTerminal == "Teste")
                    if (String.IsNullOrEmpty(parametrosIni.impRetira))
                        Console.WriteLine("Impressora de Retira não Configurada");
                    else
                        FilaPrinter.Buscar(parametrosIni.impRetira, parametrosIni.idLoja, "6");

                //FilaPrinter.Buscar(Path.Combine(pathNFE, "SGT", "Impressora"), "1;2", "1;3");
                //Thread.Sleep(Timeout.Infinite);
                await Task.Delay(Timeout.Infinite, Cts.Token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void watchNFE()
        {
            try
            {
                var pasta = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0');
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = Path.Combine(parametrosIni.uninfe, "Enviado","Autorizados", pasta);
                watcher.NotifyFilter = NotifyFilters.CreationTime;
                watcher.Filter = "*-procNFe.xml";
                watcher.Changed += new FileSystemEventHandler(OnChangedNFE);
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao busca a pasta nfe/Envio");
            }

        }

        //private static void watchSAT()
        //{
        //    try
        //    {
        //        var pasta = DateTime.Now.Year.ToString() + DateTime.Now.Day.ToString().PadLeft(2, '0');
        //        FileSystemWatcher watcher = new FileSystemWatcher();
        //        watcher.Path = Path.Combine(parametrosIni.uninfe, "sat", "Enviado/Autorizados/", pasta);
        //        watcher.NotifyFilter = NotifyFilters.CreationTime;
        //        watcher.Filter = "*-sat.xml";
        //        watcher.Changed += new FileSystemEventHandler(OnChangedSAT);
        //        watcher.EnableRaisingEvents = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Erro ao busca a pasta sat/Envio");
        //    }

        //}

        private static void OnChangedNFE(object sender, FileSystemEventArgs e)
        {
            DanfePrinter.print(e.FullPath, Path.Combine(pathDanfe, "SGT", "Danfe"));
            File.Move(e.FullPath, Path.Combine(pathDanfe, "SGT", "Danfe", e.Name));
        }

        //private static void OnChangedSAT(object sender, FileSystemEventArgs e)
        //{
        //    //DanfePrinter.print(e.FullPath, Path.Combine(pathDanfe, "SGT", "Danfe"));
        //    File.Move(e.FullPath, e.FullPath.Replace("-sat", "-cupom"));// Path.Combine(pathDanfe, "SGT", "Danfe", e.Name));
        //}
    }
}
