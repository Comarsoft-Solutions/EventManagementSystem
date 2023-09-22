using Microsoft.Ajax.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AGMSystem.models
{
    public class AGMProjects
    {
        #region variables
        protected int mID;
        protected string mName;
        protected string mDescription;
        protected DateTime mStartDate;
        protected DateTime mMaturityDate;
        protected DateTime mDateCreated;
        protected int mStatusID;
        protected string mConnectionName;
        protected Database db;
        protected long mObjectUserID;
        protected string mMsgFlg;
        #endregion
        #region properties
        public string MsgFlg
        {
            get { return mMsgFlg; }
            set { mMsgFlg = value; }
        }
        public Database Database
        {
            get { return db; }
        }
         public string ConnectionName
        {
            get { return mConnectionName; }
        }

        public int ID
        {
            get { return mID; }
            set { mID = value; }
        }

        public DateTime StartDate
        {
            get { return mStartDate; }
            set { mStartDate = value; }
        }

        public DateTime MaturityDate
        {
            get { return mMaturityDate; }
            set { mMaturityDate = value; }
        }

        public DateTime DateCreated
        {
            get { return mDateCreated; }
            set { mDateCreated = value; }
        }

        public int StatusID
        {
            get { return mStatusID; }
            set { mStatusID = value; }
        }

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }
        #endregion
        #region constructor
        public AGMProjects(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID= ObjectUserID;
            mConnectionName= ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);
        }
        #endregion
        #region methods
        public void clear()
        {
            mID = 0;
            mStartDate = DateTime.MinValue;
            mMaturityDate = DateTime.MinValue;
            mDateCreated = DateTime.MinValue;
            mStatusID = 0;
            mDescription = string.Empty;
            mName = string.Empty;

        }
        #region overloads
        public virtual bool Retrieve()
        {

            return this.Retrieve(mID);

        }
        public virtual bool Retrieve(long Id)
        {

            string sql = null;

            if (Id > 0)
            {
                sql = "SELECT * FROM AGMProjects WHERE Id = " + Id;
            }
            else
            {
                sql = "SELECT * FROM AGMProjects WHERE Id = " + mID;
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
            object dateAddedValue = rw["DateAdded"];
            object dataEnd = rw["MaturityDate"];
            object datacreated = rw["DateCreated"];

            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mName = ((object)rw["Name"] == DBNull.Value) ? "" : (rw["Name"].ToString());
            mDescription = ((object)rw["RegTypeID"] == DBNull.Value) ? "" : (rw["Description"].ToString());
            mStartDate = (dateAddedValue != DBNull.Value && DateTime.TryParse(dateAddedValue.ToString(), out DateTime parsedDate)) ? parsedDate : DateTime.MinValue;
            mMaturityDate = (dataEnd != DBNull.Value && DateTime.TryParse(dataEnd.ToString(), out DateTime parsDate)) ? parsDate : DateTime.MinValue;
            mDateCreated = (datacreated != DBNull.Value && DateTime.TryParse(datacreated.ToString(), out DateTime parsCreDate)) ? parsCreDate : DateTime.MinValue;
            mStatusID = ((object)rw["StatusID"] == DBNull.Value) ? 0 : int.Parse(rw["StatusID"].ToString());

        }
        #endregion
        public DataSet GetSelectedProjects(int id)
        {
            string str = "select * from AGMProjects where ID= '" + id + "'";

            return ReturnDs(str);
        }
        public DataSet GetAllProjects()
        {
            string str = "select * from AGMProjects";

            return ReturnDs(str);
        }
        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@StartDate", DbType.DateTime, mStartDate);
            db.AddInParameter(cmd, "@MaturityDate", DbType.DateTime, mMaturityDate);
            //db.AddInParameter(cmd, "@DateCreated", DbType.DateTime, mDateCreated);
            db.AddInParameter(cmd, "@StatusID", DbType.Int32, mStatusID);
            db.AddInParameter(cmd, "@Name", DbType.String, mName);
            db.AddInParameter(cmd, "@Description", DbType.String, mDescription);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_AGMProjects");

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
        public DataSet getProjecctsBySearch(string name = "")
        {
            string str = "";
            if (name.Length > 0 )
            {
                str = "select * from AGMProjects where Name like '%" + name + "%'";
            }
            if (name.IsNullOrWhiteSpace())
            {
                getSavedProjects();
            }
            return ReturnDs(str);
        }
        public DataSet getSavedProjects()
        {
            string str = "SELECT top 5 ID ,Name,Description,convert(varchar(12),StartDate,110) as StartDate,convert(varchar(12),MaturityDate,110) as MaturityDate,case StatusID when 0 then 'Closed' when 1 then 'Open' else 'Matured' end as ProjectStatus from AGMProjects order by ID desc";
            return ReturnDs(str);
        }

        public DataSet GetProjectList( int EventID)
        {
            string str = "select distinct(Firstname) as Name , from AGMProjects where  ID not in (select ProjectID from EventProjects where MemberID=" + EventID + ") ";
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
                MsgFlg=ex.Message;
                return null;
            }
        }
        #endregion
    }
//End Class

    public class EventProjects
    {
        #region variables
        protected int mID;
        protected int mEventID;
        protected int mProjectID;
        protected int mProjectMembers;

        protected string mConnectionName;
        protected Database db;
        protected long mObjectUserID;
        protected string mMsgFlg;
        #endregion
        #region props
        public string Msgflg
        {
            get { return mMsgFlg; }

            set { mMsgFlg = value; }
        }
        public int ID { get { return mID; } set { mID = value; } }
        public int EventID { get { return mEventID; } set { mEventID = value; } }
        public int ProjectID { get { return mProjectID; } set { mProjectID = value; } }
        public int ProjectMembers { get { return mProjectMembers; } set { mProjectMembers = value; } }
        #endregion

        #region constructor
        public EventProjects(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);
        }
        #endregion

        #region methods
        public DataSet getEventMappedProjects(int projectID,int eventID)
        {
            string str = "select distinct(ap.name) as Name, ap.ID, (SELECT COUNT(*) AS rowcounts FROM AGM_Project_Members WHERE ProjectID= "+projectID+") as ProjectMembers from AGMProjects ap  where ap.ID in (select ProjectID from AGMEventProjects where EventID="+eventID+")   ";
            return ReturnDsNew(str);
        }

        public DataSet getCount(int projectID)
        {
            string str = " Select * from ProjectMembers where ProjectID =" + projectID;
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

        public DataSet GetMappedProjects(long projectID)
        {
            string str = "select Name,id from AGMProjects where ProjectID in (select ProjectID from EventProjects where ProjectID=" + projectID + ")";
            return ReturnDsNew(str);
        }
        public DataSet GetProjectList(long projectID)
        {
            string str = "select Name,id from AGMProjects where ProjectID not in (select ProjectID from EventProjects where ProjectID=" + projectID + ")";
            return ReturnDsNew(str);
        }
        public DataSet GetSomeMembers(int projectID)
        {
            try
            {
                string str = "Select * from RegistrationMembers where ID in (select MemberID from AGM_Project_Members where ProjectID =" + projectID + " )";
                return ReturnDS(str);
            }
            catch (Exception ex)
            {

                Msgflg = ex.Message;
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
                sql = "SELECT * FROM EventProjects WHERE Id = " + Id;
            }
            else
            {
                sql = "SELECT * FROM EventProjects WHERE Id = " + mID;
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
            mEventID = ((object)rw["EventID"] == DBNull.Value) ? 0 : int.Parse(rw["EventID"].ToString());
            mProjectID = ((object)rw["ProjectID"] == DBNull.Value) ? 0 : int.Parse(rw["ProjectID"].ToString());
            mProjectMembers = ((object)rw["ProjectMembers"] == DBNull.Value) ? 0 : int.Parse(rw["ProjectMembers"].ToString());

        }
        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@Id", DbType.Int32, mID);
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);
            db.AddInParameter(cmd, "@ProjectID", DbType.Int32, mProjectID);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_EventProjects");

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
        protected virtual DataSet ReturnDS(string sql)
        {

            return db.ExecuteDataSet(CommandType.Text, sql);

        }

        public DataSet getBySearch(int projectID,string firstname = "", string lastname = "", string IdNo = "" )
        {
            string str = "";
            if (firstname.Length > 0 && lastname.Length <= 0 && IdNo.Length <= 0)
            {
                str = "Select * from RegistrationMembers  ID in (select MemberID from AGM_Project_Members where ProjectID ="+projectID+" ) where firstName like '%" + firstname + "%'";
            }
            if (firstname.Length <= 0 && lastname.Length > 0 && IdNo.Length <= 0)
            {
                str = "Select * from RegistrationMembers  ID in (select MemberID from AGM_Project_Members where ProjectID ="+projectID+" )  where LastName like '%" + lastname + "%'";
            }
            if (firstname.Length <= 0 && lastname.Length <= 0 && IdNo.Length > 0)
            {
                str = "Select * from RegistrationMembers  ID in (select MemberID from AGM_Project_Members where ProjectID ="+projectID+" ) where Company like '%" + IdNo + "%'";
            }
            if (firstname.Length > 0 && lastname.Length > 0 && IdNo.Length > 0)
            {
                str = "Select * from RegistrationMembers  ID in (select MemberID from AGM_Project_Members where ProjectID ="+projectID+" )  where firstName like '%" + firstname + "%' and LastName like '%" + lastname + "%' and Company like '%" + IdNo + "%";
            }
            if (firstname.Length > 0 && lastname.Length > 0 && IdNo.Length <= 0)
            {
                str = "Select * from RegistrationMembers  ID in (select MemberID from AGM_Project_Members where ProjectID =\"+projectID+\" )  where firstName like '%" + firstname + "%' and LastName like '%" + lastname + "%' ";
            }
            if (firstname.IsNullOrWhiteSpace() && lastname.IsNullOrWhiteSpace() && IdNo.IsNullOrWhiteSpace())
            {
                GetSomeMembers(projectID);
            }
            return ReturnDS(str);
        }
        #endregion
    }
}