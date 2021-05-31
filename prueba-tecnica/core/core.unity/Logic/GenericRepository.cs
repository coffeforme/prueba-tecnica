using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Data.Entity.Core;
using System.Data;
using System.Data.Entity.Infrastructure;

using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Metadata.Edm;
using System.Reflection;
using core.unity.Interfaces;
using uT = core.unity.Types;
using System.Data.SqlClient;
using core.unity.Context;

namespace core.unity
{
    /// <summary>
    /// Generic repository
    /// </summary>
    public class GenericRepository : IRepository
    {
        private readonly string _connectionStringName;
        private DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TEntity&gt;"/> class.
        /// </summary>
        public GenericRepository()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public GenericRepository(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GenericRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = context;
        }

        public GenericRepository(ObjectContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = new DbContext(context, true);
        }

        public TEntity GetByKey<TEntity>(object keyValue) where TEntity : class
        {
            EntityKey key = GetEntityKey<TEntity>(keyValue);

            object originalItem;
            if (((IObjectContextAdapter)DbContext).ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                return (TEntity)originalItem;
            }
            return default;
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class
        {
            // here is a work around: 
            // - cast DbContext to IObjectContextAdapter then get ObjectContext from it
            // - call CreateQuery<TEntity>(entityName) method on the ObjectContext
            // - perform querying on the returning IQueryable, and it works!
            var entityName = GetEntityName<TEntity>();
            return ((IObjectContextAdapter)DbContext).ObjectContext.CreateQuery<TEntity>(entityName);
        }

        public List<TEntity> DatatableToEntity<TEntity>(DataTable dt) where TEntity : class
        {
            List<TEntity> res = new List<TEntity>();
            if (dt == null) return res;

            Type classType = typeof(TEntity);
            PropertyInfo[] propertyInfos = classType.GetProperties();

            if (propertyInfos.Count() == 0) return res; // there is no property to set value

            var columns = dt.Columns;
            foreach (DataRow row in dt.Rows)
            {
                TEntity t = ((IObjectContextAdapter)DbContext).ObjectContext.CreateObject<TEntity>();
                bool bStruct = false;
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    DataColumn column = columns[propertyInfo.Name];
                    if (column != null)
                    {
                        Type type = Type.GetType(propertyInfo.PropertyType.FullName);
                        bStruct = true;
                        if (string.IsNullOrEmpty(row[column].ToString()))
                        {
                            t = null;
                        }
                        else
                        {
                            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                type = Type.GetType(type.GetGenericArguments()[0].FullName);
                            }
                            if (type == typeof(bool))
                                propertyInfo.SetValue(t, Convert.ChangeType(Convert.ToByte(row[column]), type));
                            else
                                propertyInfo.SetValue(t, Convert.ChangeType(row[column], type));

                        }
                    }
                }
                if (bStruct)
                {
                    res.Add(t);
                }

            }
            return res;
        }
        public IQueryable<TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetQuery<TEntity>().Where(predicate);
        }

        public IQueryable<TEntity> GetQuery<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery<TEntity>());
        }

        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery<TEntity>().OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetQuery<TEntity>().OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery(criteria).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetQuery(criteria).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return specification.SatisfyingEntitiesFrom(GetQuery<TEntity>()).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return specification.SatisfyingEntitiesFrom(GetQuery<TEntity>()).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return GetQuery<TEntity>().Single(criteria);
        }

        public TEntity Single<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return criteria.SatisfyingEntityFrom(GetQuery<TEntity>());
        }

        public TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetQuery<TEntity>().First(predicate);
        }

        public TEntity First<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery<TEntity>()).First();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Attach(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Set<TEntity>().Remove(entity);

        }


        public DbDataReader ExecuteStoreProcedure(string sql, SqlParameter[] parametros)
        {
            DbDataReader dbDataReader;
            DataTable dt = new DataTable();
            SqlConnection entityConnection = (SqlConnection)_context.Database.Connection;
            SqlConnection conn = entityConnection;

            ConnectionState initialState = conn.State;
            try
            {
                if (initialState != ConnectionState.Open)
                    conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    foreach (var sqlParameter in parametros)
                    {
                        cmd.Parameters.Add(sqlParameter);
                    }

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sql;
                    dbDataReader = cmd.ExecuteReader();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dbDataReader;
        }
        public DataTable StoreProcedureDataTable(string sql, string[] parametros)
        {
            DataTable dt = new DataTable();
            SqlConnection entityConnection = (SqlConnection)_context.Database.Connection;
            SqlConnection conn = entityConnection;
            SqlDataAdapter daAdapter = new SqlDataAdapter("", conn);
            ConnectionState initialState = conn.State;
            string sqlQuery = string.Empty;
            try
            {
                if (initialState != ConnectionState.Open)
                    conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    for (int var = 0; var < parametros.Length; var++)
                    {
                        sqlQuery += " '" + parametros[var] + "',";
                    }
                    daAdapter.SelectCommand.CommandText = sql + sqlQuery.Substring(0, sqlQuery.Length - 1);
                    daAdapter.Fill(dt);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        public void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            IEnumerable<TEntity> records = Find(criteria);

            foreach (TEntity record in records)
            {
                Delete(record);
            }
        }
        public void Delete<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            IEnumerable<TEntity> records = Find(criteria);
            foreach (TEntity record in records)
            {
                Delete(record);
            }
        }
        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return GetQuery<TEntity>().AsEnumerable();
        }
        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, uT.SortOrder sortOrder = uT.SortOrder.Ascending) where TEntity : class
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize,
                                                  uT.SortOrder sortOrder = uT.SortOrder.Ascending) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize,
                                                  uT.SortOrder sortOrder = uT.SortOrder.Ascending) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public TEntity Save<TEntity>(TEntity entity) where TEntity : class
        {
            Add(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            var fqen = GetEntityName<TEntity>();

            object originalItem;
            EntityKey key = ((IObjectContextAdapter)DbContext).ObjectContext.CreateEntityKey(fqen, entity);
            if (((IObjectContextAdapter)DbContext).ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                ((IObjectContextAdapter)DbContext).ObjectContext.ApplyCurrentValues(key.EntitySetName, entity);
            }
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return GetQuery<TEntity>().Where(criteria);
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return GetQuery<TEntity>().Where(criteria).FirstOrDefault();
        }

        public TEntity FindOne<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return criteria.SatisfyingEntityFrom(GetQuery<TEntity>());
        }

        public IEnumerable<TEntity> Find<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery<TEntity>()).AsEnumerable();
        }

        public int Count<TEntity>() where TEntity : class
        {
            return GetQuery<TEntity>().Count();
        }

        public int Count<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return GetQuery<TEntity>().Count(criteria);
        }

        public int Count<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery<TEntity>()).Count();
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork(DbContext);
                }
                return unitOfWork;
            }
        }

        private EntityKey GetEntityKey<TEntity>(object keyValue) where TEntity : class
        {
            var entitySetName = GetEntityName<TEntity>();
            var objectSet = ((IObjectContextAdapter)DbContext).ObjectContext.CreateObjectSet<TEntity>();
            var keyPropertyName = objectSet.EntitySet.ElementType.KeyMembers[0].ToString();
            var entityKey = new EntityKey(entitySetName, new[] { new EntityKeyMember(keyPropertyName, keyValue) });
            return entityKey;
        }

        private string GetEntityName<TEntity>() where TEntity : class
        {
            // Thanks to Kamyar Paykhan -  http://huyrua.wordpress.com/2011/04/13/entity-framework-4-poco-repository-and-specification-pattern-upgraded-to-ef-4-1/#comment-688
            string entitySetName = ((IObjectContextAdapter)DbContext).ObjectContext
                .MetadataWorkspace
                .GetEntityContainer(((IObjectContextAdapter)DbContext).ObjectContext.DefaultContainerName, DataSpace.CSpace)
                                    .BaseEntitySets.Where(bes => bes.ElementType.Name == typeof(TEntity).Name).First().Name;
            return string.Format("{0}.{1}", ((IObjectContextAdapter)DbContext).ObjectContext.DefaultContainerName, entitySetName);
        }

        public string[] GetEntityKeyNames<TEntity>() where TEntity : class
        {


            var set = ((IObjectContextAdapter)DbContext).ObjectContext.CreateObjectSet<TEntity>();
            var entitySet = set.EntitySet;
            return entitySet.ElementType.KeyMembers.Select(k => k.Name).ToArray();
        }

        public int GetEntityLastKeyValue<TEntity>(string PropertyName) where TEntity : class
        {
            string[] entityKeyNames = GetEntityKeyNames<TEntity>();

            foreach (var entityKeyName in entityKeyNames)
            {
                if (entityKeyName == PropertyName)
                {

                    TEntity lastOrDefault = GetAll<TEntity>().LastOrDefault();
                    if (lastOrDefault != null)
                    {
                        string value =
                            lastOrDefault.GetType().GetProperty(entityKeyNames[0]).GetValue(lastOrDefault).ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            return int.Parse(value);
                        }
                    }
                    else
                    {
                        return 0;
                    }

                }
            }

            return -1;
        }

        private DbContext DbContext
        {
            get
            {
                if (_context == null)
                {
                    if (_connectionStringName == string.Empty)
                        _context = DbContextManager.Current;
                    else
                        _context = DbContextManager.CurrentFor(_connectionStringName);
                }
                return _context;
            }
        }

        private IUnitOfWork unitOfWork;
    }

    public static class DbCommandExtensions
    {
        public static void AddInputParameters<T>(this IDbCommand cmd,
            T parameters) where T : class
        {
            foreach (var prop in parameters.GetType().GetProperties())
            {
                object val = prop.GetValue(parameters, null);
                var p = cmd.CreateParameter();
                p.ParameterName = prop.Name;
                p.Value = val ?? DBNull.Value;
                cmd.Parameters.Add(p);
            }
        }
    }
}
