using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AGMSystem
{
    public class RegistrationSave
    {

        #region "Variables"

        protected long mId;
        protected string mTshirt;
        protected string mPensionFund;
        protected string mMemberAddress;
        protected long mEventID;
        protected long mRegTypeID;
        protected string mDateAdded;
        protected bool mIsApproved;
        protected bool mGolf;
        protected string mFirstName;
        protected string mLastName;
        protected string mCompany;
        protected string mDesignation;
        protected string mPhoneNumber;
        protected string mEmail;
        protected string mNationalID;
        protected string mMsgFlg;
        protected int mCompanyID;
        protected string mMemberType;


        protected Database db;
        protected string mConnectionName;

        protected long mObjectUserID;
        #endregion

        #region "Properties"
        public int CompanyID
        {
            get { return mCompanyID; }
            set { mCompanyID = value; }
        }
        public string Tshirt
        {
            get { return mTshirt; }
            set { mTshirt = value; }
        }
        public bool Golf
        {
            get { return mGolf; }
            set { mGolf = value; }
        }
        public string MemberAddress
        {
            get { return mMemberAddress; }
            set { mMemberAddress = value; }
        }
        public string PensionFund
        {
            get { return mPensionFund; }
            set { mPensionFund = value; }
        }
        public string MemberType
        {
            get { return mMemberType; }
            set { mMemberType = value; }
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

        public long Id
        {
            get { return mId; }
            set { mId = value; }
        }

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

        public string FirstName
        {
            get { return mFirstName; }
            set { mFirstName = value; }
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

        public string Designation
        {
            get { return mDesignation; }
            set { mDesignation = value; }
        }

        public string PhoneNumber
        {
            get { return mPhoneNumber; }
            set { mPhoneNumber = value; }
        }

        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }

        public string NationalID
        {
            get { return mNationalID; }
            set { mNationalID = value; }
        }

        #endregion
        #region "Constructors"


        public RegistrationSave(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);

        }

        #endregion

        #region "Methods"

        public DataSet GetMembersBySearch(string firstname="",string lastname="",string nationalID="")
        {
            
            try
            {
                string str = "";

                if (firstname.Length>0 && lastname.Length<=0 && nationalID.Length<=0)
                {
                    str = "SELECT * from RegistrationMembers where FirstName like '%"+firstname+"%'";
                }
                if (firstname.Length<=0 && lastname.Length>0 && nationalID.Length<=0)
                {
                    str = "SELECT * from RegistrationMembers where LastName like '%"+lastname+"%'";
                }
                if (firstname.Length<=0 && lastname.Length<=0 && nationalID.Length>0)
                {
                    str = "SELECT * from RegistrationMembers where FirstName like '%"+nationalID+"%'";
                }
                if (firstname.Length>0 && lastname.Length>0 && nationalID.Length<=0)
                {
                    str = "SELECT * from RegistrationMembers where FirstName like '%"+firstname+"%' and LastName like '%"+lastname+"%'";
                }
                if (firstname.Length>0 && lastname.Length>0 && nationalID.Length>0)
                {
                    str = "SELECT * from RegistrationMembers where FirstName like '%"+firstname+"%' and LastName like '%"+lastname+ "%' and NationalID like '%"+nationalID+"%'";
                }
               return ReturnDs(str);
            }
            catch (Exception ex)
            {

                Msgflg=ex.Message;
                return null;
            }
        }

        public DataSet GetPrintOptions()
        {
            String str = "SELECT * from PrintOptions";
            return ReturnDs(str);
        }
        public DataSet GetMembersByCompany(string company="")
        {
            
            try
            {
                string str = "";

                if (company.Length>0)
                {
                    str = "SELECT * from RegistrationMembers where PensionFund like '%" + company+"%'";
                }
               return ReturnDs(str);
            }
            catch (Exception ex)
            {

                Msgflg=ex.Message;
                return null;
            }
        }
        public DataSet getDesignation()
        {
            try
            {
                string s = "Select * from Designation";
                return ReturnDs(s);
            }
            catch (Exception x)
            {

                Msgflg = x.Message;
                return null;
            }
        }
        protected DataSet ReturnDsNew(string str )
        {
            try
            {
                DataSet ds = db.ExecuteDataSet(CommandType.Text, str);
                if (ds!= null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ) {

                    return ds;
                }
                else
                {
                    return null;    
                }
            }
            catch (Exception ex) {
                mMsgFlg = ex.Message;
                return null;    
                    }
        }
        public DataSet getSavedCompanies()
        {
            try
            {
                string str = "select distinct(Firstname) as company,id from RegistrationMembers where MembershipType='Company'";
                return ReturnDsNew(str);
            }
            catch (Exception ex) {
                mMsgFlg = ex.Message;
                return null;
            }
        }
        public void Clear()
        {

            mId = 0;
            mEventID = 0;
            mRegTypeID = 0;
            mDateAdded = "";
            mIsApproved = false;
            mFirstName = "";
            mLastName = "";
            mCompany = "";
            mDesignation = "";
            mPhoneNumber = "";
            mEmail = "";
            //mNationalID = "";
            mMemberType = "";
            mMemberType = "";

        }
        public DataSet CheckPresenter(int eventID, int memberID)
        {
            string str = "Select * from RegistrationMembers where ID in (select id from Presenter where EventID=" + eventID + " and MemberID =" + memberID + ")";
            return ReturnDs(str);
        }

        #region "Retrieve Overloads"

        public DataSet  GetRegInfo(string mNationalID)
        {
            string sql = "select * from RegistrationMembers  WHERE NationalID = '" + mNationalID+"'";
            return ReturnDs(sql);
        }
        public DataSet  GetMembership()
        {
            string sql = "select * from RegistrationMembers ";
            return ReturnDs(sql);
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

        public virtual bool Retrieve()
        {

            return this.Retrieve(mId);

        }

        public virtual bool Retrieve(long Id)
        {

            string sql = null;

            if (Id > 0)
            {
                sql = "SELECT * FROM RegistrationMembers WHERE Id = " + Id;
            }
            else
            {
                sql = "SELECT * FROM RegistrationMembers WHERE Id = " + mId;
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

        public DataSet GetAllMembers(int projectID)
        {
            try
            {
                string str = "Select * from RegistrationMembers where ID not in (select MemberID from AGM_Project_Members where ProjectID="+ projectID + " )";
                return ReturnDs(str);
            }
            catch (Exception ex)
            {

                Msgflg =ex.Message;
                return null;
            }
        }
        public DataSet GetSomeMembers(int projectID)
        {
            try
            {
                string str = "Select * from RegistrationMembers where ID in (select MemberID from AGM_Project_Members where ProjectID ="+projectID+" )";
                return ReturnDs(str);
            }
            catch (Exception ex)
            {

                Msgflg =ex.Message;
                return null;
            }
        }

        public virtual System.Data.DataSet GetRegistration()
        {

            return GetRegistration(mId);

        }

        public virtual DataSet GetRegistration(long Id)
        {

            string sql = null;

            if (Id > 0)
            {
                sql = "SELECT * FROM RegistrationMembers WHERE Id = " + Id;
            }
            else
            {
                sql = "SELECT * FROM RegistrationMembers WHERE Id = " + mId;
            }

            return GetRegistration(sql);

        }

        protected virtual DataSet GetRegistration(string sql)
        {

            return db.ExecuteDataSet(CommandType.Text, sql);

        }


        #endregion


        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mId = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mEventID = ((object)rw["EventID"] == DBNull.Value) ? 0 : int.Parse(rw["EventID"].ToString());
            mRegTypeID = ((object)rw["RegTypeID"] == DBNull.Value) ? 0 : int.Parse(rw["RegTypeID"].ToString());
            mDateAdded = ((object)rw["DateAdded"] == DBNull.Value) ? "" : rw["DateAdded"].ToString();
            mIsApproved = ((object)rw["IsApproved"] == DBNull.Value) ? false : bool.Parse(rw["IsApproved"].ToString());
            mFirstName = ((object)rw["FirstName"] == DBNull.Value) ? "" : rw["FirstName"].ToString();
            mLastName = ((object)rw["LastName"] == DBNull.Value) ? "" : rw["LastName"].ToString();
            mCompany = ((object)rw["Company"] == DBNull.Value) ? "" : rw["Company"].ToString();
            mDesignation = ((object)rw["Designation"] == DBNull.Value) ? "" : rw["Designation"].ToString();
            mPhoneNumber = ((object)rw["PhoneNumber"] == DBNull.Value) ? "" : rw["PhoneNumber"].ToString();
            mEmail = ((object)rw["Email"] == DBNull.Value) ? "" : rw["Email"].ToString();
            //mNationalID = ((object)rw["NationalID"] == DBNull.Value) ? "" : rw["NationalID"].ToString();
            mMemberType = ((object)rw["MembershipType"] ==DBNull.Value)? "" : rw["MembershipType"].ToString() ;
            mCompanyID = ((object)rw["CompanyID"] == DBNull.Value) ? 0 : int.Parse( rw["CompanyID"].ToString());

        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@Id", DbType.Int32, mId);
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);
            db.AddInParameter(cmd, "@RegTypeID", DbType.Int32, mRegTypeID);
            db.AddInParameter(cmd, "@DateAdded", DbType.String, mDateAdded);
            db.AddInParameter(cmd, "@IsApproved", DbType.Boolean, mIsApproved);
            db.AddInParameter(cmd, "@FirstName", DbType.String, mFirstName);
            db.AddInParameter(cmd, "@LastName", DbType.String, mLastName);
            db.AddInParameter(cmd, "@Company", DbType.String, mCompany);
            db.AddInParameter(cmd, "@CompanyID", DbType.Int32, mCompanyID);
            db.AddInParameter(cmd, "@Designation", DbType.String, mDesignation);
            db.AddInParameter(cmd, "@PhoneNumber", DbType.String, mPhoneNumber);
            db.AddInParameter(cmd, "@Email", DbType.String, mEmail);
            db.AddInParameter(cmd, "@NationalID", DbType.String, mNationalID);
            db.AddInParameter(cmd, "@MemberType", DbType.String, mMemberType);
            db.AddInParameter(cmd, "@Tshirt", DbType.String, mTshirt);
            db.AddInParameter(cmd, "@PensionFund", DbType.String, mPensionFund);
            db.AddInParameter(cmd, "@MemberAddress", DbType.String, mMemberAddress);
            //db.AddInParameter(cmd, "@Golf", DbType.Boolean, mGolf);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_Registration");

            GenerateSaveParameters(ref db, ref cmd);


            try
            {
                DataSet ds = db.ExecuteDataSet(cmd);


                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    mId = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

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

        #region "Delete"

        public virtual bool Delete()
        {

            //Return Delete("UPDATE RegistrationMembers SET Deleted = 1 WHERE Id = " & mId) 
            return Delete("DELETE FROM RegistrationMembers WHERE Id = " + mId);

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

    public class ProjectMembers
    {

        protected string mMsgFlg;

        protected Database db;
        protected string mConnectionName;

        protected long mObjectUserID;
        protected int mID;
        protected long mMemberID;
        protected int mProjectID;


        #region props
        public Database Database
        {
            get { return db; }
        }
        public int Id
        {
            get { return mID; }
            set { mID = value; }
        }
        public long MemberId
        {
            get { return mMemberID; }
            set { mMemberID = value; }
        }
        public int ProjectID
        {
            get { return mProjectID; }
            set { mProjectID = value; }
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


        #endregion
        public ProjectMembers(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);
        }


        #region methods

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@ProjectID", DbType.Int32, mProjectID);
            db.AddInParameter(cmd, "@MemberID", DbType.Int32, mMemberID);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_ProjectMembership");

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

    public  class CompanyRegistration
    {
        #region variables
        protected Database db;
        protected string mConnectionName;
        protected long mObjectUserID;
        protected string mMsgFlg;
        protected int mID;
        protected string mName;
        protected string mAddress;
        protected string mCity;
        protected string mZipCode;

        #endregion

        #region properties
        public int ID
        {
            get { return mID; }
            set { mID = value; }
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
        public string Name { get { return mName; } set { mName = value; } }
        public string Address { get { return mAddress; } set { mAddress = value; } }
        public string City { get { return mCity; } set { mCity = value; } }
        public string ZipCode { get { return mZipCode; } set { mZipCode = value; } }

        #endregion

        #region constructor
        public CompanyRegistration(string ConnectionName, long userObjectID)
        {
            mObjectUserID = userObjectID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);
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

        public DataSet GetRegMembers()
        {
            try
            {
                string str = "Select * from RegistrationMembers";
                return ReturnDs(str);
            }
            catch (Exception xc)
            {
                Msgflg = xc.Message;
                throw;
            }
        }

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

        protected virtual DataSet GetRegistration(string sql)
        {

            return db.ExecuteDataSet(CommandType.Text, sql);

        }
        public DataSet getSavedCompanies()
        {
            try
            {
                string str = "select * from Company";
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
            mName = ((object)rw["Name"] == DBNull.Value) ? "" : (rw["Name"].ToString());
            mAddress = ((object)rw["Address"] == DBNull.Value) ? "" : (rw["Address"].ToString());
            mCity = ((object)rw["City"] == DBNull.Value) ? "" : (rw["City"].ToString());
            mZipCode = ((object)rw["ZipCode"] == DBNull.Value) ? "" : rw["ZipCode"].ToString();

        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@Name", DbType.String, mName);
            db.AddInParameter(cmd, "@Address", DbType.String, mAddress);
            db.AddInParameter(cmd, "@City", DbType.String, mCity);
            //db.AddInParameter(cmd, "@ZipCode", DbType.String, mZipCode);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_Company");

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