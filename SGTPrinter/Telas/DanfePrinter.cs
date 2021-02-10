using System;
using System.IO;
using DanfeSharp;
using DanfeSharp.Modelo;

namespace SGTPrinter.Telas
{
    public class DanfePrinter
    {
        public DanfePrinter()
        {
        }

        public static void print(string arquivo, string destino)
        {
            //appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var modelo = DanfeViewModelCreator.CriarDeArquivoXml(arquivo);
            //Danfe danfe = new Danfe(modelo);
            using (var danfe = new Danfe(modelo))
            {
                danfe.Gerar();
                danfe.Salvar(Path.Combine(destino, danfe.ViewModel.ChaveAcesso + ".pdf"));
            }
            //DanfePagina danfePagina = new DanfePagina(danfe);

        }
    }
}
