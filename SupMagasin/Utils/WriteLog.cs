using SupMagasin.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Utils
{
    //Classe de generation de Log Perso
    public class WriteLog
    {
        public string Path { get; set; } // Dossier du Log
        public string File { get; set; } // Fichier 

        public WriteLog(TypeLog log)
        {
            Path = Directory.GetCurrentDirectory() + "../../Log/";

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            switch (log) // verification par un enum
            {
                case TypeLog.MangoDb:
                    File = "Log_mango_";
                    break;
                case TypeLog.AspNet:
                    File = "Log_asp_";
                    break;
                case TypeLog.Other:
                    File = "Log_other_";
                    break;
            }
            File = File + DateTime.Now.ToString("dd/MM/yyyy").Replace('/','_') + ".log"; // ajout du nom plus de la date
        }

        public void WriteFile(string message)
        {
            Console.WriteLine("Error : {0}", message);
            using (StreamWriter file = new StreamWriter(Path + File,true))
            {
                file.WriteLine(DateTime.Now.ToString().Replace('/','_') + " : " + message); // ecriture du message
            }
        }
    }
}
