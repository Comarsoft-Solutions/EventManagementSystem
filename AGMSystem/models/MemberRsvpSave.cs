using Microsoft.Ajax.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace AGMSystem.models
{
    public class MemberRsvpSave
    {
        #region variables
        protected Database db;
        protected string mConnectionName;
        protected long mObjectUserID;
        protected string mMsgFlg;
        protected int mID;
        protected int mCompanyID;
        protected long mEventID;
        protected long mRegTypeID;
        protected string mDateAdded;
        protected bool mIsApproved;
        protected string mFirstName;
        protected string mLastName;
        protected string mCompany;
        protected string mDesignation;
        protected string mPhoneNumber;
        protected string mEmail;
        protected string mNationalID;
        protected string mMemberType;
        protected int mLogisticsCombo;
        protected bool mRsvpStatus;
        protected string mRsvpDate;
        protected string mAccomodation;
        protected string mTransport;
        protected byte[] mQRCode;
        protected bool mPaymentStatus;



        #endregion

        #region properties
        public string MemberType { get { return mMemberType; } set { mMemberType = value; } }
        public bool RsvpStatus { get { return mRsvpStatus; } set { mRsvpStatus = value; } }
        public string RsvpDate { get { return mRsvpDate; } set { mRsvpDate = value; } }
        public string Accomodation { get { return mAccomodation; } set { mAccomodation = value; } }
        public string Designation { get { return mDesignation; } set { mDesignation = value; } }
        public string NationalID { get { return mNationalID; } set { mNationalID = value; } }
        public string Transport { get { return mTransport; } set { mTransport = value; } }
        public bool PaymentStatus { get { return mPaymentStatus; } set { mPaymentStatus = value; } }
        public int LogisticsCombo { get { return mLogisticsCombo; } set { mLogisticsCombo = value; } }
        public byte[] QRCode { get { return mQRCode; } set { mQRCode = value; } }
        public int ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public int CompanyID
        {
            get { return mCompanyID; }
            set { mCompanyID = value; }
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
        public string Msgflg
        {
            get { return mMsgFlg; }

            set { mMsgFlg = value; }
        }
        public string FirstName { get { return mFirstName; } set { mFirstName = value; } }
        public long EventID
        {
            get { return mEventID; }
            set { mEventID = value; }
        }

        public long RegTypeID
        {
            get { return mRegTypeID; }
            set { mRegTypeID = value; }
        }

        public string DateAdded
        {
            get { return mDateAdded; }
            set { mDateAdded = value; }
        }

        public bool IsApproved
        {
            get { return mIsApproved; }
            set { mIsApproved = value; }
        }

        

        public string LastName
        {
            get { return mLastName; }
            set { mLastName = value; }
        }

        public string Company
        {
            get { return mCompany; }
            set { mCompany = value; }
        }

        #endregion

        #region constructor
        public MemberRsvpSave(string connectionName, long userObjectID)
        {
            mObjectUserID = userObjectID;
            mConnectionName = connectionName;
            db = new DatabaseProviderFactory().Create(connectionName);
        }
        #endregion

        #region methods

        #region "Retrieve Overloads"

        public DataSet GetMemberInfo(int memberID)
        {
            try
            {
                string str = "Select * from RegistrationMembers where id=" + memberID+"";
                return ReturnDs(str);
            }
            catch (Exception xc)
            {

                Msgflg=xc.Message;
                return null;
            }
        }
        //public DataSet GetMemberInfo(int memberID)
        //{
        //    try
        //    {
        //        string str = "Select * from MemberRSVP where id="+memberID+"";
        //        return ReturnDs(str);
        //    }
        //    catch (Exception xc)
        //    {

        //        Msgflg=xc.Message;
        //        return null;
        //    }
        //}
        public DataSet UpdateMember(string firstName, string lastname, string email, string phonenumber, string company, string accomodation, string transport, string designation, int memberID )
        {
            try
            {
                string str = "update MemberRSVP set FirstName ='" + firstName + "', LastName='" + lastname + "', Email='" + email + "', PhoneNumber='" + phonenumber + "', Company='"+company+"', Accomodation = '"+accomodation+"', Transport='"+transport+"',Designation='"+designation+"' where ID="+ memberID + "";
                return ReturnDs(str);
            }
            catch (Exception c)
            {

                Msgflg = c.Message;
                throw;
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
                sql = "SELECT * FROM Company WHERE Id = " + Id;
            }
            else
            {
                sql = "SELECT * FROM Company WHERE Id = " + mID;
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

        //public DataSet GetAllMembers()
        //{
        //    try
        //    {
        //        string str = "Select * from RegistrationMembers where ID not in (select MemberID from AGM_Project_Members )";
        //        return ReturnDs(str);
        //    }
        //    catch (Exception ex)
        //    {

        //        Msgflg = ex.Message;
        //        return null;
        //    }
        //}
        //public DataSet GetSomeMembers(int projectID)
        //{
        //    try
        //    {
        //        string str = "Select * from RegistrationMembers where ID in (select MemberID from AGM_Project_Members where ProjectID =" + projectID + " )";
        //        return ReturnDs(str);
        //    }
        //    catch (Exception ex)
        //    {

        //        Msgflg = ex.Message;
        //        return null;
        //    }
        //}

        public virtual System.Data.DataSet GetRegistredCompanies()
        {

            return GetRegistredCompanies(mID);

        }

        public virtual DataSet GetRegistredCompanies(long Id)
        {

            string sql = null;

            if (Id > 0)
            {
                sql = "SELECT * FROM Company WHERE Id = " + Id;
            }
            else
            {
                sql = "SELECT * FROM Company WHERE Id = " + mID;
            }

            return GetRegistration(sql);

        }
        //public DataSet DeleteRecord(int memberID)
        //{
        //    string str = "delete from MemberRSVP where ID=" + memberID + "";
        //    return ReturnDs(str);
        //}
        public DataSet DeleteRecord(int memberID)
        {
            string str = "update RegistrationMembers set EventID=null, RsvpStatus=0 where id=" + memberID + "";
            return ReturnDs(str);
        }
        //public DataSet GetRsvps(int eventID)
        //{
        //    try
        //    {
        //        string str = "select * from MemberRSVP  where EventID=" + eventID + " AND (PaymentStatus IS NULL OR PaymentStatus =0)";
        //        return ReturnDs(str);
        //    }
        //    catch (Exception ex)
        //    {
        //        Msgflg = ex.Message;
        //        return null;
        //    }
            
        //}
        public DataSet GetRsvps(int eventID)
        {
            try
            {
                string str = "select lc.Price,lc.Combo,r.TShirtSize,case r.Golf when 1 then'Yes' else'No' end as Golf,* from RegistrationMembers r left join Logistics_Combos lc on r.EventID=lc.ID  where r.EventID=" + eventID + " and RsvpStatus=1 and PaymentStatus is null";
                return ReturnDs(str);
            }
            catch (Exception ex)
            {
                Msgflg = ex.Message;
                return null;
            }

        }
        public DataSet GetRSVPSList(int eventID, string names = "", string surname = "", string pension = "")
        {
            try
            {
                string str = "";
                if (names.Length > 0 && surname.Length <= 0 && pension.Length <= 0)
                {
                    str = "select * from RegistrationMembers where FirstName like '%" + names + "%'";
                }
                if (names.Length <= 0 && surname.Length > 0 && pension.Length <= 0)
                {
                    str = "select * from RegistrationMembers where LastName like '%" + surname + "%'";
                }
                if (names.Length <= 0 && surname.Length <= 0 && pension.Length > 0)
                {
                    str = "select * from RegistrationMembers where PensionFund like '%" + pension + "%'";
                }
                if (names.Length > 0 && surname.Length > 0 && pension.Length <= 0)
                {
                    str = "select * from RegistrationMembers where FirstName like '%" + names + "%' and LastName like '%" + surname + "%'";
                }
                if (names.Length > 0 && surname.Length > 0 && pension.Length > 0)
                {
                    str = "select * from RegistrationMembers where FirstName like '%" + names + "%' and LastName like '%" + surname + "%' and PensionFund like '%" + pension + "%'";
                }
                if (names.IsNullOrWhiteSpace() && surname.IsNullOrWhiteSpace() && pension.IsNullOrWhiteSpace())
                {
                    GetCheckin(eventID);
                }
                return ReturnDs(str);
            }
            catch (Exception x)
            {
                Msgflg = x.Message;
                return null;
            }
        }
        //public DataSet GetRSVPSList(int eventID,string names = "", string surname = "", string pension = "")
        //{
        //    try
        //    {
        //        string str = "";
        //        if (names.Length > 0 && surname.Length <= 0 && pension.Length <= 0)
        //        {
        //            str = "select * from MemberRSVP where FirstName like '%"+names+"%' and eventID="+eventID+ " and (PaymentStatus IS NULL OR PaymentStatus =0)";
        //        }
        //        if (names.Length <= 0 && surname.Length > 0 && pension.Length <= 0)
        //        {
        //            str = "select * from MemberRSVP where LastName like '%" + surname + "%'and eventID="+eventID+ " and (PaymentStatus IS NULL OR PaymentStatus =0)";
        //        }
        //        if (names.Length <= 0 && surname.Length <= 0 && pension.Length > 0)
        //        {
        //            str = "select * from MemberRSVP where PensionFund like '%" + pension + "%'and eventID="+eventID+ " and (PaymentStatus IS NULL OR PaymentStatus =0)";
        //        }
        //        if (names.Length > 0 && surname.Length > 0 && pension.Length <= 0)
        //        {
        //            str = "select * from MemberRSVP where FirstName like '%" + names + "%' and LastName like '%" + surname + "%'and eventID="+eventID+ " and (PaymentStatus IS NULL OR PaymentStatus =0)";
        //        }
        //        if (names.Length > 0 && surname.Length > 0 && pension.Length > 0)
        //        {
        //            str = "select * from MemberRSVP where FirstName like '%" + names + "%' and LastName like '%" + surname + "%' and PensionFund like '%" + pension + "%'and eventID="+eventID+ " and (PaymentStatus IS NULL OR PaymentStatus =0)";
        //        }
        //        if (names.IsNullOrWhiteSpace() && surname.IsNullOrWhiteSpace() && pension.IsNullOrWhiteSpace())
        //        {
        //            GetCheckin( eventID);
        //        }
        //        return ReturnDs(str);
        //    }
        //    catch (Exception x)
        //    {
        //        Msgflg = x.Message;
        //        return null;
        //    }
        //}
        public DataSet GetCheckin(int eventID,string names = "", string surname = "", string pension = "")
        {
            try
            {
                string str = "";
                if (names.Length > 0 && surname.Length <= 0 && pension.Length <= 0)
                {
                    str = "select * from MemberRSVP where FirstName like '%"+names+"%' and eventID="+eventID+ " and PaymentStatus=1";
                }
                if (names.Length <= 0 && surname.Length > 0 && pension.Length <= 0)
                {
                    str = "select * from MemberRSVP where LastName like '%" + surname + "%'and eventID="+eventID+ " and PaymentStatus=1";
                }
                if (names.Length <= 0 && surname.Length <= 0 && pension.Length > 0)
                {
                    str = "select * from MemberRSVP where PensionFund like '%" + pension + "%'and eventID="+eventID+ " and PaymentStatus=1";
                }
                if (names.Length > 0 && surname.Length > 0 && pension.Length <= 0)
                {
                    str = "select * from MemberRSVP where FirstName like '%" + names + "%' and LastName like '%" + surname + "%'and eventID="+eventID+ " and PaymentStatus=1";
                }
                if (names.Length > 0 && surname.Length > 0 && pension.Length > 0)
                {
                    str = "select * from MemberRSVP where FirstName like '%" + names + "%' and LastName like '%" + surname + "%' and PensionFund like '%" + pension + "%'and eventID="+eventID+ " and PaymentStatus=1";
                }
                if (names.IsNullOrWhiteSpace() && surname.IsNullOrWhiteSpace() && pension.IsNullOrWhiteSpace())
                {
                    GetCheckin( eventID);
                }
                return ReturnDs(str);
            }
            catch (Exception x)
            {
                Msgflg = x.Message;
                return null;
            }
        }
        public DataSet GetAccomodationAndTransportInfo(string mNationalID)
        {
            string sql = "select * from MemberRSVP  WHERE FullName = '" + mNationalID + "'";
            return ReturnDs(sql);
        }

        public DataSet GetCheckin(int eventID)
        {
            string str = "select * from MemberRSVP where EventID=" + eventID+ " and RsvpStatus=1 and PaymentStatus = 1 ";
            return ReturnDs(str);
        }
        public DataSet UpdateRegMember(bool golf, string newID,int logisticsCombo, bool rsvpStatus, string nationalID, int eventID, string tshirt)
        {
            string str = "Update RegistrationMembers set Golf="+golf+", NationalID='"+ newID + "', RsvpStatus='" + rsvpStatus + "',EventID=" + eventID + ",LogisticsCombo=" + logisticsCombo + ",TShirtSize='"+ tshirt + "' WHERE NationalID ='" + nationalID + "'";
            return ReturnDs(str);
        }
        public DataSet UpdateRegMemberWithoutCombos(bool golf,string newID, bool rsvpStatus, string nationalID, int eventID, string tshirt)
        {
            string str = "Update RegistrationMembers set Golf='"+golf+"', NationalID='"+ newID + "', RsvpStatus='" + rsvpStatus + "',EventID=" + eventID + ",TShirtSize='"+ tshirt + "' WHERE NationalID ='" + nationalID + "'";
            return ReturnDs(str);
        }
        public DataSet getMemberID(string nationalID)
        {
            string str = "Select * from RegistrationMembers WHERE NationalID ='" + nationalID + "'";
            return ReturnDs(str);
        }
        public DataSet UpdateAccomodation(int eventId, int id, int combospace )
        {
            string str = "Update AGM_Accomodation set Available= (Available -" + combospace + ") where ID="+id+" and EventID="+ eventId + "";
            return ReturnDs(str);
        }
        public DataSet UpdateTransport(int eventId, int id, int combospace)
        {
            string str = "Update AGM_Transport set Available= (Available -" + combospace + ") where ID=" + id + " and EventID=" + eventId + "";
            return ReturnDs(str);
        }

        //public DataSet UpdateRegMember(int logisticsCombo, bool rsvpStatus, string nationalID, int eventID)
        //{
        //    string str = "Update RegistrationMembers set RsvpStatus='" + rsvpStatus + "',EventID=" + eventID + ",LogisticsCombo=" + logisticsCombo + " WHERE NationalID ='" + nationalID + "'";
        //    return ReturnDs(str);
        //}

        public void updateReg( int logisticsCombo, bool rsvpStatus, string nationalID, int eventID)
        {
            using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
            {
                string query = "Update RegistrationMembers set RsvpStatus='"+rsvpStatus+"',EventID="+eventID+",LogisticsCombo="+logisticsCombo+" WHERE NationalID ='"+ nationalID+"'";
                using (SqlCommand cmd = new SqlCommand(query, myConnection))
                {
                    myConnection.Open();
                    cmd.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
        }
        //public void updateRsvp( int id, bool paymentStatus )
        //{
        //    using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
        //    {
        //        string query = "Update MemberRSVP set PaymentStatus='" + paymentStatus + "' WHERE ID ='"+ id+"'";
        //        using (SqlCommand cmd = new SqlCommand(query, myConnection))
        //        {
        //            myConnection.Open();
        //            cmd.ExecuteNonQuery();
        //            myConnection.Close();
        //        }
        //    }
        //}
        public void updateRsvp( int id, bool paymentStatus )
        {
            using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
            {
                string query = "Update RegistrationMembers set PaymentStatus='" + paymentStatus + "' WHERE ID ='"+ id+"'";
                using (SqlCommand cmd = new SqlCommand(query, myConnection))
                {
                    myConnection.Open();
                    cmd.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
        }
        public void updateCheckin( int id, bool checkin )
        {
            using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
            {
                string query = "Update RegistrationMembers set Checkin='" + checkin + "' WHERE ID ='"+ id+"'";
                using (SqlCommand cmd = new SqlCommand(query, myConnection))
                {
                    myConnection.Open();
                    cmd.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
        }

        protected virtual DataSet GetRegistration(string sql)
        {

            return db.ExecuteDataSet(CommandType.Text, sql);

        }
        public DataSet getSavedCompanies()
        {
            try
            {
                string str = "select Name,id,Address,City from Company";
                return ReturnDs(str);
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }


        #endregion

        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mFirstName = ((object)rw["FirstName"] == DBNull.Value) ? "" : rw["FirstName"].ToString();
            mLastName = ((object)rw["LastName"] == DBNull.Value) ? "" : rw["LastName"].ToString();
            mCompany = ((object)rw["Company"] == DBNull.Value) ? "" : rw["Company"].ToString();
            mDesignation = ((object)rw["Designation"] == DBNull.Value) ? "" : rw["Designation"].ToString();
            mRsvpStatus = ((object)rw["mRsvpStatus"] == DBNull.Value) ? false : bool.Parse(rw["mRsvpStatus"].ToString());
            mRsvpDate = ((object)rw["mRsvpDate"] == DBNull.Value) ? "" : rw["mRsvpDate"].ToString(); 
            mPhoneNumber = ((object)rw["PhoneNumber"] == DBNull.Value) ? "" : rw["PhoneNumber"].ToString();
            mEmail = ((object)rw["Email"] == DBNull.Value) ? "" : rw["Email"].ToString();
            mEventID = ((object)rw["EventID"] == DBNull.Value) ? 0 : int.Parse(rw["EventID"].ToString());
            mMemberType = ((object)rw["MembershipType"] == DBNull.Value) ? "" : rw["MembershipType"].ToString();
            mCompanyID = ((object)rw["CompanyID"] == DBNull.Value) ? 0 : int.Parse(rw["CompanyID"].ToString());
            mPaymentStatus = ((object)rw["PaymentStatus"] == DBNull.Value) ? false : bool.Parse(rw["PaymentStatus"].ToString());
            mAccomodation = ((object)rw["Accomodation"] == DBNull.Value) ? "" : rw["Accomodation"].ToString();
            mTransport = ((object)rw["Transport"] == DBNull.Value) ? "" : rw["Transport"].ToString();
            mLogisticsCombo = ((object)rw["LogisticsCombo"] == DBNull.Value) ? 0 : int.Parse(rw["LogisticsCombo"].ToString());
            //mQRCode = ((object)rw["mQRCode"] == DBNull.Value) ? null : byte[].Parse(rw["QRCode"].ToString());
            mNationalID = ((object)rw["FullName"] == DBNull.Value) ? "" : rw["FullName"].ToString();

        }


        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID ", DbType.Int32, mID);
            db.AddInParameter(cmd, "@FirstName", DbType.String, mFirstName);
            db.AddInParameter(cmd, "@LastName", DbType.String, mLastName);
            db.AddInParameter(cmd, "@Company", DbType.String, mCompany);
            db.AddInParameter(cmd, "@Designation", DbType.String, mDesignation);
            db.AddInParameter(cmd, "@RsvpStatus", DbType.Boolean, mRsvpStatus);
            db.AddInParameter(cmd, "@RsvpDate", DbType.String, mRsvpDate);
            db.AddInParameter(cmd, "@PhoneNumber", DbType.String, mPhoneNumber);
            db.AddInParameter(cmd, "@Email", DbType.String, mEmail);
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);
            db.AddInParameter(cmd, "@MembershipType", DbType.String, mMemberType);
            db.AddInParameter(cmd, "@CompanyID", DbType.Int32, mCompanyID);
            db.AddInParameter(cmd, "@PaymentStatus", DbType.Boolean, mPaymentStatus);
            db.AddInParameter(cmd, "@Accomodation", DbType.String, mAccomodation);
            db.AddInParameter(cmd, "@Transport", DbType.String, mTransport);
            db.AddInParameter(cmd, "@LogisticsCombo", DbType.Int32, mLogisticsCombo);
            db.AddInParameter(cmd, "@QRCode", DbType.Binary, mQRCode);
            db.AddInParameter(cmd, "@NationalID", DbType.String, mNationalID);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_MemberRsvp");

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