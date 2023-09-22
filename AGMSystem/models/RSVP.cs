using Microsoft.Ajax.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;

namespace AGMSystem.models
{
    public class RSVP
    {
        #region variables
        protected int mID;
        protected Database db;
        protected string mConnectionName;
        protected long mObjectUserID;
        protected string mMsgFlg;
        protected string mFirstName;
        protected string mLastName;
        protected string mCompany;
        protected string mDesignation;
        protected bool mRSVPStatus;
        protected string mPhoneNumber;
        protected string mEmail;
        protected int mEventID;
        protected string mMembershipType;
        protected int mCompanyID;
        protected byte mProofOfPayment;
        protected int mAccomodationID;
        protected int mTransportID;

        #endregion

        #region properties 
        public string FirstName { get { return mFirstName; } set { mFirstName = value; } }
        public string LastName { get { return mLastName; } set { mLastName = value; } }
        public string Company { get { return mCompany; } set { mCompany = value; } }
        public string Designation { get { return mDesignation; } set { mDesignation = value; } }
        public bool RSVPStatus { get { return mRSVPStatus; } set { mRSVPStatus = value; } }
        public string PhoneNumber { get { return mPhoneNumber; } set { mPhoneNumber = value; } }
        public string Email { get { return mEmail; } set { mEmail = value; } }
        public int EventID { get { return mEventID; } set { mEventID = value; } }
        public string MembershipType { get { return mMembershipType; } set { mMembershipType = value; } }
        public int CompanyID { get { return mCompanyID; } set { mCompanyID = value; } }
        public byte ProofOfPayment { get { return mProofOfPayment; } set { mProofOfPayment = value; } }
        public int AccomodationID { get { return mAccomodationID; } set { mAccomodationID = value; } }
        public int TransportID { get { return mTransportID; } set { mTransportID = value; } }
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

        public int ID
        {
            get { return mID; }
            set { mID = value; }
        }

        #endregion
        #region constructor
        public RSVP(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);
        }
        #endregion

        #region methods

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

        public DataSet getAllEvents()
        {
            string str = "select * from AGMEvents";
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
                str = "select ID ,EventName,case Venue when null then 'Not Set' when NULL then 'Not Set' else Venue end as Venue ,convert(varchar(12),StartDate,110) as StartDate,convert(varchar(12),EndDate,110) as EndDate, case StatusID when 0 then 'Closed' when 1 then 'Open' end as StatusID from AGMEvents where EventName like '%" + name.Trim() + "%'";
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
        public DataSet GetMemberCount()
        {
            string str = "Select Count(ID)as count from RegistrationMembers";
            return ReturnDs(str);
        }
        public DataSet GetEventCount()
        {
            string str = "Select Count(ID)as count from AGMEvents";
            return ReturnDs(str);
        }
        public DataSet GetUpcommingEvents()
        {
            string str = "select * from AGMEvents where StartDate>= GETDATE() order by StartDate ";
            return ReturnDs(str);
        }
        public DataSet GetProjectCount()
        {
            string str = "Select Count(ID)as count from AGMProjects";
            return ReturnDs(str);
        }
        public DataSet GetAGMCount()
        {
            string str = "Select Count(ID)as count from AGMS";
            return ReturnDs(str);
        }
        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mFirstName = ((object)rw["FirstName"] == DBNull.Value) ? "" : rw["FirstName"].ToString();
            mLastName = ((object)rw["LastName"] == DBNull.Value) ? "" : rw["LastName"].ToString();
            mCompany = ((object)rw["Company"] == DBNull.Value) ? "" : rw["Company"].ToString();
            mDesignation = ((object)rw["Designation"] == DBNull.Value) ? "" : (rw["Designation"].ToString()); 
            mRSVPStatus =  (object)rw["RSVPStatus"] != DBNull.Value && bool.Parse(rw["RSVPStatus"].ToString());
            mPhoneNumber = ((object)rw["PhoneNumber"] == DBNull.Value) ? "" : (rw["PhoneNumber"].ToString());
            mEmail =  ((object)rw["Email"] == DBNull.Value) ? "" : (rw["Email"].ToString());
            mEventID = ((object)rw["EventID"] == DBNull.Value) ? 0 : int.Parse(rw["EventID"].ToString());
            mMembershipType = ((object)rw["MembershipType"] == DBNull.Value) ? "" : rw["MembershipType"].ToString();
            mCompanyID = ((object)rw["CompanyID"] == DBNull.Value) ? 0 : int.Parse(rw["CompanyID"].ToString());
            //mProofOfPayment = ((object)rw["ProofOfPayment"] == DBNull.Value) ?  : byte.Parse(rw["ProofOfPayment"].ToString()); 
            mAccomodationID = ((object)rw["AccomodationID"] == DBNull.Value) ? 0 : int.Parse(rw["AccomodationID"].ToString());
            mTransportID = ((object)rw["TransportID"] == DBNull.Value) ? 0 : int.Parse(rw["TransportID"].ToString());


        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@FirstName", DbType.String, mFirstName);
            db.AddInParameter(cmd, "@LastName", DbType.String, mLastName);
            db.AddInParameter(cmd, "@Company", DbType.String, mCompany);
            db.AddInParameter(cmd, "@Designation", DbType.String, mDesignation);
            db.AddInParameter(cmd, "@RSVPStatus", DbType.Boolean, mRSVPStatus);
            db.AddInParameter(cmd, "@PhoneNumber", DbType.String, mPhoneNumber);
            db.AddInParameter(cmd, "@Email", DbType.String, mEmail);
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);
            db.AddInParameter(cmd, "@MembershipType", DbType.String, mMembershipType);
            db.AddInParameter(cmd, "@CompanyID", DbType.Int32, mCompanyID);
            db.AddInParameter(cmd, "@ProofOfPayment", DbType.Byte, mProofOfPayment);
            db.AddInParameter(cmd, "@AccomodationID", DbType.Int32, mAccomodationID);
            db.AddInParameter(cmd, "@TransportID", DbType.Int32, mTransportID);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_MemberRSVP");

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


        public DataSet getSavedRSVPList()
        {
            string str = "SELECT  * from MemberRSVP where RSVPStatus=1 order by ID desc";
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
                MsgFlg =ex.Message;
                return null;
            }
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

        #endregion

    }
}