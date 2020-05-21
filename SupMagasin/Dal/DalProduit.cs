using MongoDB.Bson;
using MongoDB.Driver;
using SupMagasin.Dal;
using SupMagasin.Model;
using SupMagasin.Model.ProductModel;
using SupMagasin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Dal
{
    public class DalProduit : Dal<Produit>
    {

        public DalProduit() : base("Product"){}


        #region Insert
        //Insert d"un nouveau Produit
        public async Task<string> AddProduitAsync(Produit newProduit)
        {
            newProduit.ID = GenerateId(newProduit);
            return await AddElement(newProduit);
        }

        //Insert D'une Multitude produit
        public async Task<string> AddMultiProduit(List<Produit> produits)
        {
            //pour chaque on genere un produit
            foreach(Produit prod in produits) { prod.ID = GenerateId(prod); }
            return await AddListOfElement(produits);
        }

        #endregion

        #region Query

        public async Task<string> GetAllProduit()
        {
            return await QueryAllElement();
        }

        public async Task<string> GetProduitByID(string id)
        {
            var list = await QueryElementById();
            return list.Where(mag => mag.ID == id).ToJson();
        }

        public async Task<string> GetProduitByName(string designation)
        {
            var result = await Collection.FindAsync(pro => pro.Designation == designation);
            return result.First().ToJson(); 
        }

        //Requete des produits ayant leur stock =< au seuil d'alerte
       /* public async Task<string> GetProduitToOrder(int stockAlert)
        {
            var Produit = 
            

        }*/


        public async Task<string> GetCommentaryById(string id)
        {
            var result = await Collection.FindAsync(pro => pro.ID == id);
            return result.FirstOrDefault().Commentaire.ToJson();
        }

        public async Task<string> GetSupplierByIdProduct(string id)
        {
            var result = await Collection.FindAsync(pro => pro.ID == id);
            return result.FirstOrDefault().Fournisseur.ToJson();
        }

        public async Task<string> GetDeliveryByIdProduct(string id)
        {
            var result = await Collection.FindAsync(pro => pro.ID == id);
            return result.FirstOrDefault().Lots.ToJson();
        }

        #endregion

        #region Update

        public async Task<string> UpdateProduit(Produit currentProduit)
        {
            return await UdpateElement(currentProduit.ID.ToString(), currentProduit);

        }

        #endregion

        #region Delete
        public async Task<string> RemoveProduit(string id)
        {
            return await DeleteEntry(id);
        }
        public async Task<string> RemoveLotProduit(List<Produit> ProduitsToDelete)
        {
            return await DeleteMultielement(ProduitsToDelete);
        }
        #endregion


        //Region Gestion des Enfants

        //Ajout du fournisseur
        public async Task<string> AddFournisseur(string id, Fournisseur fournisseur)
        {
            //on filtre et on met
            var filter = Builders<Produit>.Filter.Eq("_id", id);
            var update = Builders<Produit>.Update.Set(pro => pro.Fournisseur, fournisseur);

            try
            {
                await Collection.UpdateOneAsync(filter, update);
                return true.ToJson();
            }
            catch (MongoException e)
            {

                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return false.ToJson();

            }
        }
         //Ajout des Commentaires
        public async Task<string> AddCommentaire(string id, Commentaire coment)
        {
            //on filtre et on met l'update
            var filter = Builders<Produit>.Filter.Eq("_id", id);
            var update = Builders<Produit>.Update.Push<Commentaire>(enf => enf.Commentaire, coment);

            try
            {
                await Collection.UpdateManyAsync(filter, update);
                return true.ToJson();
            }
            catch (MongoException e)
            {

                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return false.ToJson();

            }
        }
        //ajout des Lots
        public async Task<string> AddLot(string id, LotsModel lot)
        {
            //on filtre et on met l'update
            var filter = Builders<Produit>.Filter.Eq("_id", id);
            var update = Builders<Produit>.Update.Push<LotsModel>(enf => enf.Lots, lot);

            try
            {
                await Collection.UpdateManyAsync(filter, update);
                return true.ToJson();
            }
            catch (MongoException e)
            {

                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return false.ToJson();

            }
        }
        
        #region Private 
        //Generation de Id
        private string GenerateId(Produit product)
        {
            return product.Designation.Substring(0, 3).Replace(' ', 'X') + product.Weight.ToString().Substring(0,1) + new Random().Next(0, 684654564).ToString(); ;
        } 
        #endregion

    }
}
