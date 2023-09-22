using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AGMSystem.models
{
    public class ResolutionSave
    {
        #region vars
        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;
        protected long mObjectUserID;


        protected int mID;
        protected string mResolution;
        protected string mDetails;
        protected int mAGMID;
        #endregion

        #region props
        public int ID { get { return mID; } set { mID = value; } }
        public int AGMID { get { return mAGMID; } set { mAGMID = value; } }
        public string Resolution { get { return mResolution; } set { mResolution = value; } }
        public string Details { get { return mDetails; } set { mDetails = value; } }

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

        #region constructor
        public ResolutionSave(string connectionName, long ObjectUserID) 
        {
            mObjectUserID = ObjectUserID;
            this.mConnectionName = connectionName;
            db = new DatabaseProviderFactory().Create(connectionName);
        }
        #endregion

        #region methods

        #region "Retrieve Overloads"

        
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
                mMsgFlg=ex.Message;
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
                sql = "SELECT * FROM Resolutions WHERE Id = " + Id;
            }
            else
            {
                sql = "SELECT * FROM Resolutions WHERE Id = " + mID;
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

        public DataSet GetAllREsolutions(int AgmID)
        {
            try
            {
                string str = "Select * from Resolutions where AGMID=" + AgmID ;
                return ReturnDs(str);
            }
            catch (Exception ex)
            {

                Msgflg = ex.Message;
                return null;
            }
        }
        public DataSet GetSomeMembers(int projectID)
        {
            try
            {
                string str = "Select * from RegistrationMembers where ID in (select MemberID from AGM_Project_Members where ProjectID =" + projectID + " )";
                return ReturnDs(str);
            }
            catch (Exception ex)
            {

                Msgflg = ex.Message;
                return null;
            }
        }

        #endregion


        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mAGMID= ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mResolution = ((object)rw["EventID"] == DBNull.Value) ? "" : (rw["EventID"].ToString());
            mDetails = ((object)rw["RegTypeID"] == DBNull.Value) ? "" : (rw["RegTypeID"].ToString());
        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@AGMID", DbType.Int32, mAGMID);
            db.AddInParameter(cmd, "@Resolution", DbType.String, mResolution);
            db.AddInParameter(cmd, "@Details", DbType.String, mDetails);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_Resolutions");

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
        #endregion
    }

    public class VotingSave
    {
        #region vars
        public int mID;
        public int mAGMID;
        public int mResolutionID;
        public int mVotedBy;
        public bool mVote;

        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;
        protected long mObjectUserID;
        #endregion

        #region props
        public int ID { get { return mID; } set { mID = value; } }
        public int AGMID { get { return mAGMID; } set { mAGMID = value; } }
        public int ResolutionID { get { return mResolutionID; } set { mResolutionID = value; } }
        public int VotedBy { get { return mVotedBy; } set { mVotedBy = value; } }
        public bool Vote { get { return mVote; } set { mVote = value; } }

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

        #region constructor
        public VotingSave(string connectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            this.mConnectionName = connectionName;
            db = new DatabaseProviderFactory().Create(connectionName);
        }
        #endregion

        #region methods

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
        public DataSet checkIfVoted( int systemRef)
        {
            string str = "select * from AGM_Voting where VotedBy=" + systemRef;
            return ReturnDs(str);
        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@AGMID", DbType.Int32, mAGMID);
            db.AddInParameter(cmd, "@ResolutionID", DbType.Int32, mResolutionID);
            db.AddInParameter(cmd, "@VotedBy", DbType.Int32, mVotedBy);
            db.AddInParameter(cmd, "@Vote", DbType.Boolean, mVote);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_Voting");

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
        #endregion
    }
}