using MySql.Data.MySqlClient;
using NLog;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.DAL.CTL;

namespace QST.MicroERP.Service
{
    public class CustomerService
    {
        #region Class Members/Class Variables

        private CustomerDAL _customerDAL;
        private CoreDAL _corDAL;
        private ItemDAL _itemDAL;
        private Logger _logger;
        private SupplierDE _supplier;
        private SupplierDAL _supplierDAL;

        #endregion
        #region Constructors
        public CustomerService()
        {
            _itemDAL = new ItemDAL();
            _customerDAL = new CustomerDAL();
            _supplierDAL = new SupplierDAL();
            _corDAL = new CoreDAL();
            _supplier = new SupplierDE();
            _logger = LogManager.GetLogger("fileLogger");
        }

        #endregion
        #region Customer
        public bool ManagementCustomer(CustomerDE mod)
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.customer.ToString());
                if ( mod.DBoperation == DBoperations.Update)
                    this.UpdateSupplier(mod);
                retVal = _customerDAL.ManageCustomer(mod, cmd);
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public void UpdateSupplier(CustomerDE mod)
        {
            MySqlCommand cmd = null;
            List<SupplierDE> supList = new List<SupplierDE>();
            if (mod.SupplierId > 0)
            {
                supList = _supplierDAL.SearchSuppliers("where 1=1 and Id= " + mod.SupplierId + "");
                if (supList.Count > 0)
                    _supplier = supList.FirstOrDefault();
                var supplier = new SupplierDE();
                supplier.ContactName = mod.Name;
                supplier.Phone = mod.Phone;
                supplier.CountryId = mod.CountryId;
                supplier.CityId = mod.CityId;
                supplier.AccId = mod.AccId;
                supplier.Address = mod.Address;
                supplier.IsCustomer = mod.IsSupplier;
                supplier.CustomerId = mod.Id;
                supplier.DBoperation = DBoperations.Update;
                supplier.Id = mod.SupplierId;
                supplier.CompanyName = _supplier.CompanyName;
                supplier.DiscRate = _supplier.DiscRate;
                supplier.IsActive=_supplier.IsActive;
                CheckandSetVal(supplier, mod);
                _supplierDAL.ManageSupplier(supplier, cmd);
            }
           
        }
        public bool DealCustomerAsASupplier(CustomerDE mod)
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);

                #region Deal Customer As a Supplier

                List<SupplierDE> supList = new List<SupplierDE>();
                if (mod.SupplierId > 0)
                {
                    supList = _supplierDAL.SearchSuppliers("where 1=1 and Id= " + mod.SupplierId + "");
                    if (supList.Count > 0)
                        _supplier = supList.FirstOrDefault();
                }
                _supplier.IsCustomer = mod.IsSupplier;
                _supplier.CustomerId = mod.Id;

                if (mod.IsSupplier == true)
                {
                    if (mod.SupplierId == 0)
                    {
                        _supplier.Id = _corDAL.GetnextId(TableNames.supplier.ToString());
                        _supplier.ContactName = mod.Name;
                        _supplier.Phone = mod.Phone;
                        _supplier.CountryId = mod.CountryId;
                        _supplier.CityId = mod.CityId;
                        _supplier.AccId = mod.AccId;
                        _supplier.Address = mod.Address;
                        _supplier.IsActive = true;
                        _supplier.DBoperation = DBoperations.Insert;
                        mod.SupplierId = _supplier.Id;
                    }
                    else
                    {
                        _supplier.DBoperation = DBoperations.Update;
                        _supplier.Id = mod.SupplierId;
                    }
                    _supplier.IsActive = true;
                    CheckandSetVal(_supplier, mod);
                    _supplierDAL.ManageSupplier(_supplier, cmd);
                }
                else
                {
                    if (mod.SupplierId > 0)
                    {
                        _supplier.DBoperation = DBoperations.Update;
                        _supplier.Id = mod.SupplierId;
                        _supplier.IsActive = false;
                        CheckandSetVal(_supplier, mod);
                        _supplierDAL.ManageSupplier(_supplier, cmd);
                    }
                }

                #endregion

                retVal = _customerDAL.ManageCustomer(mod, cmd);
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                MicroERPDataContext.EndTransaction(cmd);
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MicroERPDataContext.CancelTransaction(cmd);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public void CheckandSetVal(SupplierDE sup, CustomerDE cust)
        {
            if (sup.IsActive == true && cust.IsActive == true)
            {
                    sup.IsCustomer = true;
                    cust.IsSupplier = true;
            }
            else
            {
                sup.IsCustomer = false;
                cust.IsSupplier = false;
            }
        }
        public List<CustomerDE> SearchCustomers(CustomerDE mod)
        {
            List<CustomerDE> Customers = new List<CustomerDE>();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ClientId != default)
                    whereClause += $" AND ClientId={mod.ClientId}";
                if (mod.Name != default)
                    whereClause += $" AND Name like ''" + mod.Name + "''";
                if (mod.Email != default)
                    whereClause += $" AND Email like ''" + mod.Email + "''";
                if (mod.Phone != default)
                    whereClause += $" AND Phone like ''" + mod.Phone + "''";
                if (mod.Address != default)
                    whereClause += $" AND Address like ''" + mod.Address + "''";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                Customers = _customerDAL.SearchCustomers(whereClause);

                #endregion
            }
            catch (Exception exp)
            {
                _logger.Error(exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
            return Customers;
        }

        #endregion
    }
}
