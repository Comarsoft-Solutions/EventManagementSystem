using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AGMSystem.models
{
    public class PresenterSave
    {
        #region vars
        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;
        protected long mObjectUserID;

        protected int mID;
        protected int mEventID;
        protected string mFullName;
        protected string mCompany;
        protected byte mImage;
        #endregion
        #region props
        public string MsgFlg
        {
            get { return mMsgFlg; }
            set { mMsgFlg = value; }
        }
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
        public string FullName
        {
            get { return mFullName; }
            set { mFullName = value; }
        }
        public string Company
        {
            get { return mCompany; }
            set { mCompany = value; }
        }
        public byte Image
        {
            get { return mImage; }
            set { mImage = value; }
        }
        #endregion
        public PresenterSave(string ConnectionName, long ObjectUserID) 
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
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);
            db.AddInParameter(cmd, "@FullName", DbType.Int32, mFullName);
            db.AddInParameter(cmd, "@Company", DbType.String, mCompany);

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
        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_Presenter");

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
        public DataSet GetPresenter(int EventID)
        {
            string str = "Select * from Presenter where eventID="+ EventID +"";
            return ReturnDs(str);
        }
        #endregion
        #endregion
    }
}