using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AGMSystem.models
{

    public class logins
    {
        #region vars
        Menu objMenu;
        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;
        protected DataSet mMemberDs;
        protected long mUserID;
        protected string mUserAction;
        protected string mUserName;
        protected string mMessage;
        protected string mIpAddress;
        #endregion

        #region props
        public DataSet MemberDs
        {
            get { return mMemberDs; }
            set { mMemberDs = value; }
        }
        public string UserAction
        {
            get { return mUserAction; }
            set { mUserAction = value; }
        }
        public long UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }
        public string UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }
        public string IpAddress
        {
            get { return mIpAddress; }
            set { mIpAddress = value; }
        }
        public string Message
        {
            get { return mMessage; }
            set { mMessage = value; }
        }


        protected long mObjectUserID;
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
        #endregion

        #region constructor
        public logins(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);
        }
        #endregion

        public DataSet GetBusinessActivitiesByIndustry(int IndID)

        {
            try
            {
                string str = "select ID,ClassCode from CountryIndustryBusinessClasses where IndustryID = " + IndID + "";
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


        public DataSet getAllLogins()
        {
            try
            {

                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_get_Logins");
                DataSet ds = db.ExecuteDataSet(cmd);
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

        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@username", DbType.String, mUserName);
            db.AddInParameter(cmd, "@useraction", DbType.String, mUserAction);
            db.AddInParameter(cmd, "@message", DbType.String, mMessage);
            db.AddInParameter(cmd, "@ipaddress", DbType.String, mIpAddress);
        }
        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Login_Attempts_Portal");

            GenerateSaveParameters(ref db, ref cmd);


            try
            {

                DataSet ds = db.ExecuteDataSet(cmd);



                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    mUserID = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                }

                return true;


            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return false;

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
                mMsgFlg = ex.Message;
                return null;
            }
        }

    }
}