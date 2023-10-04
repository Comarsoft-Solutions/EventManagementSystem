using Microsoft.Ajax.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AGMSystem.models
{
    public class BroadcastMessagesList : ErrorsLog
    {
        #region "Variables"

        protected string mMsgFlg;
        protected long mID;
        protected long mCreatedBy;
        protected long mStatusID;
        protected string mDateCreated;
        protected string mBroadcastMessgeTitle;

        protected string mMessageType;
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

        public long CreatedBy
        {
            get { return mCreatedBy; }
            set { mCreatedBy = value; }
        }

        public long StatusID
        {
            get { return mStatusID; }
            set { mStatusID = value; }
        }

        public string DateCreated
        {
            get { return mDateCreated; }
            set { mDateCreated = value; }
        }

        public string BroadcastMessgeTitle
        {
            get { return mBroadcastMessgeTitle; }
            set { mBroadcastMessgeTitle = value; }
        }

        public string MessageType
        {
            get { return mMessageType; }
            set { mMessageType = value; }
        }

        #endregion

        #region "Methods"

        #region "Constructors"


        public BroadcastMessagesList(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);

        }

        #endregion


        public void Clear()
        {
            mID = 0;
            mCreatedBy = mObjectUserID;
            mStatusID = 0;
            mDateCreated = "";
            mBroadcastMessgeTitle = "";
            mMessageType = "";

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
                sql = "SELECT * FROM BroadcastMessagesList WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM BroadcastMessagesList WHERE ID = " + mID;
            }

            return Retrieve(sql);

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
                    SetErrorDetails("BroadcastMessagesList not found.");

                    return false;

                }


            }
            catch (Exception e)
            {
                SetErrorDetails(e.Message);
                return false;

            }

        }

        public virtual System.Data.DataSet GetBroadcastMessagesList()
        {

            return GetBroadcastMessagesList(mID);

        }
        public bool UpdateEmailListStatus(int BroadcastID, long MemberID)
        {
            string str = "update BroadcastListContacts set StatusID=2 where BroadCastListID=" + BroadcastID + " and MemberID=" + MemberID + "";
            db.ExecuteNonQuery(CommandType.Text, str);
            return true;

        }

        public bool UpdateEmailListStatusFailed(int BroadcastID, long MemberID)
        {
            string str = "update BroadcastListContacts set StatusID=3 where BroadCastListID=" + BroadcastID + " and MemberID=" + MemberID + "";
            db.ExecuteNonQuery(CommandType.Text, str);
            return true;

        }

        public virtual DataSet GetBroadcastMessagesList(long ID)
        {

            string sql = null;

            if (ID > 0)
            {
                sql = "SELECT * FROM BroadcastMessagesList WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM BroadcastMessagesList WHERE ID = " + mID;
            }

            return GetBroadcastMessagesList(sql);

        }

        protected virtual DataSet GetBroadcastMessagesList(string sql)
        {

            return db.ExecuteDataSet(CommandType.Text, sql);

        }

        #endregion


        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mCreatedBy = ((object)rw["CreateBy"] == DBNull.Value) ? 0 : int.Parse(rw["CreateBy"].ToString());
            mStatusID = ((object)rw["StatusID"] == DBNull.Value) ? 0 : int.Parse(rw["StatusID"].ToString());
            mDateCreated = ((object)rw["DateCreated"] == DBNull.Value) ? string.Empty : rw["DateCreated"].ToString();
            mBroadcastMessgeTitle = ((object)rw["BroadcastMessgeTitle"] == DBNull.Value) ? string.Empty : rw["BroadcastMessgeTitle"].ToString();
            mMessageType = ((object)rw["MessageType"] == DBNull.Value) ? string.Empty : rw["MessageType"].ToString();


        }

        public DataSet getUnassignedContacts(int BroadcastListID)
        {
            try
            {
                //string str = "select m.ID,m.LastName + '' +m.FirstName as Pensioner ,EmailAddress,f.RegName from Member m inner join FUND f on f.RegNo = m.RegNo where EmailAddress like '%@%' and m.ID not in (select distinct(PensionNo) from BroadcastListContacts where BroadCastListID = " + BroadcastListID + " ) order by m.ID,f.RegName";
                string str = "select pp.Id,pp.LastName + ' ' + pp.FirstName + ' ' + pp.Email as Member ,pp.Email from MemberRSVP pp  where Email like '%@%' and pp.Id not in (select distinct(MemberID) from BroadcastListContacts where BroadCastListID = " + BroadcastListID + " ) order by pp.Id ";
                return ReturnDs(str);
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }

        //public DataSet getassignedContacts(int BroadcastListID)
        //{
        //    try
        //    {
        //        string str = "select pp.Id,pp.LastName + ' ' + pp.FirstName + ' ' + pp.Email as Member ,pp.Email from RegistrationMembers pp  where Email like '%@%' and pp.Id in (select distinct(MemberID) from BroadcastListContacts where BroadCastListID = " + BroadcastListID + ") order by pp.Id ";
        //        return ReturnDs(str);

        //    }
        //    catch (Exception ex)
        //    {
        //        mMsgFlg = ex.Message;
        //        return null;
        //    }
        //}
        //public DataSet getassignedContacts(int BroadcastListID)
        //{
        //    try
        //    {
        //        string str = "select pp.Id,pp.LastName + ' ' + pp.FirstName + ' ' + pp.Email as Member ,pp.Email from RegistrationMembers pp  where Email like '%@%' and pp.Id in (select distinct(MemberID) from BroadcastListContacts where BroadCastListID = " + BroadcastListID + ") order by pp.Id ";
        //        return ReturnDs(str);

        //    }
        //    catch (Exception ex)
        //    {
        //        mMsgFlg = ex.Message;
        //        return null;
        //    }
        //}
        public DataSet getassignedContacts(int BroadcastListID)
        {
            try
            {
                string str = "select pp.Id,pp.LastName + ' ' + pp.FirstName + ' ' + pp.Email as Member ,pp.Email from MemberRSVP pp  where Email like '%@%' and pp.Id in (select distinct(MemberID) from BroadcastListContacts where BroadCastListID = " + BroadcastListID + ") order by pp.Id ";
                return ReturnDs(str);

            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }
        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@CreatedBy", DbType.Int32, mObjectUserID);
            db.AddInParameter(cmd, "@StatusID", DbType.Int32, mStatusID);
            db.AddInParameter(cmd, "@DateCreated", DbType.String, mDateCreated);
            db.AddInParameter(cmd, "@BroadcastMessgeTitle", DbType.String, mBroadcastMessgeTitle);
            db.AddInParameter(cmd, "@MessageType", DbType.String, mMessageType);

        }
        public DataSet getMessageTypes()
        {
            string str = "select ID,MessageTypes from MessageTypes";
            return ReturnDs(str);
        }

        public DataSet getMessageFormatTypes()
        {
            string str = "select ID,MessageFormatTypes from MessageFormatTypes";
            return ReturnDs(str);
        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_BroadcastMessagesList");

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
                SetErrorDetails(ex.Message);
                return false;
            }

        }

        #endregion

        #region "Delete"

        public virtual bool Delete()
        {

            //Return Delete("UPDATE BroadcastMessagesList SET Deleted = 1 WHERE ID = " & mID) 
            return Delete("DELETE FROM BroadcastMessagesList WHERE ID = " + mID);

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