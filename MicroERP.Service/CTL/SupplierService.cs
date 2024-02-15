using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NLog;
using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using System;
using MicroERP.DAL.CTL;

namespace MicroERP.Service
{
    public class SupplierService
    {
        #region Class Members/Class Variables

        private SupplierDAL _supplierDAL;
        private CustomerDAL _customerDAL;
        private CoreDAL _corDAL;
        private ItemDAL _itemDAL;
        private Logger _logger;
        private CustomerDE searchedCust;

        #endregion
        #region Constructors
        public SupplierService()
        {
            _itemDAL = new ItemDAL();
            _supplierDAL = new SupplierDAL();
            _customerDAL = new CustomerDAL()
; _corDAL = new CoreDAL();
            searchedCust = new CustomerDE();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region Supplier
        public bool ManagementSupplier(SupplierDE mod)
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.supplier.ToString());
                if (mod.DBoperation == DBoperations.Update)
                    this.UpdateCustomer(mod);
                _supplierDAL.ManageSupplier(mod, cmd);
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
        public void UpdateCustomer(SupplierDE mod)
        {
            MySqlCommand cmd = null;
            List<SupplierDE> supList = new List<SupplierDE>();
            List<CustomerDE> custList = new List<CustomerDE>();
            if (mod.CustomerId > 0)
            {
                custList = _customerDAL.SearchCustomers("where 1=1 and Id= " + mod.CustomerId + "");
                if (custList.Count > 0)
                    searchedCust = custList.FirstOrDefault();
                var customer = new CustomerDE();
                customer.Name = mod.ContactName;
                customer.Phone = mod.Phone;
                customer.CountryId = mod.CountryId;
                customer.CityId = mod.CityId;
                customer.AccId = mod.AccId;
                customer.Address = mod.Address;
                customer.SupplierId = mod.Id;
                customer.IsSupplier = mod.IsCustomer;
                customer.DBoperation = DBoperations.Update;
                customer.Id = searchedCust.Id;
                customer.Email = searchedCust.Email;
                customer.Region = searchedCust.Region;
                customer.SendEmail = searchedCust.SendEmail;
                customer.IsActive = searchedCust.IsActive;
               CheckandSetVal(mod,customer);
                _customerDAL.ManageCustomer(customer, cmd);
            }

        }
        public bool DealSupplierAsACustomer(SupplierDE mod)
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);

                #region Deal Supplier As a Customer

                List<CustomerDE> custList = new List<CustomerDE>();
                if (mod.CustomerId > 0)
                {
                    custList = _customerDAL.SearchCustomers("where 1=1 and Id= " + mod.CustomerId + "");
                    if (custList.Count > 0)
                        searchedCust = custList.FirstOrDefault();
                }
                //var customer = new CustomerDE();
                searchedCust.SupplierId = mod.Id;
                searchedCust.IsSupplier = mod.IsCustomer;
                if (mod.IsCustomer == true)
                {
                    if (mod.CustomerId == 0)
                    {
                        searchedCust.Id = _corDAL.GetnextId(TableNames.customer.ToString());
                        searchedCust.DBoperation = DBoperations.Insert;
                        searchedCust.Name = mod.ContactName;
                        searchedCust.Phone = mod.Phone;
                        searchedCust.CountryId = mod.CountryId;
                        searchedCust.CityId = mod.CityId;
                        searchedCust.AccId = mod.AccId;
                        searchedCust.Address = mod.Address;
                        searchedCust.IsActive = true;
                        mod.CustomerId = searchedCust.Id;
                        searchedCust.IsActive = true;
                    }
                    else
                    {
                        searchedCust.DBoperation = DBoperations.Update;
                        searchedCust.Id = mod.CustomerId;
                        searchedCust.IsActive = true;
                    }
                    CheckandSetVal(mod, searchedCust);
                    _customerDAL.ManageCustomer(searchedCust, cmd);
                }
                else
                {
                    if (mod.CustomerId > 0)
                    {
                        searchedCust.DBoperation = DBoperations.Update;
                        searchedCust.Id = mod.CustomerId;
                        searchedCust.IsActive = false;
                        CheckandSetVal(mod, searchedCust);
                        _customerDAL.ManageCustomer(searchedCust, cmd);
                    }
                }

                #endregion

                retVal = _supplierDAL.ManageSupplier(mod, cmd);
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
        public List<SupplierDE> SearchSuppliers(SupplierDE mod)
        {
            List<SupplierDE> Suppliers = new List<SupplierDE>();
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
                if (mod.Phone != default)
                    whereClause += $" AND Phone like ''" + mod.Phone + "''";
                if (mod.Address != default)
                    whereClause += $" AND Address like ''" + mod.Address + "''";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                Suppliers = _supplierDAL.SearchSuppliers(whereClause);

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
            return Suppliers;
        }
        #endregion
    }
}