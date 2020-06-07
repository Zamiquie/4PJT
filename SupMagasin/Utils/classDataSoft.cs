using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Utils
{
    public class classDataSoft
    {

        public class Rootobject
        {
            public int nhits { get; set; }
            public Parameters parameters { get; set; }
            public Record[] records { get; set; }
            public Facet_Groups[] facet_groups { get; set; }
        }

        public class Parameters
        {
            public string[] dataset { get; set; }
            public string timezone { get; set; }
            public int rows { get; set; }
            public string format { get; set; }
            public string[] facet { get; set; }
        }

        public class Record
        {
            public string datasetid { get; set; }
            public string recordid { get; set; }
            public Fields fields { get; set; }
            public DateTime record_timestamp { get; set; }
        }

        public class Fields
        {
            public string code_epci { get; set; }
            public string type_fonction { get; set; }
            public string nom_dept { get; set; }
            public string nom_epci { get; set; }
            public string prenom_de_l_elu { get; set; }
            public string nom_de_l_elu { get; set; }
            public string date_de_debut_du_mandat { get; set; }
            public string libelle_region { get; set; }
            public string nationalite_de_l_elu { get; set; }
            public string libelle_commune { get; set; }
            public string libelle_de_la_profession { get; set; }
            public string code_sexe { get; set; }
            public string date_de_naissance { get; set; }
            public string code_commune { get; set; }
            public string code_departement { get; set; }
            public int code_profession { get; set; }
            public string libelle_de_fonction { get; set; }
            public string date_de_debut_de_la_fonction { get; set; }
        }

        public class Facet_Groups
        {
            public Facet[] facets { get; set; }
            public string name { get; set; }
        }

        public class Facet
        {
            public int count { get; set; }
            public string path { get; set; }
            public string state { get; set; }
            public string name { get; set; }
        }

    }
}
