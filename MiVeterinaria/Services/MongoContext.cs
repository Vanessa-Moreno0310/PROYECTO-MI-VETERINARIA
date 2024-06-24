using MiVeterinaria.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MiVeterinaria.Services
{
    public class MongoContext
    {
        public MongoContext() //constructor
        {

        }   

        public static IMongoDatabase Database()
        {

            string conectionString = ConfigurationManager.AppSettings["MongoConectionstring"];
            var mongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];
            var _client = new MongoClient(conectionString);
            var _database = _client.GetDatabase(mongoDatabaseName);

            return _database;
        }
    }    
}