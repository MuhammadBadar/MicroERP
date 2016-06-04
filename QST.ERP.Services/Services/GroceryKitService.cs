using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain.GroceryKit;
using QST.ERP.Domain.BDM;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Transactions;

using QST.ERP.Domain;
using QST.ERP.Domain.Data;
using QST.ERP.DAL.UoWorks;
using QST.ERP.DAL;

using QST.ERP.Domain.Translators;

namespace QST.ERP.Services
{
    public class GroceryKitService : Service, IGroceryKit
    {
        private IBDMServiceContract _bdmSvc;
        private readonly IRepository<MedicalProblemBE> _medProbRepo;
        private readonly IRepository<MemberStatusBE> _memStatusRepo;
        private readonly IUnitOfWork _uow;

        public GroceryKitService()
        {
            _uow = new GroceryKitDataUoWork(DBHelper.ConnectionString);
            _medProbRepo = new EFRepository<MedicalProblemBE>(_uow);
            _memStatusRepo = new EFRepository<MemberStatusBE>(_uow);
        }

        public int AddMember(MemberDE mod)
        {
            int Id = 0;
            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);
            StringBuilder sb = new StringBuilder();
            bool entityAddFlag = false;
            using (TransactionScope trans = new TransactionScope())
            {
                _bdmSvc = new BDMService();
                Person p = mod.ToPerson();
                Id = _bdmSvc.AddPerson(p);

                #region Commented Code
                
                //#region Add Entity

                //SqlCommand cmd = new SqlCommand();

                //#region CommandText & Params

                //sb.Append("INSERT INTO [dbo].[Entity]");
                //sb.Append("([SiteCode]");
                //sb.Append(",[EntityTypeCode]");
                
                ////sb.Append(",[ParentEntityID]");
                //sb.Append(",[EntityName]");
                //sb.Append(",[IsActive])");
                //sb.Append("VALUES");
                //sb.Append("(@SiteCode");
                //sb.Append(",@EntityTypeCode");
                ////sb.Append(",@ParentEntityID");
                //sb.Append(",@EntityName");
                //sb.Append(",@IsActive)");
                //sb.Append("Select @@IDENTITY");
                ////sb.Append("GO");

                //cmd.Parameters.AddWithValue("@SiteCode", mod.SiteCode);
                //cmd.Parameters.AddWithValue("@EntityTypeCode", mod.EntityTypeCode);
                ////cmd.Parameters.AddWithValue("@ParentEntityID", mod.Person.ParentEntityID);
                //cmd.Parameters.AddWithValue("@EntityName", mod.EntityName);
                //cmd.Parameters.AddWithValue("@IsActive", mod.IsActive);
                
                //#endregion

                //cmd.Connection = con;
                //cmd.CommandText = sb.ToString();
                //con.Open();

                //try
                //{
                //    decimal val = decimal.Zero;
                //    object retVal = cmd.ExecuteScalar();
                //    if (retVal != null)
                //    {
                //        val = (decimal)retVal;
                //        Id = Convert.ToInt32(val);
                //    }
                //}
                //catch (Exception ex)
                //{ 
                
                //}
                //#endregion

                //#region Add Person
                
                //cmd = new SqlCommand();

                //#region CommandText & Params

                //sb = new StringBuilder();

                //sb.Append("INSERT INTO [dbo].[Person]");
                //   sb.Append("([SiteCode]");
                //   sb.Append(",[EntityTypeCode]");
                //   sb.Append(",[PersonId]");
                //   sb.Append(",[ParentId]");
                //   sb.Append(",[ParentRelationId]");
                //   sb.Append(",[MeritalStatusId]");
                //   //sb.Append(",[Occupation]");
                //   sb.Append(",[DOB]");
                //   sb.Append(",[Age]");
                //   sb.Append(",[Picture]");
                //   sb.Append(",[Gender]");
                //   sb.Append(",[Signature]");
                //   sb.Append(",[ThumbImpression]");
                //   sb.Append(",[NationalId])");
                //sb.Append("VALUES");
                //    sb.Append("(@SiteCode");
                //    sb.Append(",@EntityTypeCode");
                //    sb.Append(",@PersonId");
                //    sb.Append(",@ParentId");
                //    sb.Append(",@ParentRelationId");
                //    sb.Append(",@MeritalStatusId");
                //    //sb.Append(",@Occupation");
                //    sb.Append(",@DOB");
                //    sb.Append(",@Age");
                //    sb.Append(",@Picture");
                //    sb.Append(",@Gender");
                //    sb.Append(",@Signature");
                //    sb.Append(",@ThumbImpression");
                //    sb.Append(",@NationalId)");
                //   //sb.Append("GO");

                //   cmd.Parameters.AddWithValue("@SiteCode", mod.SiteCode);
                //   cmd.Parameters.AddWithValue("@EntityTypeCode", mod.EntityTypeCode);
                //   cmd.Parameters.AddWithValue("@PersonId", Id);
                //   cmd.Parameters.AddWithValue("@ParentId", mod.ParentId);
                //   cmd.Parameters.AddWithValue("@ParentRelationId", mod.ParentRelationId);
                //   cmd.Parameters.AddWithValue("@MeritalStatusID", mod.MeritalStatuId);
                //   //cmd.Parameters.AddWithValue("@Occupation", mod.Occupation);
                //   cmd.Parameters.AddWithValue("@DOB", mod.DOB);
                //   cmd.Parameters.AddWithValue("@Age", mod.Age);
                //   cmd.Parameters.AddWithValue("@Picture", mod.Picture);
                //   cmd.Parameters.AddWithValue("@Gender", mod.Gender);
                //   cmd.Parameters.AddWithValue("@Signature", mod.Signature);
                //   cmd.Parameters.AddWithValue("@ThumbImpression", mod.ThumbImpression);
                //   cmd.Parameters.AddWithValue("@NationalId", mod.NationalId);

                //#endregion

                //cmd.CommandText = sb.ToString();
                //cmd.Connection = con;
                //try
                //{
                //    cmd.ExecuteNonQuery();
                //}
                //catch (Exception ex)
                //{ 
                
                //}
                //#endregion

                #endregion

                #region Add Form

                SqlCommand cmd = new SqlCommand();

                #region CommandText & Params

                sb = new StringBuilder();

                    sb.Append("INSERT INTO [dbo].[Form]");
                            sb.Append("([SiteCode]");
                            sb.Append(",[EntityTypeCode]");
                            sb.Append(",[FormId]");
                           
                            sb.Append(",[FormNo]");
                           sb.Append(",[FormType]");
                           sb.Append(",[FatherOrHusbandName]");
                           sb.Append(",[MedicalProblemId]");
                           sb.Append(",[MemberStatusId]");
                           sb.Append(",[OccupationId]");
                           sb.Append(",[HouseStatus]");
                           sb.Append(",[ZakatAcceptable]");
                           sb.Append(",[FamilySize]");
                           sb.Append(",[Salary]");
                           sb.Append(",[Donations]");
                           sb.Append(",[OtherIncome]");
                           sb.Append(",[ShortfallInCash]");
                           sb.Append(",[Remarks]");
                           sb.Append(",[IsDeserving]");
                           sb.Append(",[ReferredBy]");
                           sb.Append(",[DataCollectedBy]");
                           sb.Append(",[DataCollectedBySign]");
                           sb.Append(",[ApprovalByDataCollectedPerson]");
                           sb.Append(",[AuthorizedBy]");
                           sb.Append(",[ApprovalByDataAuthorizedPerson])");
                    sb.Append("VALUES");
                            sb.Append("(@SiteCode");
                            sb.Append(",@EntityTypeCode");
                            sb.Append(",@FormId");
                    
                            sb.Append(",@FormNo");
                           sb.Append(",@FormType");
                           sb.Append(",@FatherOrHusbandName");
                           sb.Append(",@MedicalProblemId");
                           sb.Append(",@MemberStatusId");
                           sb.Append(",@OccupationId");

                           sb.Append(",@HouseStatus");
                           sb.Append(",@ZakatAcceptable");
                           sb.Append(",@FamilySize");
                           sb.Append(",@Salary");
                           sb.Append(",@Donations");
                           sb.Append(",@OtherIncome");
                           sb.Append(",@ShortfallInCash");
                           sb.Append(",@Remarks");
                           sb.Append(",@IsDeserving");
                           sb.Append(",@ReferredBy");
                           
                sb.Append(",@DataCollectedBy");
                           sb.Append(",@DataCollectedBySign");
                           sb.Append(",@ApprovalByDataCollectedPerson");
                           sb.Append(",@AuthorizedBy");
                           sb.Append(",@ApprovalByDataAuthorizedPerson)");

                           cmd.Parameters.AddWithValue("@SiteCode", mod.SiteCode);
                           cmd.Parameters.AddWithValue("@EntityTypeCode", mod.EntityTypeCode);
                           cmd.Parameters.AddWithValue("@FormId", Id);
                    cmd.Parameters.AddWithValue("@FormNo", mod.FormNo);
                    cmd.Parameters.AddWithValue("@FormType", mod.FormType);
                    cmd.Parameters.AddWithValue("@FatherOrHusbandName", mod.FatherOrHusbandName);
                    cmd.Parameters.AddWithValue("@MedicalProblemId", mod.MedicalProblemId);
                    cmd.Parameters.AddWithValue("@MemberStatusId", mod.MemberStatusId);
                    cmd.Parameters.AddWithValue("@OccupationId", mod.OccupationId);
                    cmd.Parameters.AddWithValue("@HouseStatus", mod.HouseStatus);
                    cmd.Parameters.AddWithValue("@ZakatAcceptable", mod.ZakatAcceptable);
                    cmd.Parameters.AddWithValue("@FamilySize", mod.FamilySize);
                    cmd.Parameters.AddWithValue("@Salary", mod.Salary);
                    cmd.Parameters.AddWithValue("@Donations", mod.Donations);
                    cmd.Parameters.AddWithValue("@OtherIncome", mod.OtherIncome);
                    cmd.Parameters.AddWithValue("@ShortfallInCash", mod.ShortFallInCash);
                    cmd.Parameters.AddWithValue("@Remarks", mod.Remarks);
                    cmd.Parameters.AddWithValue("@IsDeserving", mod.IsDeserving);
                    
                    cmd.Parameters.AddWithValue("@ReferredBy", mod.ReferredBy);
                    cmd.Parameters.AddWithValue("@DataCollectedBy", mod.DataCollectedBy);
                    cmd.Parameters.AddWithValue("@DataCollectedBySign", mod.DataCollectedBySignatures);
                    cmd.Parameters.AddWithValue("@ApprovalByDataCollectedPerson", mod.ApprovalByDataCollectedPerson);
                    cmd.Parameters.AddWithValue("@AuthorizedBy", mod.AuthorizedBy);
                    cmd.Parameters.AddWithValue("@ApprovalByDataAuthorizedPerson", mod.ApprovalByDataAuthorizedPerson);

                #endregion

                cmd.CommandText = sb.ToString();
                cmd.Connection = con;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                { 
                
                }
                #endregion

                #region Member

                cmd = new SqlCommand();
                sb = new StringBuilder();

                sb.Append("INSERT INTO [dbo].[Member]");
                       sb.Append("([SiteCode]");
                       sb.Append(",[EntityTypeCode]");
                       sb.Append(",[MemberId]");
                       sb.Append(",[RegDate]");
                       sb.Append(",[RegNo]");
                       sb.Append(",[DistributionPointId]");
                       sb.Append(",[FamilyPackageId]");
                       sb.Append(",[IsActive])");
                sb.Append("VALUES");
                       sb.Append("(@SiteCode");
                       sb.Append(",@EntityTypeCode");
                       sb.Append(",@MemberId");
                       sb.Append(",@RegDate");
                       sb.Append(",@RegNo");
                       sb.Append(",@DistributionPointId");
                       sb.Append(",@FamilyPackageId");
                       sb.Append(",@IsActive)");
                //sb.Append("GO");

                cmd.Parameters.AddWithValue("@SiteCode", mod.SiteCode);
                cmd.Parameters.AddWithValue("@EntityTypeCode", mod.EntityTypeCode);
                cmd.Parameters.AddWithValue("@MemberId", Id);
                cmd.Parameters.AddWithValue("@RegDate", mod.RegDate);
                cmd.Parameters.AddWithValue("@RegNo", mod.RegNo);
                cmd.Parameters.AddWithValue("@DistributionPointId", mod.DistributionPointId);
                cmd.Parameters.AddWithValue("@FamilyPackageId", mod.FamilyPackageId);
                cmd.Parameters.AddWithValue("@IsActive", mod.IsActive);

                #endregion

                cmd.CommandText = sb.ToString();
                cmd.Connection = con;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                { 
                
                }
                con.Close();
                trans.Complete();
            }

            return Id;
        }
        public List<FormDE> GetAllForms()
        {
            throw new NotImplementedException();
        }
        public List<FormVw> GetViewOfAllForms()
        {
            throw new NotImplementedException();
        }
        public List<FormVw> GetViewOfForms(FormSearchCriteriaBE criteria)
        {
            throw new NotImplementedException();
        }

        public List<MemberStatusBE> GetAllMemberStatuses()
        {
            return _memStatusRepo.GetAll().ToList();
        }

        public List<MedicalProblemBE> GetAllMedicalProblems()
        {
            return _medProbRepo.GetAll().ToList();
        }



        //public int AddMember(FormDE mod)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
