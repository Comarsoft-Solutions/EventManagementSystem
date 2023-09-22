using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AGMSystem.models
{
    public class AGMS
    {
        #region variables

        protected int mID;
        protected string mName;
        protected string mVenue;
        protected DateTime mStartDate;
        protected DateTime mEndDate;
        protected bool mAttendeeSettings;
        protected bool mEventSettigs;
        protected int mEventID;
        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;
        protected int mObjectUserID;

        #endregion

        #region Props
        public int ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public int EventID
        {
            get { return mEventID; }
            set { mEventID = value; }
        }
        public string Name { get { return mName; } set { mName = value; } }
        public string Venue { get { return mVenue; } set { mVenue = value; } }
        public DateTime StartDate { get { return mStartDate; } set { mStartDate = value; } }
        public DateTime EndDate { get { return mEndDate; } set { mEndDate = value; } }
        public bool AttendeeSettings { get { return mAttendeeSettings; } set { mAttendeeSettings = value; } }
        public bool EventSettings { get { return mEventSettigs; } set { mEventSettigs = value; } }
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
        public AGMS(string connectionName, int objectUserID)
        {
            mConnectionName = connectionName;
            db = new DatabaseProviderFactory().Create(connectionName);
        }
        #endregion

        #region methods
        public DataSet GetAGMS()
        {
            string str = "Select * from AGMS";
            return ReturnDsNew(str);
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
        #region retrieve
        public virtual bool Retrieve()
        {

            return this.Retrieve(mID);

        }

        public virtual bool Retrieve(long Id)
        {

            string sql = null;

            if (Id > 0)
            {
                sql = "SELECT * FROM AGMS WHERE Id = " + Id;
            }
            else
            {
                sql = "SELECT * FROM AGMS WHERE Id = " + mID;
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

            object startDate = rw["StartDate"];
            object endDate = rw["EndDate"];
            object attendeeSettingsValue = rw["AttendeeSettings"];
            object eventSettingsValue = rw["mEventSettigs"];

            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mName = ((object)rw["Name"] == DBNull.Value) ? "" : (rw["Name"].ToString());
            mVenue = ((object)rw["Venue"] == DBNull.Value) ? "" : (rw["Venue"].ToString());
            mStartDate = (startDate != DBNull.Value && DateTime.TryParse(startDate.ToString(), out DateTime parsedDate)) ? parsedDate : DateTime.MinValue;
            mEndDate = (endDate != DBNull.Value && DateTime.TryParse(endDate.ToString(), out DateTime parsDate)) ? parsDate : DateTime.MinValue;
            mAttendeeSettings = (attendeeSettingsValue != DBNull.Value) ? bool.Parse(attendeeSettingsValue.ToString()) : false;
            mEventSettigs = (eventSettingsValue != DBNull.Value) ? bool.Parse(eventSettingsValue.ToString()) : false;
            mEventID = ((object)rw["EventID"] == DBNull.Value) ? 0 : int.Parse(rw["EventID"].ToString());


        }
        #endregion

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@Id", DbType.Int32, mID);
            db.AddInParameter(cmd, "@Name", DbType.String, mName);
            db.AddInParameter(cmd, "@Venue", DbType.String, mVenue);
            db.AddInParameter(cmd, "@StartDate", DbType.DateTime, mStartDate);
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, mEndDate);
            db.AddInParameter(cmd, "@AttendeeSettings", DbType.Boolean, mAttendeeSettings);
            db.AddInParameter(cmd, "@EventSettigs", DbType.Boolean, mEventSettigs);
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_AGMS");

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