using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Xml.Linq;
using static Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design.CommonDesignTime;

namespace AGMSystem.models
{
    public class Logistics
    {
        #region vars
        protected int mID;
        protected string mType;
        protected int mProjectID;
        protected int mMemberID;
        protected int mEventID;
        protected int mTransportID;
        protected int mAccomodationID;
        protected int mComboCapacity;

        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;

        protected long mObjectUserID;
        #endregion

        #region properties
        public int ID { get { return mID; } set { mID = value; } }
        public string Type { get { return mType; } set { mType = value; } }
        public int ProjectID { get { return mProjectID; } set { mProjectID = value; } }
        public int MemberID { get { return mMemberID; } set { mMemberID = value; } }
        public int EventID { get { return mEventID; } set { mEventID= value; } }
        public int TransportID { get { return mTransportID; } set { mTransportID = value;} }
        public int ComboCapacity { get { return mComboCapacity; } set { mComboCapacity = value;} }
        public int AccomodationID { get { return mAccomodationID; }
            set
            {
                mAccomodationID = value;
            } }

        #endregion
        public Logistics( string connectionName, long userId) 
        { 
            this.mConnectionName = connectionName;
            db = new DatabaseProviderFactory().Create(connectionName);
        }

        #region methods

        public DataSet getSavedAccoAndTrans(int eventID)
        {
            string str = "SELECT ID, Name,EventID,Capacity,Available from AGM_Accomodation where EventID =" + eventID + " union SELECT ID, Name,EventID,Capacity,Available from AGM_Transport where EventID =" + eventID + "  order by ID desc  ";
            return ReturnDs(str);
        }
        public DataSet GetLogisticsType()
        {
            string str = "select * from AGM_LogisticsType";
                return ReturnDs(str);
        }

        public DataSet GetLogistics(int eventID)
        {
            try
            {
                string str = "select * from AGM_Accomodation where EventID="+eventID+" Union select * from AGM_Transport where EventID="+eventID;
                return ReturnDs(str);
            }
            catch (Exception xc)
            {
                mMsgFlg = xc.Message;
                return null;
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
                string mMsgFlg = ex.Message;
                return null;
            }
        }

        public virtual void GenerateLogisticsParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@Type", DbType.String, mType);
            db.AddInParameter(cmd, "@ProjectID", DbType.Int32, mProjectID);
            db.AddInParameter(cmd, "@MemberID", DbType.Int32, mMemberID);
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);
            db.AddInParameter(cmd, "@TransportID", DbType.Int32, mTransportID);
            db.AddInParameter(cmd, "@AccomodationID", DbType.Int32, mAccomodationID);
        }
        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_Logistics");

            GenerateLogisticsParameters(ref db, ref cmd);


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
                mMsgFlg = ex.Message;
                return false;

            }

        }

        #endregion
    }
    #region Transport Class
    public class Transport
    {
        public int mID;
        public int mEventID;
        public string mName;
        protected int mCapacity;
        protected int mAvailable;
        protected double mPrice;
        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;

        protected long mObjectUserID;

        public int ID { get {  return mID; } set {  mID = value; } }
        public int EventID { get {  return mEventID; } set {  mEventID = value; } }
        public string Name { get { return mName; } set { mName = value; } }
        public double Price { get {  return mPrice; } set { mPrice = value; } }
        public int Capacity { get {  return mCapacity; } set { mCapacity = value; } }
        public int Available { get {  return mAvailable; } set { mAvailable = value; } }

        public Transport(string connectionName, long userId)
        {
            this.mConnectionName = connectionName;
            db = new DatabaseProviderFactory().Create(connectionName);
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
        #region methods
        public DataSet getSavedTransport( int eventID)
        {
            string str = "SELECT ID ,EventID,Name,Capacity,Price from AGM_Transport where EventID=" + eventID + "order by ID desc";
            return ReturnDs(str);
        }
        public virtual void GenerateTransportParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);
            db.AddInParameter(cmd, "@Name", DbType.String, mName);
            db.AddInParameter(cmd, "@Capacity", DbType.Int32, mCapacity);
            db.AddInParameter(cmd, "@Available", DbType.Int32, mAvailable);
            db.AddInParameter(cmd, "@Price", DbType.String, mPrice);

        }
        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_Transport");

            GenerateTransportParameters(ref db, ref cmd);


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
                mMsgFlg = ex.Message;
                return false;

            }

        }
        #endregion
    }
    #endregion
    #region Accomodation Class
    public class Accomodation
    {
        public int mID;
        public int mEventID;
        public string mName;
        protected int mCapacity;
        protected int mAvailable;
        protected double mPrice;
        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;

        protected long mObjectUserID;

        public int ID { get { return mID; } set { mID = value; } }
        public int EventID { get { return mEventID; } set { mEventID = value; } }
        public string Name { get { return mName; } set { mName = value; } }
        public double Price { get { return mPrice; } set { mPrice = value; } }
        public int Capacity { get { return mCapacity; } set { mCapacity = value; } }
        public int Available { get { return mAvailable; } set { mAvailable = value; } }

        public Accomodation(string connectionName, long userId)
        {
            this.mConnectionName = connectionName;
            db = new DatabaseProviderFactory().Create(connectionName);
        }

        #region methods
        public DataSet getSavedAccomodation(int eventID)
        {
            string str = "SELECT ID, Name,EventID,Capacity,Available from AGM_Accomodation where EventID ="+ eventID + "  order by ID desc  ";
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
        public virtual void GenerateTransportParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);
            db.AddInParameter(cmd, "@Name", DbType.String, mName);
            db.AddInParameter(cmd, "@Capacity", DbType.Int32, mCapacity);
            db.AddInParameter(cmd, "@Available", DbType.Int32, mAvailable);
            db.AddInParameter(cmd, "@Price", DbType.String, mPrice);

        }
        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_Accomodation");

            GenerateTransportParameters(ref db, ref cmd);


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
                mMsgFlg = ex.Message;
                return false;

            }

        }
        #endregion
    }
    #endregion

    #region Combo Class
    public class Combos
    {
        #region vars
        public int mID;
        public string mCombo;
        public double mPrice;
        public int mEventID;
        public int mTransportID;
        public int mAccomodationID;
        protected int mComboCapacity;


        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;
        protected long mObjectUserID;
        #endregion

        #region props
        public int ID { get { return mID; } set { mID = value; } }
        public string Combo { get { return mCombo; } set { mCombo = value; } }
        public double Price { get { return mPrice; } set { mPrice = value; } }
        public int EventID { get { return mEventID; } set { mEventID = value; } }
        public int TransportID { get { return mTransportID; } set { mTransportID = value; } }
        public int AccomodationID { get { return mAccomodationID; } set { mAccomodationID = value; } }
        public int ComboCapacity { get { return mComboCapacity; } set { mComboCapacity = value; } }

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
        #endregion
        #region constructor
        public Combos(string connectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            this.mConnectionName = connectionName;
            db = new DatabaseProviderFactory().Create(connectionName);
        }
        #endregion


        #region methods

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
        public DataSet GetCombos(int eventID)
        {
            string str = "select lc.ID,lc.Price,lc.ComboCapacity, ac.id as acID, agmt.ID as agmtID, ac.Name as Accomodation,ac.Available as AvailableAccomodation,agmt.Name as Transport,agmt.Available as AvailableTransport ,lc.ID,lc.Combo, lc.ComboCapacity from Logistics_Combos lc left join AGM_Accomodation ac on lc.AccomodationID=ac.ID left join AGM_Transport agmt on lc.TransportID=agmt.ID where ac.EventID=" + eventID;
            return ReturnDs(str);
        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@Combo", DbType.String, mCombo);
            db.AddInParameter(cmd, "@Price", DbType.Double, mPrice);
            db.AddInParameter(cmd, "@EventID", DbType.Int32, mEventID);
            db.AddInParameter(cmd, "@TransportID", DbType.Int32, mTransportID);
            db.AddInParameter(cmd, "@AccomodationID", DbType.Int32, mAccomodationID);
            db.AddInParameter(cmd, "@ComboCapacity", DbType.Int32, mComboCapacity);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_save_Logistics_Combos");

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
    #endregion
}