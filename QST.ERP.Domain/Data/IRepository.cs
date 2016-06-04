using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.Data
{
    
    /// <summary>
    ///   Interface defining all the required method for the DAL Repository
    /// </summary>    
    public interface IRepository<T> where T : BaseDomain
    {
        BaseDomain Insert(T entity);
        //void Delete(object id);
        void Delete(T entity);
        void Update(T entity);
        
        //T GetById(object id);
        T GetById(params object[] primaryKyes);
        IList<T> GetAll();
        IList<T> GetAllPaged(int pageNumber, int pageSize, out int totalCount);
        IQueryable<T> Query { get; }

        void CommitAllChanges();
    }
}
