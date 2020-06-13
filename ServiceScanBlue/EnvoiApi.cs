using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Net;


namespace ApiContact {
    public class EnvoiApi {

        static void Main() {
            tagStart:            
            // Mise en forme des noms avant envoi, synthaxe : nom1|nom2|etc...
            string scanFile = File.ReadAllText("/home/pi/Documents/ServiceScanBlue/blueDevices.txt");
            
            string codeFile = File.ReadAllText("/home/pi/Desktop/code.txt");
            string code = codeFile.Split("\n")[0];
            string idMag = codeFile.Split("\n")[1];
            List<string> splitted = scanFile.Split("\n").ToList();
            string names = "";
            int err = 0;
            splitted.RemoveAt(0);
            splitted.RemoveAt((splitted.Count) - 1);

            foreach (string line in splitted) {
					names += line.Split("\t")[2] + "|";
            }

			try{
				tagSend:
				HttpWebRequest req = WebRequest.CreateHttp("http://192.168.1.29:15403/bluetooth?code=" + code + "&phones=" + names + "&idMagasin=" + idMag);
				req.Headers.Clear();
				req.Method="GET";
				
				HttpWebResponse response = (HttpWebResponse)req.GetResponse();
				
				if (response.StatusCode != HttpStatusCode.OK){
					err++;
					if (err >=3)
						throw new Exception("3 erreurs de suite, arrët.");
					int statusCode = (int)response.StatusCode;
					if (statusCode == 420){
						Thread.Sleep(1000);
						goto tagSend;
						// 421 -> code faux | 422 -> id Magasin faux
					}else if (statusCode == 421 || statusCode == 422)
						goto tagStart;
				}
				response.Close();
			}catch(Exception e){
				Console.WriteLine("Exception : " + e.Message);
			}
		}
    }
}
