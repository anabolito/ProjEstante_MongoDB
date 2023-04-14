using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;
using System.Diagnostics.Metrics;
using System.Xml.Linq;


namespace ProjEstante_MongoDB
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Isbn { get; set; }


        [BsonElement("titulo")]
        public string Title { get; set; }


        [BsonElement("editora")]
        public string Publisher { get; set; }


        [BsonElement("anoPublicacao")]
        public int YearOfPublication { get; set; }


        [BsonElement("autores")]
        public string[] Authors { get; set; }


        public Book(string t, string p, int y, string[] a)
        {
            Title = t;
            Publisher = p;
            YearOfPublication = y;
            Authors = a;
        }

        

        public override string ToString()
        {
            return $"Título:{Title} \nEditora: {Publisher} " +
                $"\nAno de publicãção:{YearOfPublication} \nAutores: {Authors}";
        }




    }
}
