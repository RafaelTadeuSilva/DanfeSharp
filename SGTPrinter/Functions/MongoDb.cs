using System;
using System.Collections.Generic;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using SGTPrinter.model;

namespace SGTPrinter.Functions
{
    public class MongoDb
    {
        public static IMongoClient client;
        public static IMongoDatabase database;

        public MongoDb()
        {

        }

        public static IList<T> List<T>(string tabela, FilterDefinition<T> filter)
        {
            var collection = database.GetCollection<T>(tabela);
            return collection.Find(filter).ToList();
            //return collection.Find(x => x.status == true && x.tipo == "3" && x.idLoja == "1").ToList();

            //IList<FilaImpressao> documents = 
            //var update = Builders<FilaImpressao>.Update.Set("status", false);
            //foreach (var item in documents)
            //{
            //    WriteRead.WriteFile(Path.Combine(destino, item.tipo + "-" + item.idVenda + ".txt"), item.buffer);
            //    collection.UpdateOne(x => x.Id == item.Id, update);
            //}

        }

        public static void Update<T>(string tabela, FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var collection = database.GetCollection<T>(tabela);

            collection.UpdateOne(filter, update);

        }
        public static void Connect()
        {
            var credential = MongoCredential.CreateCredential("SGTVendas", "rafael", "rafa@123");
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress("jrsilva2306.ddns-intelbras.com.br", 9899);
            settings.Credential = credential;
            client = new MongoClient(settings);
            database = client.GetDatabase("SGTVendas");
        }

        public static void createCollection()
        {
            //var colProd = database.GetCollection<Produto>("produto");
            //if (colProd == null)
            //    database.CreateCollection("produto");

            //Produto produto = new Produto();
            //produto.Descricao = "Test2e";

            //colProd.InsertOne(produto);
            //Console.Write("teste2");
        }
    }
}
