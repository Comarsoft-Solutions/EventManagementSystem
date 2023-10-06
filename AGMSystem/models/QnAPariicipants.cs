using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AGMSystem.models
{
    public class QnAPariicipants
    {
        #region "Variables"

        protected long mID;
        protected string mQName;
        protected string mCompany;

        protected string mMsgFlg;
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

        public string QName
        {
            get { return mQName; }
            set { mQName = value; }
        }

        public string Company
        {
            get { return mCompany; }
            set { mCompany = value; }
        }

        #endregion

        #region "Methods"

        #region "Constructors"


        public QnAPariicipants(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);

        }

        #endregion


        public void Clear()
        {
            ID = 0;
            mQName = "";
            mCompany = "";

        }

        #region "Retrieve Overloads"

        public virtual bool Retrieve()
        {

            return this.Retrieve(mID);

        }
        public DataSet ValidateExistanceOfName(string Person, string Company)
        {
            try
            {
                string str = "select * from QnAParticipants where replace(QName,' ','')  like replace('%" + Person + " %',' ','') and  replace(Company,' ','') like replace('%" + Company + "%',' ','')";
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
            catch (Exception e)
            {
                mMsgFlg = e.Message;
                return null;
            }
        }

        public virtual bool Retrieve(long ID)
        {

            string sql = null;

            if (ID > 0)
            {
                sql = "SELECT * FROM QnAParticipants WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM QnAParticipants WHERE ID = " + mID;
            }

            return Retrieve(sql);

        }
        protected void SetErrorDetails(string str)
        {
            mMsgFlg = str;
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
                    SetErrorDetails("QnAPariicipants not found.");

                    return false;

                }


            }
            catch (Exception e)
            {
                SetErrorDetails(e.Message);
                return false;

            }

        }

        public virtual System.Data.DataSet GetQnAPariicipants()
        {

            return GetQnAPariicipants(mID);

        }

        public virtual DataSet GetQnAPariicipants(long ID)
        {

            string sql = null;

            if (ID > 0)
            {
                sql = "SELECT * FROM QnAParticipants WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM QnAParticipants WHERE ID = " + mID;
            }

            return GetQnAPariicipants(sql);

        }

        protected virtual DataSet GetQnAPariicipants(string sql)
        {

            return db.ExecuteDataSet(CommandType.Text, sql);

        }

        #endregion


        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mQName = ((object)rw["QName"] == DBNull.Value) ? string.Empty : rw["QName"].ToString();
            mCompany = ((object)rw["Company"] == DBNull.Value) ? string.Empty : rw["Company"].ToString();


        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@QName", DbType.String, mQName);
            db.AddInParameter(cmd, "@Company", DbType.String, mCompany);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_QnAPariicipants");

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

            //Return Delete("UPDATE QnAParticipants SET Deleted = 1 WHERE ID = " & mID) 
            return Delete("DELETE FROM QnAParticipants WHERE ID = " + mID);

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