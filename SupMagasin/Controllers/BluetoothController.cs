using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using SupMagasin.Dal;
using SupMagasin.Model;
using SupMagasin.Utils;

namespace SupMagasin.Controllers
{
    [Route("bluetooth")]
    [ApiController]
    [AllowAnonymous]
    public class BluetoothController : ControllerBase
    {
        DalCustomer _dal { get; set; }
        IConfiguration _configuration { get; set; }

        public BluetoothController(IConfiguration configuration)
        {
            _dal = new DalCustomer();
            _configuration = configuration;
        }
        #region Get

        [HttpGet]
        public async Task<IActionResult> ControlBluetooth(string code, string phones,string idMagasin)
        {
            //si different de la clé on renvois un 421. Histoire de brouiller les pistes , si Pas de renseignement IdMagasin on rejette
            if (code == null || code != _configuration["Borne:key"]) return StatusCode(421,"Key not correspond.");
            if (idMagasin == null || idMagasin == String.Empty) return StatusCode(422, "IdMagasin is empty.");
            if (phones == null) return StatusCode(420, "Phones attibuts is empty");
            //on recherche les Id des personnes présentes
            Dictionary<string, string> phoneCustDict = new Dictionary<string, string>();
            phoneCustDict.Add("IdMagasin", idMagasin);
            foreach(var phoneMac in phones.Split('|'))
            {
                var custID = _dal.GetCustomerByMacTelephone(phoneMac).Result;
                if (custID == null)
                {
                    phoneCustDict.Add(phoneMac, null);
                }
                else { 
                    phoneCustDict.Add(phoneMac, custID.Id); 
                }
            }
            //on Log les connextions Bluethooths
            new WriteLog(TypeLog.Bluethoo).WriteFile(string.Join(",",phoneCustDict)+" totalDevice:["+phoneCustDict.Count+"]" );

            //on enregistrer les nouvelles collections
            if(System.IO.File.Exists(Directory.GetCurrentDirectory()+ @"/temps/bluCust.txt")) { System.IO.File.Delete(Directory.GetCurrentDirectory() + @"/temps/bluCust.txt"); }
            using (var streamWriter = new StreamWriter(new FileStream(Directory.GetCurrentDirectory() + @"/temps/bluCust.txt", FileMode.OpenOrCreate, FileAccess.Write), Encoding.UTF8))
            {
                foreach(var telCu in phoneCustDict)
                {
                   await streamWriter.WriteLineAsync(telCu.ToString());
                }
                streamWriter.Close();
            }
            return Ok();
        }
        [Route("{macheck}")]
        [HttpGet]
        public async Task<IActionResult> IsPresentInShop(string macheck)
        {
            Dictionary<string, string> MacIdCustomer = new Dictionary<string, string>();
            using (var streamRide = new StreamReader(new FileStream(Directory.GetCurrentDirectory() + @"/temps/bluCust.txt", FileMode.OpenOrCreate, FileAccess.Read), Encoding.UTF8))
            {
                var file = streamRide.ReadToEnd();
                file = file.Replace("\r",String.Empty);
                file = file.Replace("[", String.Empty);
                file = file.Replace("]", String.Empty);
                var listMac = new List<string>(file.Split('\n'));
                listMac.RemoveAt(file.Split(',').Length-1);

                MacIdCustomer = listMac.ToDictionary(x => x.Split(',')[0], x => x.Split(',')[1]);
                                    
            }
            //on regarde si la clé est présente
            if (MacIdCustomer.ContainsKey(macheck))
            {
                //possibilité de creer une promo ??

                return Ok(new { idMagasin =MacIdCustomer["idMagasin"] });
            }
            else
            {
                return StatusCode(425,"MAC Adress not found "); 
            }
        }
        #endregion

    }
}