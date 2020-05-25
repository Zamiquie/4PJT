using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model.ModelService
{
    public class StockAlert
    {
       
        public string item { get; set; }
        public string _id { get; set; }
        public string Designation { get; set; }
        public bool isRupture { get; set; }
        public BsonArray isNull { get; set; }
        public int TotalStock { get; set; }
        public int StAlert { get; set; }
    }
}
