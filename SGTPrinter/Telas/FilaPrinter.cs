using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using SGTPrinter.Functions;
using SGTPrinter.model;

namespace SGTPrinter.Telas
{
    public class FilaPrinter : IDisposable
    {
        static Timer timer;
        public FilaPrinter()
        {

        }

        public static void Buscar(string destino, string loja, string tipo)
        {
            MongoDb.Connect();
            var builders = Builders<FilaImpressao>.Filter;
            var filter = builders.In(x => x.idLoja, loja.Split(';')) & builders.Eq(x => x.status, true) & builders.In(x => x.tipo, tipo.Split(';'));
            timer = new System.Threading.Timer(
                e => Gravar(MongoDb.List<FilaImpressao>(FilaImpressao.tabela, filter), destino),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(10));
        }

        static void Gravar(IList<FilaImpressao> documents, string destino)
        {
            Console.Clear();

            Console.WriteLine(MainClass.ultPedido);
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(MainClass.erroMessage);

            if (documents != null && documents.Count > 0)
            {
                var update = Builders<FilaImpressao>.Update.Set("status", false);
                foreach (var item in documents)
                {
                    Tools.WriteFile(Path.Combine(destino, item.tipo + "-" + item.idVenda + ".txt"), item.buffer);
                    MainClass.ultPedido = item.buffer;
                    MongoDb.Update<FilaImpressao>(FilaImpressao.tabela, "{_id:'" + item.Id + "'}", update);
                }
            }
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}
