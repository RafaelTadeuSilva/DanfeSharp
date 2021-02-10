using System;
using MongoDB.Bson.Serialization.Attributes;

namespace SGTPrinter.model
{
    public class FilaImpressao
    {
        public const string tabela = "filaImpressao";

        [BsonElement("_id")]
        public String Id { get; set; }

        [BsonElement("idLoja")]
        public String idLoja { get; set; }

        [BsonElement("idVenda")]
        public String idVenda { get; set; }

        [BsonElement("tipo")]
        public String tipo { get; set; }

        [BsonElement("buffer")]
        public String buffer { get; set; }

        [BsonElement("status")]
        public bool status { get; set; }

        [BsonElement("vias")]
        public int vias { get; set; }


    }
}
