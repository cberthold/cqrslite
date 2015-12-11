﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class MongoReadRepository<TDocument> : IReadRepository<TDocument>
        where TDocument : IEntity
    {

        IMongoDatabase database;
        string tableName = typeof(TDocument).Name;

        public MongoReadRepository()
        {
            // get the database - normally we'd inject this but lets KISS for now
            // http://mongodb.github.io/mongo-csharp-driver/2.1/getting_started/quick_tour/
            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Customer");
            this.database = database;

        }

        protected IMongoCollection<TDocument> GetMongoTable()
        {
            return database.GetCollection<TDocument>(tableName);
        }

        public IQueryable<TDocument> GetCollection()
        {
            var queryable = GetMongoTable().AsQueryable();
            return queryable;
        }

        public void Insert(TDocument document)
        {
            var table = GetMongoTable();

            var task = table.InsertOneAsync(document);
            task.Wait();
        }

        public void Update(TDocument document)
        {
            var table = GetMongoTable();
            var task = table.ReplaceOneAsync(GetIdExpression(document), document);
            task.Wait();
        }

        public Expression<Func<TDocument, bool>> GetIdExpression(IEntity entity)
        {
            var id = entity.Id;

            return (document) => document.Id == id;
        }

        public TDocument GetById(Guid id)
        {
            return GetCollection()
                .Where(c => c.Id == id)
                .FirstOrDefault();
        }
    }
}
