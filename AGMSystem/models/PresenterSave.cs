using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

        #endregion
    }
}