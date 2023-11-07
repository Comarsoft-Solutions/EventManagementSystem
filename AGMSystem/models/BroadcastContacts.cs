using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AGMSystem.models
{
    public class BroadcastContacts : ErrorsLog
    {
        #region "Variables"

        protected string mMsgFlg;
        protected long mID;
        protected long mBroadcastListID;
        protected long mStatusID;
        protected decimal mMemberID;
        protected string mEmailAddress;

        protected string mMobileNo;
        protected Database db;
        protected string mConnectionName;

        protected long mObjectUserID;
        #endregion

        #region "Properties"
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

        public long ID
        {
            get { return mID; }
            set { mID = value; }
        }

        public long BroadcastListID
        {
            get { return mBroadcastListID; }
            set { mBroadcastListID = value; }
        }

        public long StatusID
        {
            get { return mStatusID; }
            set { mStatusID = value; }
        }

        public decimal MemberID
        {
            get { return  mMemberID;
; }
            set { mMemberID = value; }
        }

        public string EmailAddress
        {
            get { return mEmailAddress; }
            set { mEmailAddress = value; }
        }

        public string MobileNo
        {
            get { return mMobileNo; }
            set { mMobileNo = value; }
        }

        #endregion

        #region "Methods"

        #region "Constructors"


        public BroadcastContacts(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);

        }

        #endregion
        public bool updateMessageStatustoReadySend(int BroadCastID)
        {

            string str = "update BroadcastMessagesList set StatusID=2 where ID=" + BroadCastID + "";
            try
            {
                db.ExecuteNonQuery(CommandType.Text, str);
                return true;


            }
            catch (Exception e)
            {
                SetErrorDetails(e.Message);
                return false;

            }

        }
        public bool checkMessageStatus(int BroadCastID)
        {
            try
            {
                string str = "select ID from BroadcastMessagesList where ID=" + BroadCastID + "and StatusID=2";
                if (ReturnDs(str) == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return false;
            }
        }
        public bool checkContantsStatus(int BroadCastID)
        {
            try
            {
                string str = "select ID from BroadcastListContacts where BroadcastListID=" + BroadCastID + " and StatusID=2";
                if (ReturnDs(str) == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return false;
            }
        }
        public bool updateContantsStatustoReadySend(int BroadCastID)
        {

            string str = "update BroadcastListContacts set StatusID=2 where BroadcastListID=" + BroadCastID + "";
            try
            {
                db.ExecuteNonQuery(CommandType.Text, str);
                return true;


            }
            catch (Exception e)
            {
                SetErrorDetails(e.Message);
                return false;

            }

        }

        public void Clear()
        {
            mID = 0;
            mBroadcastListID = 0;
            mStatusID = 0;
            mMemberID = 0;
            mEmailAddress = "";
            mMobileNo = "";

        }

        #region "Retrieve Overloads"

        public virtual bool Retrieve()
        {

            return this.Retrieve(mID);

        }
        public DataSet getBroadCastContactDetails(int BroadCastID)
        {
            try
            {
                string str = "select Bc.ID,Bc.MemberID,pp.LastName,pp.FirstName,pp.Email from BroadcastListContacts Bc inner join RegistrationMembers pp on pp.Id = Bc.MemberID  where BroadcastListID = " + BroadCastID + ""; 
                return ReturnDs(str);
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
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
        public virtual bool Retrieve(long ID)
        {

            string sql = null;

            if (ID > 0)
            {
                sql = "SELECT * FROM BroadcastListContacts WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM BroadcastListContacts WHERE ID = " + mID;
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
                    SetErrorDetails("BroadcastContacts not found.");

                    return false;

                }


            }
            catch (Exception e)
            {
                SetErrorDetails(e.Message);
                return false;

            }

        }

        public virtual System.Data.DataSet GetBroadcastContacts()
        {

            return GetBroadcastContacts(mID);

        }

        public virtual DataSet GetBroadcastContacts(long ID)
        {

            string sql = null;

            if (ID > 0)
            {
                sql = "SELECT * FROM BroadcastListContacts WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM BroadcastListContacts WHERE ID = " + mID;
            }

            return GetBroadcastContacts(sql);

        }

        protected virtual DataSet GetBroadcastContacts(string sql)
        {

            return db.ExecuteDataSet(CommandType.Text, sql);

        }

        #endregion


        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mBroadcastListID = ((object)rw["BroadcastListID"] == DBNull.Value) ? 0 : int.Parse(rw["BroadcastListID"].ToString());
            mStatusID = ((object)rw["StatusID"] == DBNull.Value) ? 0 : int.Parse(rw["StatusID"].ToString());
            mMemberID = ((object)rw["MemberID"] == DBNull.Value) ? 0 : int.Parse(rw["MemberID"].ToString());
            mEmailAddress = ((object)rw["EmailAddress"] == DBNull.Value) ? string.Empty : rw["EmailAddress"].ToString();
            mMobileNo = ((object)rw["MobileNo"] == DBNull.Value) ? string.Empty : rw["MobileNo"].ToString();


        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);

            db.AddInParameter(cmd, "@BroadcastListID", DbType.Int32, mBroadcastListID);
            db.AddInParameter(cmd, "@StatusID", DbType.Int32, mStatusID);
            db.AddInParameter(cmd, "@MemberID", DbType.Decimal, mMemberID);
            db.AddInParameter(cmd, "@EmailAddress", DbType.String, mEmailAddress);
            db.AddInParameter(cmd, "@MobileNo", DbType.String, mMobileNo);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_BroadcastContacts");

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

            //Return Delete("UPDATE BroadcastListContacts SET Deleted = 1 WHERE ID = " & mID) 
            return Delete("DELETE FROM BroadcastListContacts WHERE ID = " + mID);

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

        #endregion

        #endregion

    }
}