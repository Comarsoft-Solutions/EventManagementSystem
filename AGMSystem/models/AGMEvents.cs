using Microsoft.Ajax.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public class AGMEvents
    {


        #region "Variables"
        protected string mVenue;
        protected long mID;
        protected string mStartDate;
        protected string mEndDate;
        protected string mDateCreated;
        protected bool mStatusID;
        protected double mAttendanceFee;
        protected bool mAttendeeSettings;

        protected string mEventName;
        protected Database db;
        protected string mConnectionName;
        protected long mObjectUserID;
        protected string mMsgFlg;
        #endregion

        #region "Properties"
        public bool AttendeeSettings { get { return mAttendeeSettings; } set { mAttendeeSettings = value; } }
        public double AttendanceFee { get { return mAttendanceFee; } set { mAttendanceFee = value; } }
        public string Venue { get { return mVenue; } set { mVenue = value; } }
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

        public string StartDate
        {
            get { return mStartDate; }
            set { mStartDate = value; }
        }

        public string EndDate
        {
            get { return mEndDate; }
            set { mEndDate = value; }
        }

        public string DateCreated
        {
            get { return mDateCreated; }
            set { mDateCreated = value; }
        }

        public bool StatusID
        {
            get { return mStatusID; }
            set { mStatusID = value; }
        }

        public string EventName
        {
            get { return mEventName; }
            set { mEventName = value; }
        }

        #endregion
        #region "Constructors"


        public AGMEvents(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);

        }

        #endregion

        #region "Methods"


        public DataSet GetEventInfo(string eventName)
        {
            string sql = "select * from AGMEvents  WHERE EventName = '" + eventName + "'";
            return ReturnDs(sql);
        }
        public void Clear()
        {
            mID = 0;
            mStartDate = "";
            mEndDate = "";
            mDateCreated = "";
            mStatusID = false;
            mEventName = "";
            mMsgFlg = "";

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
                sql = "SELECT * FROM AGMEvents WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM AGMEvents WHERE ID = " + mID;
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
                    mMsgFlg = "AGMEvents not found.";

                    return false;

                }


            }
            catch (Exception e)
            {
                mMsgFlg = e.Message;
                return false;

            }

        }

        public virtual System.Data.DataSet GetAGMEvents()
        {

            return GetAGMEvents(mID);

        }

        public DataSet getAllEvents() {
            string str = "select ID,EventName,format(StartDate,'dd/MM/yyyy') as StartDate, format(EndDate,'dd/MM/yyyy')as EndDate,AttendanceFee from AGMEvents";
            return ReturnDs(str);
        }
        public DataSet updateEvent(string theme, string sponsor, int eventID) 
        {
            string str = "Update AGMEvents set Theme = '"+theme+"', Sponsor= '"+sponsor+"' where ID="+eventID+"";
            return ReturnDs(str);
        }

        public virtual DataSet GetAGMEvents(long ID)
        {

            string sql = null;

            if (ID > 0)
            {
                sql = "SELECT * FROM AGMEvents WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM AGMEvents WHERE ID = " + mID;
            }

            return GetAGMEvents(sql);

        }
        public DataSet getEventsBySearch(string name = "")
        {
            string str = "";
            if (name.Length > 0)
            {
                str = "select ID ,EventName,case Venue when null then 'Not Set' when NULL then 'Not Set' else Venue end as Venue ,convert(varchar(12),StartDate,110) as StartDate,convert(varchar(12),EndDate,110) as EndDate, case StatusID when 0 then 'Closed' when 1 then 'Open' end as StatusID from AGMEvents where EventName like '%" + name.Trim()+ "%'";
            }
            if (name.IsNullOrWhiteSpace())
            {
                getSavedProjects();
            }
            return ReturnDs(str);
        }
        public DataSet getSavedProjects()
        {
            string str = "SELECT top 5 ID ,EventName,convert(varchar(12),StartDate,110) as StartDate,convert(varchar(12),EndDate,110) as EndDate, case StatusID when 0 then 'Closed' when 1 then 'Open'  end as StatusID from AGMEvents order by DateCreated desc";
            return ReturnDs(str);
        }

        protected virtual DataSet GetAGMEvents(string sql)
        {

            return db.ExecuteDataSet(CommandType.Text, sql);

        }

        #endregion


        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mStartDate = ((object)rw["StartDate"] == DBNull.Value) ? "" : rw["StartDate"].ToString();
            mEndDate = ((object)rw["EndDate"] == DBNull.Value) ? "" : rw["EndDate"].ToString();
            mDateCreated = ((object)rw["DateCreated"] == DBNull.Value) ? "" : rw["DateCreated"].ToString();
            mStatusID = ((object)rw["StatusID"] == DBNull.Value) ? false : bool.Parse(rw["StatusID"].ToString());
            mEventName = ((object)rw["EventName"] == DBNull.Value) ? "" : rw["EventName"].ToString();
            mAttendanceFee = ((object)rw["AttendanceFee"] == DBNull.Value) ? 0 : double.Parse(rw["AttendanceFee"].ToString());
            mAttendeeSettings = (object)rw["AttendeeSettings"] != DBNull.Value && bool.Parse(rw["AttendeeSettings"].ToString());
            mVenue = ((object)rw["Venue"] == DBNull.Value) ? "" : rw["Venue"].ToString();


        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@StartDate", DbType.String, mStartDate);
            db.AddInParameter(cmd, "@EndDate", DbType.String, mEndDate);
            db.AddInParameter(cmd, "@DateCreated", DbType.String, mDateCreated);
            db.AddInParameter(cmd, "@StatusID", DbType.Boolean, mStatusID);
            db.AddInParameter(cmd, "@EventName", DbType.String, mEventName);
            db.AddInParameter(cmd, "@AttendanceFee", DbType.Double, mAttendanceFee);
            db.AddInParameter(cmd, "@AttendeeSettings", DbType.Boolean, mAttendeeSettings);
            db.AddInParameter(cmd, "@Venue", DbType.String, mVenue);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_AGMEvents");

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


        public DataSet getSavedEvents()
        {
            string str = "SELECT  ID ,EventName,convert(varchar(12),StartDate,110) as StartDate,convert(varchar(12),EndDate,110) as EndDate,case StatusID when 0 then 'Closed' else 'Open' end as EventStatus from AGMEvents order by ID desc";
            return ReturnDs(str);
        }
        public DataSet getSavedEventID(string eventName)
        {
            string str = "SELECT top 1 ID from AGMEvents where EventName like '%"+eventName+"%'order by ID desc";
            return ReturnDs(str);
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
                return null;
            }
        }


        public DataSet getEnquiriesByFilterSearch(string eventNames = "")
        {
            string str = "";

            if (eventNames.Length > 0 )
            {

                str = "select * from AGMEvents where firstName like '%" + eventNames + "%' ";

            }
            if (eventNames.Length <= 0 )
            {
                GetEnquiries();
            }

            return ReturnDs(str);
        }
        public virtual DataSet GetEnquiries()
        {
            string cmd = "select * from AGMEvents ";
            return db.ExecuteDataSet(CommandType.Text, cmd);

        }

        #region get Event 
        public DataSet GetEventName()
        {
            string Sql = "Select top 1* from AGMEvents where StatusID = 1";
            return ReturnDs(Sql);
        }

        public DataSet editEvent(string IDs)
        {
            string str = "SELECT ID ,EventName,convert(varchar(12),StartDate,110) as StartDate,convert(varchar(12),EndDate,110) as EndDate,case StatusID when 0 then 'Closed' else 'Open' end as EventStatus from AGMEvents where id=" + IDs + "order by ID desc ";
            return ReturnDs(str);
        }

        public DataSet currentEvent(int EventID = 0)
        {
            try
            {
                string st = "";
                if (EventID > 0)
                {
                    st = "select top 1 ID,EventName from AGMEvents where ID=" + EventID + " order by DateCreated desc";
                }
                else
                {
                    st = "select top 1 ID,EventName from AGMEvents order by DateCreated desc";
                }

                return ReturnDs(st);
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }

        }
        #endregion
        #endregion

        #region "Delete"

        public virtual bool Delete()
        {

            //Return Delete("UPDATE AGMEvents SET Deleted = 1 WHERE ID = " & mID) 
            return Delete("DELETE FROM AGMEvents WHERE ID = " + mID);

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
                mMsgFlg = e.Message;
                return false;

            }

        }

        #endregion

        #endregion
    }


    public class ActivitiesSave
    {
        #region vars

        protected string mStartDate;
        protected int mID;
        protected int mEventID;
        protected Database db;
        protected string mConnectionName;
        protected long mObjectUserID;
        protected string mMsgFlg;
        protected string mName;

        #endregion

        #region props
        public int ID 
        {
            get { return mID; }
            set { mID = value; } 
        }
        public string Name { get { return mName; } set { mName = value; } }
        public string StartDate { get { return mStartDate; } set { mStartDate = value; } }
        public int EventID { get { return mEventID; } set { mEventID = value; } }
        #endregion

        #region "Constructors"


        public ActivitiesSave(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);

        }

        #endregion

        #region methods

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);
            db.AddInParameter(cmd, "@StartDate", DbType.String, mStartDate);
            db.AddInParameter(cmd, "@Name", DbType.String, mName);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_Activity");

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


        public DataSet getSavedActivities( int eventID)
        {
            string str = "SELECT * from Activities where eventID="+eventID+"";
            return ReturnDs(str);
        }
        public DataSet getSavedEventID(string eventName)
        {
            string str = "SELECT top 1 ID from AGMEvents where EventName like '%" + eventName + "%'order by ID desc";
            return ReturnDs(str);
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
                return null;
            }
        }


        public DataSet getEnquiriesByFilterSearch(string eventNames = "")
        {
            string str = "";

            if (eventNames.Length > 0)
            {

                str = "select * from AGMEvents where firstName like '%" + eventNames + "%' ";

            }
            if (eventNames.Length <= 0)
            {
                GetEnquiries();
            }

            return ReturnDs(str);
        }
        public virtual DataSet GetEnquiries()
        {
            string cmd = "select * from AGMEvents ";
            return db.ExecuteDataSet(CommandType.Text, cmd);

        }

        #endregion
        #endregion
    }
}