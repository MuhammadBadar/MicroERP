using MicroERP.DAL.IDAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.DAL
{
    public abstract class BaseDAL<T> : IBaseDAL<T>
    {
        public  MySqlCommand _cmd;
        public BaseDAL(  )
        {
            _cmd = new MySqlCommand ();
        }
        public bool ManageData ( T entity, MySqlCommand? cmd )
        {
            throw new NotImplementedException ();
        }
        public List<T> SearchData ( string whereClause, MySqlCommand? cmd )
        {
            throw new NotImplementedException ();
        }

        //public T ManageData ( T entity )
        //{
        //        bool closeConnectionFlag = false;
        //        try
        //        {
        //            _cmd = MicroERPDataContext.OpenMySqlConnection ();
        //            closeConnectionFlag = true;
        //            return entity;
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            if (closeConnectionFlag)
        //                MicroERPDataContext.CloseMySqlConnection (_cmd);
        //        }
        //}

    }
}
