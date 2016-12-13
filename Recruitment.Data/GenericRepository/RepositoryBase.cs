using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace Recruitment.Data
{
    /// <summary>
    /// Base Class of the Repository
    /// </summary>
    public abstract class RepositoryBase : IRepository
    {
        /// <summary>
        /// Inherits IRepository makes them abstract, so they can be used in EFRepository
        /// </summary>
        /// <typeparam name="T">generic class where the class is unknown</typeparam>
        
        
       
        #region IGenericRepository Members
        public abstract IQueryable<T> GetAll<T>() where T : class;
        public abstract IQueryable<T> GetAll<T>(string orderBy) where T : class;
        public abstract IQueryable<T> GetAll<T>(int pageSize, int pageNumber, string orderBy) where T : class;
        public abstract IQueryable<T> Find<T>(Func<T, bool> predicate) where T : class;
        public abstract IQueryable<T> Find<T>(Func<T, bool> predicate, string orderBy) where T : class;
        public abstract IQueryable<T> Find<T>(Func<T, bool> predicate, int pageSize, int pageNumber, string orderBy) where T : class;
        public abstract ObjectResult<T> ExecuteQuery<T>(string query, params object[] parameters) where T : class;
        public abstract T Single<T>(Func<T, bool> predicate) where T : class;
        public abstract T First<T>(Func<T, bool> predicate) where T : class;
        public abstract int Count<T>() where T : class;
        public abstract int Count<T>(Func<T, bool> predicate) where T : class;
        public abstract void Create<T>(T entityToCreate) where T : class;
        public abstract void CreateNotCommit<T>(T entityTOCreate) where T : class;
        public abstract void CreateAll<T>(List<T> entityListToCreate) where T : class;
        public abstract void CreateAllNotCommit<T>(List<T> entityListToCreate) where T : class;
        public abstract void Modify<T>(T entityToEdit) where T : class;
        public abstract void ModifyNotCommit<T>(T entityToModify) where T : class;
        public abstract void Delete<T>(T entityToDelete) where T : class;
        public abstract void DeleteNotCommit<T>(T entityToDelete) where T : class;
        public abstract void DeleteAll<T>() where T : class;
        public abstract void DeleteAllNotCommit<T>() where T : class;
        public abstract void DeleteAll<T>(List<T> entityListToDelete) where T : class;
        public abstract void DeleteAllNotCommit<T>(List<T> entityListToDelete) where T : class;
        public abstract void CommitChanges();
        public abstract void Dispose();
        #endregion










    }
}
