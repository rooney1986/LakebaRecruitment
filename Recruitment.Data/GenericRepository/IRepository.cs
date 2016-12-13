using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace Recruitment.Data
{
    

    /// <summary>
    /// Interface of the Repository
    /// </summary>

    public interface IRepository
    {
        IQueryable<T> GetAll<T>() where T : class;
        IQueryable<T> GetAll<T>(string orderBy) where T : class;
        IQueryable<T> GetAll<T>(int pageSize, int pageNumber, string orderBy) where T : class;
        IQueryable<T> Find<T>(Func<T, bool> predicate) where T : class;
        IQueryable<T> Find<T>(Func<T, bool> predicate, string orderBy) where T : class;
        IQueryable<T> Find<T>(Func<T, bool> predicate, int pageSize, int pageNumber, string orderBy) where T : class;
        ObjectResult<T> ExecuteQuery<T>(string query, params object[] parameters) where T : class;
        T Single<T>(Func<T, bool> predicate) where T : class;
        T First<T>(Func<T, bool> predicate) where T : class;
        int Count<T>() where T : class;
        int Count<T>(Func<T, bool> predicate) where T : class;
        void Create<T>(T entityTOCreate) where T : class;
        void CreateNotCommit<T>(T entityTOCreate) where T : class;
        void CreateAll<T>(List<T> entityListToCreate) where T : class;
        void CreateAllNotCommit<T>(List<T> entityListToCreate) where T : class;
        void Modify<T>(T entityToModify) where T : class;
        void ModifyNotCommit<T>(T entityToModify) where T : class;
        void Delete<T>(T entityToDelete) where T : class;
        void DeleteNotCommit<T>(T entityToDelete) where T : class;
        void DeleteAll<T>() where T : class;
        void DeleteAllNotCommit<T>() where T : class;
        void DeleteAll<T>(List<T> entityListToDelete) where T : class;
        void DeleteAllNotCommit<T>(List<T> entityListToDelete) where T : class;
        void CommitChanges();
        void Dispose();
    }
}
