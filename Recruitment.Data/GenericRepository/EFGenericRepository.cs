using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Linq.Dynamic;

namespace Recruitment.Data
{
    public class EFGenericRepository : RepositoryBase
    {
        private ObjectContext _context;

        /// <summary>
        /// Create an instance of an EDM (Entity Data Model)
        /// </summary>
        /// <param name="context"></param>
        public EFGenericRepository(ObjectContext context)
        {
            _context = context;
        }

        //return objectSet for Query, Create, Update, Delete
        public ObjectSet<T> GetObjectSet<T>() where T : class
        {
            return _context.CreateObjectSet<T>();
        }

        protected string GetEntitySetName<T>()
        {
            return String.Format("{0}", typeof(T).Name);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        #region IGenericRepository Members
        /// <returns>returns all EDM of type T into a querable list</returns>
        public override IQueryable<T> GetAll<T>()
        {
            return GetObjectSet<T>().AsQueryable();
        }


        public override IQueryable<T> GetAll<T>(string orderBy)
        {
            orderBy = "it." + orderBy;
            return GetObjectSet<T>().OrderBy(orderBy).AsQueryable();
        }

        /// <summary>
        /// Adds a new record of type T to T and saves it.
        /// </summary>
        public override void Create<T>(T entityToCreate)
        {
            this.GetObjectSet<T>().AddObject(entityToCreate);
            this.CommitChanges();
        }

        /// <summary>
        ///  Adds a new record of type T to T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityTOCreate"></param>
        public override void CreateNotCommit<T>(T entityTOCreate)
        {
            this.GetObjectSet<T>().AddObject(entityTOCreate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityListToCreate"></param>
        public override void CreateAll<T>(List<T> entityListToCreate)
        {
            foreach (T entity in entityListToCreate)
            {
                this.GetObjectSet<T>().AddObject(entity);
                this.CommitChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityListToCreate"></param>
        public override void CreateAllNotCommit<T>(List<T> entityListToCreate)
        {
            foreach (T entity in entityListToCreate)
            {
                this.GetObjectSet<T>().AddObject(entity);
            }
        }

        /// <summary>
        /// Edits record of id in type T and save changes to Database
        /// </summary>
        public override void Modify<T>(T entityToEdit)
        {
            GetObjectSet<T>().ApplyCurrentValues(entityToEdit);
            this.CommitChanges();
        }

        /// <summary>
        ///  Edits record of id in type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToModify"></param>
        public override void ModifyNotCommit<T>(T entityToModify)
        {
            this.GetObjectSet<T>().ApplyCurrentValues(entityToModify);
        }

        /// <summary>
        /// Delets record of id in type T and commit to database
        /// </summary>
        public override void Delete<T>(T entityToDelete)
        {
            this.GetObjectSet<T>().DeleteObject(entityToDelete);
            this.CommitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToDelete"></param>
        public override void DeleteNotCommit<T>(T entityToDelete)
        {
            this.GetObjectSet<T>().DeleteObject(entityToDelete);
        }

        /// <summary>
        /// Delete All Data on Type T and commit to database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public override void DeleteAll<T>()
        {
            foreach (T obj in this.GetObjectSet<T>().AsQueryable())
            {
                this.GetObjectSet<T>().DeleteObject(obj);
            }
            this.CommitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public override void DeleteAllNotCommit<T>()
        {
            foreach (T obj in this.GetObjectSet<T>().AsQueryable())
            {
                this.GetObjectSet<T>().DeleteObject(obj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityListToDelete"></param>
        public override void DeleteAll<T>(List<T> entityListToDelete)
        {
            foreach (T obj in entityListToDelete)
            {
                this.GetObjectSet<T>().DeleteObject(obj);
            }
            this.CommitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityListToDelete"></param>
        public override void DeleteAllNotCommit<T>(List<T> entityListToDelete)
        {
            foreach (T obj in entityListToDelete)
            {
                this.GetObjectSet<T>().DeleteObject(obj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CommitChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override IQueryable<T> Find<T>(Func<T, bool> predicate)
        {
            return this.GetObjectSet<T>().Where(predicate).AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public override IQueryable<T> Find<T>(Func<T, bool> predicate, string orderBy)
        {
            orderBy = "it." + orderBy;
            return this.GetObjectSet<T>().OrderBy(orderBy).Where(predicate).AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override T Single<T>(Func<T, bool> predicate)
        {
            return this.GetObjectSet<T>().SingleOrDefault<T>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override T First<T>(Func<T, bool> predicate)
        {
            return this.GetObjectSet<T>().FirstOrDefault<T>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override int Count<T>()
        {
            return this.GetObjectSet<T>().Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override int Count<T>(Func<T, bool> predicate)
        {
            return this.GetObjectSet<T>().Count(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public override IQueryable<T> Find<T>(Func<T, bool> predicate, int pageSize, int pageNumber, string orderBy)
        {
            orderBy = "it." + orderBy;

            if (pageSize <= 0) pageSize = 20;

            int rowsCount = GetObjectSet<T>().Where(predicate).Count();

            if (rowsCount <= pageSize || pageNumber <= 0) pageNumber = 1;

            int excludedRows = (pageNumber - 1) * pageSize;

            return this.GetObjectSet<T>().OrderBy(orderBy)
                .Where(predicate)
                .Skip(excludedRows)
                .Take(pageSize)
                .AsQueryable();
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public override IQueryable<T> GetAll<T>(int pageSize, int pageNumber, string orderBy)
        {
            orderBy = "it." + orderBy;

            if (pageSize <= 0) pageSize = 20;

            int rowsCount = GetObjectSet<T>().Count();

            if (rowsCount <= pageSize || pageNumber <= 0) pageNumber = 1;

            int excludedRows = (pageNumber - 1) * pageSize;

            return this.GetObjectSet<T>().OrderBy(orderBy)
                .Skip(excludedRows)
                .Take(pageSize)
                .AsQueryable();
        }

        public override ObjectResult<T> ExecuteQuery<T>(string query, params object[] parameters)
        {
            return _context.ExecuteStoreQuery<T>(query, parameters);
        }
    }
}
