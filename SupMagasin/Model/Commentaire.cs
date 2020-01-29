﻿using System;

namespace SupMagasin.Model
{
    public class Commentaire
    {
        public int ID { get; set; }
        public int ID_Produit { get; set; }
        public int IDClient { get; set; }
        public string Comment { get; set; } // text du commantaire max 120 cara
        public int Note { get; set; } // maximun 10
        public DateTime fdate { get; set; } // date de creation

    }
}