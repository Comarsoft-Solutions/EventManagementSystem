using Microsoft.Ajax.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AGMSystem
{
    public class AGMQueries
    {
        #region "Variables"

        protected long mID;
        protected long mEventID;
        protected long mRegistrationID;
        protected string mDateCreated;
        protected bool misSolved;
        protected string mQuery;
        protected string mQueryType;
        protected string mComment;
        protected string mActionType;
        protected string mMsgflg;
        protected Database db;
        protected string mConnectionName;

        protected long mObjectUserID;
        #endregion

        #region "Properties"
        public string MsgFlg
        {
            get { return mMsgflg; }
            set { mMsgflg = value; }
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

        public long EventID
        {
            get { return mEventID; }
            set { mEventID = value; }
        }

        public long RegistrationID
        {
            get { return mRegistrationID; }
            set { mRegistrationID = value; }
        }

        public string DateCreated
        {
            get { return mDateCreated; }
            set { mDateCreated = value; }
        }

        public bool isSolved
        {
            get { return misSolved; }
            set { misSolved = value; }
        }

        public string Query
        {
            get { return mQuery; }
            set { mQuery = value; }
        }

        public string QueryType
        {
            get { return mQueryType; }
            set { mQueryType = value; }
        }

        public string Comment
        {
            get { return mComment; }
            set { mComment = value; }
        }

        public string ActionType
        {
            get { return mActionType; }
            set { mActionType = value; }
        }

        #endregion

        #region "Methods"

        #region "Constructors"


        public AGMQueries(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);

        }

        #endregion


        public void Clear()
        {
            ID = 0;
            mEventID = 0;
            mRegistrationID = 0;
            mDateCreated = "";
            misSolved = false;
            mQuery = "";
            mQueryType = "";
            mComment = "";
            mActionType = "";

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
                sql = "SELECT * FROM AGMQueries WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM AGMQueries WHERE ID = " + mID;
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
                    mMsgflg = "AGMQueries not found.";

                    return false;

                }


            }
            catch (Exception e)
            {
                mMsgflg = e.Message;
                return false;

            }

        }

        public virtual System.Data.DataSet GetAGMQueries()
        {

            return GetAGMQueries(mID);

        }

        public DataSet getEnquiriesByFilterSearch(string firstnames = "", string lastname = "", string idnumber = "")
        {
            string str = "";

            if (firstnames.Length > 0 && lastname.Length <= 0 && idnumber.Length <= 0)
            {

                str = "select q.ID,m.firstName,m.LastName,m.NationalID,m.PhoneNumber,m.Email,m.Category,m.Company,q.DateCreated,q.isSolved from AGMQueries q inner join RegistrationMembers m on q.RegistrationID=m.Id  where firstName like '%" + firstnames + "%' ";

            }
            if (firstnames.Length > 0 && lastname.Length > 0 && idnumber.Length <= 0)
            {
                str = "select q.ID,m.firstName,m.LastName,m.NationalID,m.PhoneNumber,m.Email,m.Category,m.Company,q.DateCreated,q.isSolved from AGMQueries q inner join RegistrationMembers m on q.RegistrationID=m.Id  where firstName like '%" + firstnames + "%' and m.LastName like '%" + lastname + "%' ";

            }
            if (firstnames.Length > 0 && lastname.Length > 0 && idnumber.Length > 0)
            {
                str = "select q.ID,m.firstName,m.LastName,m.NationalID,m.PhoneNumber,m.Email,m.Category,m.Company,q.DateCreated,q.isSolved from AGMQueries q inner join RegistrationMembers m on q.RegistrationID=m.Id  where firstName like '%" + firstnames + "%' and m.LastName like '%" + lastname + "%' and NationalID like '" + idnumber + "%'";
            }
            if (firstnames.Length <= 0 && lastname.Length > 0 && idnumber.Length <= 0)
            {
                str = "select q.ID,m.firstName,m.LastName,m.NationalID,m.PhoneNumber,m.Email,m.Category,m.Company,q.DateCreated,q.isSolved from AGMQueries q inner join RegistrationMembers m on q.RegistrationID=m.Id  where m.LastName like '%" + lastname + "%' ";

            }
            if (firstnames.Length <= 0 && lastname.Length <= 0 && idnumber.Length > 0)
            {
                str = "select q.ID,m.firstName,m.LastName,m.NationalID,m.PhoneNumber,m.Email,m.Category,m.Company,q.DateCreated,q.isSolved from AGMQueries q inner join RegistrationMembers m on q.RegistrationID=m.Id  where  NationalID like '" + idnumber + "%'";

            }
            if (firstnames.Length <= 0 && lastname.Length <= 0 && idnumber.Length <= 0)
            {
                GetEnquiries();
            }

            return ReturnDs(str);
        }

        public DataSet getEnquiriesBySearch(string firstname = "", string lastname = "", string company =""  ) 
        {
            string str = "";
            if (firstname.Length>0 && lastname.Length<=0 && company.Length<=0)
            {
                str = "select ID,NationalID, firstName,LastName,PhoneNumber,Email,Company,Designation,PensionFund from RegistrationMembers where firstName like '%" + firstname + "%'";
            }
            if (firstname.Length <= 0 && lastname.Length > 0 && company.Length <= 0)
            {
                str = "select ID, firstName,NationalID,LastName,PhoneNumber,Email,Company,Designation,PensionFund from RegistrationMembers where LastName like '%" + lastname + "%'";
            }
            if (firstname.Length <= 0 && lastname.Length <= 0 && company.Length > 0)
            {
                str = "select ID, firstName,NationalID,LastName,PhoneNumber,Email,Company,Designation,PensionFund from RegistrationMembers where Company like '%" + company + "%'";
            }
            if (firstname.Length > 0 && lastname.Length > 0 && company.Length > 0)
            {
                str = "select ID, firstName,NationalID,LastName,PhoneNumber,Email,Company,Designation,PensionFund from RegistrationMembers where firstName like '%" + firstname + "%' and LastName like '%"+ lastname +"%' and Company like '%" +company+ "%";
            }
            if (firstname.Length > 0 && lastname.Length > 0 && company.Length <= 0)
            {
                str = "select ID, firstName,LastName,NationalID,PhoneNumber,Email,Company,Designation,PensionFund from RegistrationMembers where firstName like '%" + firstname + "%' and LastName like '%" + lastname + "%' ";
            }
            if (firstname.IsNullOrWhiteSpace() && lastname.IsNullOrWhiteSpace() && company.IsNullOrWhiteSpace())
            {
                GetEnquiries();
            }
            return ReturnDs(str);
        }

        public DataSet GetRegTypes()
        {
            string str = "Select ID, Type from AGM_RegType";
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
                string mMsgFlg = ex.Message;
                return null;
            }
        }

        public virtual DataSet GetAGMQueries(long ID)
        {

            string sql = null;

            if (ID > 0)
            {
                sql = "SELECT * FROM AGMQueries WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM AGMQueries WHERE ID = " + mID;
            }

            return ReturnDS(sql);

        }

        public virtual DataSet GetEnquiries(int EventID)
        {
            //string cmd = "select q.ID,firstName,LastName,NationalID,q.DateCreated,case isSolved when 0 then 'Open' else 'Closed' end as isSolved from AGMQueries q inner join RegistrationMembers m on q.RegistrationID=m.Id where q.eventID="+EventID +"  ";

            string cmd = "select ID,FirstName,LastName,NationalID,Company,Email,PhoneNumber,Designation,isnull((select case isSolved when 0 then 'Open    ' else 'Closed' end from AGMQueries where RegistrationID = rm.ID and EventID=rm.EventID),'No Query Submitted' ) as QueryStatus from RegistrationMembers rm where EventID=" + EventID;

            return ReturnDS(cmd);

        }
        public virtual DataSet GetEnquiries()
        {
            string cmd = "select ID,FirstName,LastName,Company,Email,PensionFund,NationalID,PhoneNumber,Designation,case IsApproved when 0 then 'InActive' else 'Active' end as Status from RegistrationMembers ";
            return  db.ExecuteDataSet(CommandType.Text, cmd);

        }

        protected virtual DataSet ReturnDS(string sql)
        {

            return db.ExecuteDataSet(CommandType.Text, sql);

        }

        #endregion


        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mEventID = ((object)rw["EventID"] == DBNull.Value) ? 0 : int.Parse(rw["EventID"].ToString());
            mRegistrationID = ((object)rw["RegistrationID"] == DBNull.Value) ? 0 : int.Parse(rw["RegistrationID"].ToString());
            mDateCreated = ((object)rw["DateCreated"] == DBNull.Value) ? "" : rw["DateCreated"].ToString();
            misSolved = ((object)rw["isSolved"] == DBNull.Value) ? false : bool.Parse(rw["isSolved"].ToString());
            mQuery = ((object)rw["Query"] == DBNull.Value) ? "" : rw["Query"].ToString();
            mQueryType = ((object)rw["QueryType"] == DBNull.Value) ? "" : rw["QueryType"].ToString();
            mComment = ((object)rw["Comment"] == DBNull.Value) ? "" : rw["Comment"].ToString();
            mActionType = ((object)rw["ActionType"] == DBNull.Value) ? "" : rw["ActionType"].ToString();


        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);
            db.AddInParameter(cmd, "@RegistrationID", DbType.Int32, mRegistrationID);
            db.AddInParameter(cmd, "@DateCreated", DbType.String, mDateCreated);
            db.AddInParameter(cmd, "@isSolved", DbType.Boolean, misSolved);
            db.AddInParameter(cmd, "@Query", DbType.String, mQuery);
            db.AddInParameter(cmd, "@QueryType", DbType.String, mQueryType);
            db.AddInParameter(cmd, "@Comment", DbType.String, mComment);
            db.AddInParameter(cmd, "@ActionType", DbType.String, mActionType);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_AGMQueries");

            GenerateSaveParameters(ref db, ref cmd);


            try
            {
                DataSet ds = db.ExecuteDataSet(cmd);


                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    mID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString().ToString());

                }

                return true;


            }
            catch (Exception ex)
            {
                mMsgflg = ex.Message;
                return false;

            }

        }

        #endregion

        #region "Delete"

        public virtual bool Delete()
        {

            //Return Delete("UPDATE AGMQueries SET Deleted = 1 WHERE ID = " & mID) 
            return Delete("DELETE FROM AGMQueries WHERE ID = " + mID);

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
                mMsgflg = e.Message;
                return false;

            }

        }

        #endregion

        #endregion

    }
}