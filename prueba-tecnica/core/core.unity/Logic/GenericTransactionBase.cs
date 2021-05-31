using System;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using core.globalization;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Collections.Generic;
using core.util.json;
using core.util;
using core.unity.Types;
using core.unity.Context;

namespace core.unity.Logic
{
    /// <summary>
    /// Transacción base generica para el uso de contextos de EF
    /// </summary>
    /// <typeparam name="TEntity">Entidad para la cual se instanciara el contexto</typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class GenericTransactionBase<TEntity, TContext> : IDisposable where TEntity : class where TContext : DbContext, new()
    {

        private readonly GenericRepository Repositorio;
        private readonly TContext context;
        public string Log = string.Empty;
        protected TContext Context { get { return context; } set { } }

        public Exception Error { get; private set; }

        public GenericTransactionBase()
        {
            DbContextManager.InitStorage(new SimpleDbContextStorage());
            context = new TContext();
            context.Database.Log = (e) => { Log = e; };
            Repositorio = new GenericRepository(context);
        }

        /// <summary>
        /// Par su uso se debe recibir el Tkey generico en la clase
        /// </summary>
        /// <param name="_entity"></param>
        /// <param name="_TransactionaType"></param>
        /// <returns></returns>
        //public bool DeleteByKey(object key)
        //{
        //    bool transactionCommit = false;
        //    try
        //    {
        //        Tkey tKey = (Tkey)Convert.ChangeType(key, typeof(Tkey));
        //        TEntity entity = Repositorio.GetByKey<TEntity>(tKey);
        //        if (entity != null)
        //            transactionCommit = SaveChanges(entity, Transaction.Delete);
        //    }
        //    catch (InvalidCastException)
        //    {
        //        transactionCommit = false;
        //        throw new Exception("El key no es compatible con el tipo");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return transactionCommit;
        //}

        protected bool UpdateSaveChanges(TEntity _entity, Transaction _TransactionaType)
        {
            try
            {
                return SaveChanges(_entity, _TransactionaType);
            }
            catch//las operaciones internas generar Log 
            {
                return false;
            }
        }

        private bool SaveChanges(TEntity _Entity, Transaction transactionType)
        {
            bool transactionCommit = false;
            switch (transactionType)
            {
                case Transaction.Insert:
                    transactionCommit = Insert(_Entity);
                    break;
                case Transaction.Update:
                    transactionCommit = Update(_Entity);
                    break;
                case Transaction.Delete:
                    transactionCommit = Delete(_Entity);
                    break;
            }
            return transactionCommit;
        }

        private bool Insert(TEntity _Entity)
        {
            try
            {

                Repositorio.UnitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);
                Repositorio.Add(_Entity);
                Repositorio.UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                util.Log.Write(ex);
                Error = ex;
                Repositorio.UnitOfWork.RollBackTransaction();
                return false;
            }
        }

        private bool Update(TEntity _Entity)
        {
            try
            {
                Repositorio.UnitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);
                Repositorio.Update(_Entity);
                Repositorio.UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                util.Log.Write(ex);
                Error = ex;
                Repositorio.UnitOfWork.RollBackTransaction();
                return false;
            }
        }

        private bool Delete(TEntity _Entity)
        {
            try
            {
                Repositorio.UnitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);
                Repositorio.Delete(_Entity);
                Repositorio.UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                util.Log.Write(ex);
                Error = ex;
                Repositorio.UnitOfWork.RollBackTransaction();
                return false;
            }
        }

        protected TEntity FindOne(Expression<Func<TEntity, bool>> criteria)
        {
            TEntity ob;
            try
            {
                Repositorio.UnitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);
                ob = Repositorio.FindOne(criteria);
                Repositorio.UnitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                util.Log.Write(ex);
                Error = ex;
                Repositorio.UnitOfWork.RollBackTransaction();
                throw ex;
            }

            return ob;
        }

        protected bool Delete<S>(S _Entity) where S : class
        {
            try
            {
                Repositorio.UnitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);
                Repositorio.Delete(_Entity);
                Repositorio.UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                util.Log.Write(ex);
                Error = ex;
                Repositorio.UnitOfWork.RollBackTransaction();
                return false;
            }
        }

        protected bool Delete(Expression<Func<TEntity, bool>> criteria)
        {
            try
            {
                Repositorio.UnitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);
                Repositorio.Delete(criteria);
                Repositorio.UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                util.Log.Write(ex);
                Error = ex;
                Repositorio.UnitOfWork.RollBackTransaction();
                return false;
            }
        }

        protected TEntity SelectOne(Expression<Func<TEntity, bool>> criteria)
        {
            try
            {
                Repositorio.UnitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);
                TEntity ob = Repositorio.First(criteria);
                Repositorio.UnitOfWork.CommitTransaction();
                return ob;
            }
            catch (Exception ex)
            {
                util.Log.Write(ex);
                Error = ex;
                Repositorio.UnitOfWork.RollBackTransaction();
                throw ex;
            }
        }


        protected IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> criteria)
        {
            if (criteria != null)
                return Repositorio.GetQuery(criteria);
            else
                return Repositorio.GetQuery<TEntity>();

        }

        protected IQueryable<TEntity> Select()
        {
            return Repositorio.GetQuery<TEntity>();
        }

        protected IQueryable<S> Select<S>() where S : class
        {
            return Repositorio.GetQuery<S>();
        }

        protected List<S> Select<S, T>() where S : class where T : DbContext, new()
        {
            var AnotherContext = new T();
            var AnotherRepositorio = new GenericRepository(AnotherContext);
            return AnotherRepositorio.GetQuery<S>().ToList();
        }

        protected IQueryable<S> Select<S>(Expression<Func<S, bool>> criteria) where S : class
        {
            return Repositorio.GetQuery(criteria);
        }

        protected void HandleResponse(jsonresponse resp, Transaction trans, TEntity ob)
        {
            if (resp.done)
            {
                switch (trans)
                {
                    case Transaction.Insert:
                        resp.response = commonres.save_succesful;
                        break;
                    case Transaction.Update:
                        resp.response = commonres.update_succesful;
                        break;
                    case Transaction.Delete:
                        resp.response = commonres.delete_succesful;
                        break;
                }
                resp.status = jsonstatus.success;
                resp.data = ob;
            }
            else
            {
                resp.response = commonres.unknown_erro;
                resp.status = jsonstatus.error;
            }
        }

        protected void HandleResponse<S>(jsonresponse<S> resp, Transaction trans, object ob)
        {
            var _ob = JsonConvert.DeserializeObject<S>(JsonConvert.SerializeObject(ob, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

            if (resp.done)
            {
                switch (trans)
                {
                    case Transaction.Insert:
                        resp.response = commonres.save_succesful;
                        break;
                    case Transaction.Update:
                        resp.response = commonres.update_succesful;
                        break;
                    case Transaction.Delete:
                        resp.response = commonres.delete_succesful;
                        break;
                }
                resp.status = jsonstatus.success;
                resp.data = _ob;
            }
            else
            {
                resp.response = commonres.unknown_erro;
                resp.status = jsonstatus.error;
            }
        }
        public void Dispose()
        {
            util.Log.Write(Log);
            context.Dispose();
            GC.SuppressFinalize(this);
        }
        ~GenericTransactionBase()
        {
            GC.Collect();
        }
    }
}
