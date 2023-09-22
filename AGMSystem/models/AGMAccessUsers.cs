using AGMSystem.models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AGMSystem
{
    public class AGMAccessUsers
    {
        #region "Variables"

        protected string mMsgFlg;
        protected string mLastPasswordUpdate;
        protected long mID;
        protected long mCreatedBy;
        protected string mDateCreated;
        protected string mCode;
        protected string mUsername;
        protected string mPassword;
        protected string mFirstname;
        protected string mSurname;
        protected int mSystemRef;
        protected DataSet mUserDetails;
        protected string mRoleType;
        protected Database db;
        protected Database dbPay;
        protected string mConnectionName;

        protected long mObjectUserID;
        #endregion

        #region "Properties"
        public int SystemRef
        {
            get { return mSystemRef; }
            set { mSystemRef = value; }
        }
        public string MsgFlg
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

        public string LastPasswordUpdate
        {
            get { return mLastPasswordUpdate; }
            set { mLastPasswordUpdate = value; }
        }

        public long ID
        {
            get { return mID; }
            set { mID = value; }
        }

        public long CreatedBy
        {
            get { return mCreatedBy; }
            set { mCreatedBy = value; }
        }

        public string DateCreated
        {
            get { return mDateCreated; }
            set { mDateCreated = value; }
        }
        public string Username
        {
            get { return mUsername; }
            set { mUsername = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        public string Firstname
        {
            get { return mFirstname; }
            set { mFirstname = value; }
        }

        public string Surname
        {
            get { return mSurname; }
            set { mSurname = value; }
        }

        public string RoleType
        {
            get { return mRoleType; }
            set { mRoleType = value; }
        }

        public string Code
        {
            get { return mCode; }
            set { mCode = value; }
        }

        #endregion

        #region "Methods"

        #region "Constructors"


        public AGMAccessUsers(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);
            dbPay = new DatabaseProviderFactory().Create("cn");
        }

        #endregion


        public void Clear()
        {
            mLastPasswordUpdate = null;
            mID = 0;
            mCreatedBy = mObjectUserID;
            mDateCreated = "";
            mUsername = "";
            mPassword = "";
            mFirstname = "";
            mSurname = "";
            mRoleType = "";
            mSystemRef = 0;

        }

        #region "Retrieve Overloads"

        public virtual bool Retrieve()
        {

            return this.Retrieve(mID);

        }

        public virtual bool Retrieve(long ID)
        {

            string sql = null;

            if (ID > 0)
            {
                sql = "SELECT * FROM PortalAccessUsers WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM PortalAccessUsers WHERE ID = " + mID;
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
                    SetErrorDetails("PortalAccesssUsers not found.");

                    return false;

                }


            }
            catch (Exception e)
            {
                SetErrorDetails(e.Message);
                return false;

            }

        }
        protected DataSet ReturnDs(string str)
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
        public DataSet getQueryTypes()
        {
            try
            {
                string str = "select * from QueryTypes";
                return ReturnDs(str);
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;

            }
        }
        public DataSet getSearchOptions()
        {
            string str = "select * from SearchOption";
            return ReturnDs(str);

        }
        public Boolean ValidateUserLogin(string username, string password)
        {
            try
            {
                string str = "select * from AGM_Users where LoginCode ='" + username + "'";
                DataSet ds = db.ExecuteDataSet(CommandType.Text, str);
                if (ReturnDs(str) != null)
                {
                    DataRow rw = ds.Tables[0].Rows[0];
                    EncryptDecrypt ep = new EncryptDecrypt();
                    if (password == ep.DecryptPassword(rw["password"].ToString()))
                    {
                        mUserDetails = ds;
                        mSystemRef = int.Parse(ReturnDs(str).Tables[0].Rows[0]["ID"].ToString());
                        mRoleType = ReturnDs(str).Tables[0].Rows[0]["RoleID"].ToString();
                        mCode = ReturnDs(str).Tables[0].Rows[0]["LoginCode"].ToString();
                        return true;
                    }
                    else
                    {
                        //Put code to increase password failed attempts and also to lock account here
                        mMsgFlg = "Invalid Password";
                        return false;
                    }

                }
                else
                {
                    mMsgFlg = "Invalid UserName";
                    return false;
                }
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return false;
            }
        }

        public virtual System.Data.DataSet GetPortalAccesssUsers()
        {

            return GetPortalAccesssUsers(mID);

        }

        public virtual DataSet GetPortalAccesssUsers(long ID)
        {

            string sql = null;

            if (ID > 0)
            {
                sql = "SELECT * FROM PortalAccessUsers WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM PortalAccessUsers WHERE ID = " + mID;
            }

            return GetPortalAccesssUsers(sql);

        }

        protected virtual DataSet GetPortalAccesssUsers(string sql)
        {

            return db.ExecuteDataSet(CommandType.Text, sql);

        }

        #endregion


        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mLastPasswordUpdate = (rw["LastPasswordUpdate"] == DBNull.Value) ? string.Empty : rw["LastPasswordUpdate"].ToString();
            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mCreatedBy = ((object)rw["CreatedBy"] == DBNull.Value) ? 0 : int.Parse(rw["CreatedBy"].ToString());
            mDateCreated = rw["DateCreated"] == DBNull.Value ? DateTime.Today.ToString() : rw["DateCreated"].ToString();
            mUsername = (rw["Username"] == DBNull.Value) ? string.Empty : rw["Username"].ToString();
            mPassword = (rw["Password"] == DBNull.Value) ? string.Empty : rw["Password"].ToString();
            mFirstname = (rw["Firstname"] == DBNull.Value) ? string.Empty : rw["Firstname"].ToString();
            mSurname = (rw["Surname"] == DBNull.Value) ? string.Empty : rw["Surname"].ToString();
            mRoleType = (rw["RoleType"] == DBNull.Value) ? string.Empty : rw["RoleType"].ToString();


        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@LastPasswordUpdate", DbType.Date, mLastPasswordUpdate);
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@DateCreated", DbType.String, mDateCreated);
            db.AddInParameter(cmd, "@Username", DbType.String, mUsername);
            db.AddInParameter(cmd, "@Password", DbType.String, mPassword);
            db.AddInParameter(cmd, "@Firstname", DbType.String, mFirstname);
            db.AddInParameter(cmd, "@Surname", DbType.String, mSurname);
            db.AddInParameter(cmd, "@RoleType", DbType.String, mRoleType);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_PortalAccesssUsers");

            GenerateSaveParameters(ref db, ref cmd);


            try
            {
                DataSet ds = db.ExecuteDataSet(cmd);


                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    mID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

                }

                return true;


            }
            catch (Exception ex)
            {
                SetErrorDetails(ex.Message);
                return false;

            }

        }

        #endregion

        #region "Delete"

        public virtual bool Delete()
        {

            //Return Delete("UPDATE PortalAccessUsers SET Deleted = 1 WHERE ID = " & mID) 
            return Delete("DELETE FROM PortalAccessUsers WHERE ID = " + mID);

        }

        protected virtual bool Delete(string DeleteSQL)
        {


            try
            {
                db.ExecuteNonQuery(CommandType.Text, DeleteSQL);
                return true;


            }
            catch (Exception e)
            {
                SetErrorDetails(e.Message);
                return false;

            }

        }
        protected void SetErrorDetails(string str)
        {
            mMsgFlg = str;
        }

        #endregion

        #endregion
    }
}