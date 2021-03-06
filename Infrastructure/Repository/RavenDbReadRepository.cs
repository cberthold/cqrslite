﻿using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RavenDbReadRepository<TDocument> : IReadRepository<TDocument>, IDisposable
        where TDocument : IEntity
    {

        private static IDocumentStore store;
        string tableName = typeof(TDocument).Name;
        private IDocumentSession session;

        static RavenDbReadRepository()
        {
            store = new EmbeddableDocumentStore { ConnectionStringName = "RavenDB" };
            store.Conventions.AllowQueriesOnId = true;
            store.Initialize();
        }

        public RavenDbReadRepository()
        {
            session = store.OpenSession();
        }
        

        public IQueryable<TDocument> GetCollection()
        {
            var queryable = session.Query<TDocument>();
            return queryable;
        }

        public void Insert(TDocument document)
        {
            session.Store(document);
            session.SaveChanges();
        }

        public void Update(TDocument document)
        {
            session.Store(document);
            session.SaveChanges();
        }

        public Expression<Func<TDocument, bool>> GetIdExpression(IEntity entity)
        {
            var id = entity.Id;

            return (document) => document.Id == id;
        }

        public void Dispose()
        {
            if(session != null)
            {
                session.Dispose();
                session = null;
            }
           

            //if(store != null)
            //{
            //    store.Dispose();
            //    store = null;
            //}
        }

        public TDocument GetById(Guid id)
        {
            return session.Load<TDocument>(id);
        }
    }
}
