using MongoDB.Bson;
using MongoDB.Driver;
using RaftLabs.Enterprise.Utility.Enums;
using System.Linq.Expressions;
using System.Security.Authentication;

namespace RaftLabs.Enterprise.Database.DbProviders
{
    internal class MongoDb<T> : BaseDatabase<T> where T : class
    {
        private readonly MongoClient mongoClient;
        private readonly MongoClientSettings settings;
        private readonly IMongoDatabase mongoDatabase;
        private readonly IMongoCollection<T> mongoCollection;
        private readonly MongoUrl mongoUrl;

        public MongoDb(string connectionString)
        {
            connectionString = @"connectionString";
            settings = MongoClientSettings.FromUrl(new(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            mongoClient = new(settings);
            mongoDatabase = mongoClient.GetDatabase("ace-plus");
            mongoCollection = mongoDatabase.GetCollection<T>(typeof(T).Name + "s");
        }

        public override bool Create(T type)
        {
            mongoCollection.InsertOne(type);
            return true;
        }

        public override bool Create(List<T> types)
        {
            mongoCollection.InsertMany(types);
            return true;
        }

        public override async Task<bool> CreateAsync(T type)
        {
            await mongoCollection.InsertOneAsync(type);
            return true;
        }

        public override async Task<bool> CreateAsync(List<T> types)
        {
            await mongoCollection.InsertManyAsync(types);
            return true;
        }

        public override bool Delete(object Id)
        {
            if (!ObjectId.TryParse(Id.ToString(), out ObjectId id))
            {
                throw new Exception("Object Id not correct");
            }
            FilterDefinition<T> filterId = Builders<T>.Filter.Eq("_id", id);
            return mongoCollection.FindOneAndDelete(filterId) is not null;
        }

        public override async Task<bool> DeleteAsync(object Id)
        {
            if (!ObjectId.TryParse(Id.ToString(), out ObjectId id))
            {
                throw new Exception("Object Id not correct");
            }
            FilterDefinition<T> filterId = Builders<T>.Filter.Eq("_id", id);
            return await mongoCollection.FindOneAndDeleteAsync(filterId) is not null;
        }

        public override T Get(object Id)
        {
            if (!ObjectId.TryParse(Id.ToString(), out ObjectId id))
            {
                throw new Exception("Object Id not correct");
            }
            FilterDefinition<T> filterId = Builders<T>.Filter.Eq("_id", id);
            return mongoCollection.Find(filterId).FirstOrDefault();
        }

        public override async Task<T> GetAsync(object Id)
        {
            if (!ObjectId.TryParse(Id.ToString(), out ObjectId id))
            {
                throw new Exception("Object Id not correct");
            }
            FilterDefinition<T> filterId = Builders<T>.Filter.Eq("_id", id);
            return await mongoCollection.Find(filterId).FirstOrDefaultAsync();
        }

        public override List<T> Get()
        {
            return mongoCollection.Find(FilterDefinition<T>.Empty).ToList();
        }

        public override async Task<List<T>> GetAsync()
        {
            return await mongoCollection.Find(FilterDefinition<T>.Empty).ToListAsync();
        }

        public override List<T> Get(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public override List<T> Get(Expression<Func<T, bool>> filter)
        {
            return mongoCollection.Find(filter).ToList();
        }

        public override bool Update(T type, object Id)
        {
            if (!ObjectId.TryParse(Id.ToString(), out ObjectId id))
            {
                throw new Exception("Object Id not correct");
            }
            FilterDefinition<T> filterId = Builders<T>.Filter.Eq("_id", id);
            return mongoCollection.FindOneAndReplace(filterId, type) is not null;
        }

        public override async Task<bool> UpdateAsync(T type, object Id)
        {
            if (!ObjectId.TryParse(Id.ToString(), out ObjectId id))
            {
                throw new Exception("Object Id not correct");
            }
            FilterDefinition<T> filterId = Builders<T>.Filter.Eq("_id", id);
            return await mongoCollection.FindOneAndReplaceAsync(filterId, type) is not null;
        }

        public override int Count(Expression<Func<T, bool>> filter)
        {
            return (int)mongoCollection.CountDocuments(filter);
        }

        public override async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            return (int)await mongoCollection.CountDocumentsAsync(filter);
        }

        public override List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, int>> orderby, OrderType orderType, int skip, int take)
        {
            return orderType switch
            {
                OrderType.Ascending => mongoCollection.Find(filter).Sort(Builders<T>
                                                      .Sort.Ascending(filter.Body.ToString().Split('.')[^1]))
                                                      .Skip(skip).Limit(take).ToList(),
                OrderType.Descending => mongoCollection.Find(filter).Sort(Builders<T>
                                                      .Sort.Descending(filter.Body.ToString().Split('.')[^1]))
                                                      .Skip(skip).Limit(take).ToList(),
                _ => mongoCollection.Find(filter).Skip(skip).Limit(take).ToList(),
            };
        }

        public override List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, DateTime>> orderby, OrderType orderType, int skip, int take)
        {
            return orderType switch
            {
                OrderType.Ascending => mongoCollection.Find(filter).Sort(Builders<T>
                                                      .Sort.Ascending(filter.Body.ToString().Split('.')[^1]))
                                                      .Skip(skip).Limit(take).ToList(),
                OrderType.Descending => mongoCollection.Find(filter).Sort(Builders<T>
                                                      .Sort.Descending(filter.Body.ToString().Split('.')[^1]))
                                                      .Skip(skip).Limit(take).ToList(),
                _ => mongoCollection.Find(filter).Skip(skip).Limit(take).ToList(),
            };
        }

        public override List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, string>> orderby, OrderType orderType, int skip, int take)
        {
            return orderType switch
            {
                OrderType.Ascending => mongoCollection.Find(filter).Sort(Builders<T>
                                                      .Sort.Ascending(filter.Body.ToString().Split('.')[^1]))
                                                      .Skip(skip).Limit(take).ToList(),
                OrderType.Descending => mongoCollection.Find(filter).Sort(Builders<T>
                                                      .Sort.Descending(filter.Body.ToString().Split('.')[^1]))
                                                      .Skip(skip).Limit(take).ToList(),
                _ => mongoCollection.Find(filter).Skip(skip).Limit(take).ToList(),
            };
        }
    }
}
