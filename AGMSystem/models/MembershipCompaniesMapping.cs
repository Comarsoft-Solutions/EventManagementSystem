using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AGMSystem.models
{
    
    public class MembershipCompaniesMapping
    {
        #region "Variables"
        protected int mID ;
        protected long mMemberID;
        protected long mCompanyID;
        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;

        protected long mObjectUserID;

        #endregion
        #region "Properties"
        public int ID
        {
            get { return mID; }  
            set { mID = value; }    
        }
        public long CompanyID
        {
            get { return (long)mCompanyID; }
            set { mCompanyID = value; }
        }
        public long MemberID
        {
            get { return mMemberID; }
            set { mMemberID = value; }
        }

        public string Msgflg
        {
            get { return mMsgFlg; }

            set { mMsgFlg = value; }
        }

        public Database Database
        {
            get { return db; }
        }

        public string OwnerType
        {
            get { return this.GetType().Name; }
        }

        public string ConnectionName
        {
            get { return mConnectionName; }
        }


        #endregion
        public MembershipCompaniesMapping(string ConnectionName,int mObjectUserID) {

            
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);

        }

        protected DataSet ReturnDsNew(string str)
        {
            try
            {
                DataSet ds = db.ExecuteDataSet(CommandType.Text, str);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }


        public virtual bool Retrieve()
        {

            return this.Retrieve(mID);

        }

        public virtual bool Retrieve(long Id)
        {

            string sql = null;

            if (Id > 0)
            {
                sql = "SELECT * FROM MembershipCompaniesMapping WHERE Id = " + Id;
            }
            else
            {
                sql = "SELECT * FROM MembershipCompaniesMapping WHERE Id = " + mID;
            }

            return Retrieve(sql);

        }

        protected virtual bool Retrieve(string sql)
        {


            try
            {
                DataSet dsRetrieve = db.ExecuteDataSet(CommandType.Text, sql);


                if (dsRetrieve != null && dsRetrieve.Tables.Count > 0 && dsRetrieve.Tables[0].Rows.Count > 0)
                {
                    LoadDataRecord(dsRetrieve.Tables[0].Rows[0]);

                    dsRetrieve = null;
                    return true;


                }
                else
                {
                    mMsgFlg = "Registration not found.";

                    return false;

                }


            }
            catch (Exception e)
            {
                mMsgFlg = e.Message;
                return false;

            }

        }


        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mMemberID = ((object)rw["MemberID"] == DBNull.Value) ? 0 : int.Parse(rw["MemberID"].ToString());
            mCompanyID = ((object)rw["CompanyID"] == DBNull.Value) ? 0 : int.Parse(rw["CompanyID"].ToString());
            

        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@Id", DbType.Int32, mID);
            db.AddInParameter(cmd, "@MemberID", DbType.Int32, mMemberID);
            db.AddInParameter(cmd, "@CompanyID", DbType.Int32, mCompanyID);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_MembershipCompaniesMapping");

            GenerateSaveParameters(ref db, ref cmd);


            try
            {
                DataSet ds = db.ExecuteDataSet(cmd);


                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    mID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

                }

                return true;


            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return false;

            }

        }


        #endregion


        #region  mapping
        public DataSet getMemberMappedCompanies(long MemberID)
        {
            string str = "select distinct(Name) as company,id from Company where  ID in (select CompanyID from MembershipCompaniesMapping where MemberID=" + MemberID + ")";
            return ReturnDsNew(str);
        }
        public DataSet GetCompanies(long MemberID)
        {
            string str = "select distinct(Name) as company,id from Company where  ID not in (select companyID from MembershipCompaniesMapping where MemberID=" + MemberID + ")";
            return ReturnDsNew(str);
        }
        public DataSet RemoveMapping(int companyId)
        {
            try
            {
                string query = "DELETE FROM MembershipCompaniesMapping WHERE CompanyID = " + companyId;
                DataSet rowsAffected = ReturnDsNew(query);

                return rowsAffected ;
            }
            catch (Exception ex)
            {

                mMsgFlg = ("Error removing mapping: " + ex.Message);
                return null;
            }
        }
        #endregion



    }
}