using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace myApp
{
    class Program
    {
        private static string conString = "mongodb://localhost:27017";
        private static string database = "teste";
        private static string collectionName = "lst";
        static void Main(string[] args)
        {
            getItems(args).Wait();
            Console.ReadLine();
        }

        static async Task getItems(string[] args)
        {
            //Console.WriteLine("---------- Insert ----------");

            //var document = new BsonDocument
            //{
            //    {"_id",5} 
            //};

            //document.Add("adicionado", 17);
            //document["Level"] = "AHOTAVIO"; 

            //var arr = new BsonArray();
            //arr.Add(new BsonDocument("Jones", "Carvalho"));

            //document.Add("Hired", arr);

            //var collection = returnDatabaseCollection();

            //collection.InsertOneAsync(document);


            Console.WriteLine("---------- Response Methods MongoDB ----------");
            var collection = returnDatabaseCollection();

            Console.WriteLine("-- Method 1 --");
            using (var cursor = await collection.Find(new BsonDocument()).ToCursorAsync())
                while (await cursor.MoveNextAsync())
                    foreach (var doc in cursor.Current)
                        Console.WriteLine(doc);

            Console.WriteLine("-- Method 2 --");
            var list = await collection.Find(new BsonDocument()).ToListAsync();
            foreach (var dox in list)
                Console.WriteLine(dox);

            Console.WriteLine("--    Method 3 --");
            await collection.Find(new BsonDocument()).ForEachAsync(X => Console.WriteLine(X));
        }

        private static IMongoCollection<BsonDocument> returnDatabaseCollection()
        {
            var Client = new MongoClient(conString);
            var DB = Client.GetDatabase(database);
            var collection = DB.GetCollection<BsonDocument>(collectionName);
            return collection;
        }
    }
}

