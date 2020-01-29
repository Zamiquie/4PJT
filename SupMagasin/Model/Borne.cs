using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class Borne
    {
        public int ID { get; set; }
        public string Position { get; set; }
        public EtatBorne EtatBorne { get; set; }
    }
}
