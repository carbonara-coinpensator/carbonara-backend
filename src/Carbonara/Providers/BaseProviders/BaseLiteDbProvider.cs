using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LiteDB;

namespace Carbonara.Providers
{
    public class BaseLiteDbProvider
    {
        protected string ConnectionString = String.Empty;

        public BaseLiteDbProvider()
        {
            this.ConnectionString = @"Db\Carbonara.db";
        }

        protected async Task<IEnumerable<T>> ReadCollectionFromDb<T>(string collectionName)
        {
            using (var db = new LiteDatabase(this.ConnectionString))
            {
                var collection = db.GetCollection<T>(collectionName);
                return await Task.FromResult(collection.FindAll());
            }
        }
    }
}