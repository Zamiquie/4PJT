using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestMongo
{
    class Model
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("Type")]
        public string Type { get; set; }
        [BsonElement("Price")]
        public float Price { get; set; }
        [BsonElement("list_Adress")]
        public List<Models> Adress { get; set; }


        public Model(string id,string name,string firstname,float price,List<Models> adresses)
        {
            ID = id;
            Name = name;
            FirstName = firstname;
            Type = "model";
            Price = price;
            Adress = adresses;
        }
    }

}
