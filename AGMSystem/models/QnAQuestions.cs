using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AGMSystem.models
{
    public class QnAQuestions
    {
        #region "Variables"

        protected string mMsgFlg;
        protected long mID;
        protected long mQnAParticipantID;
        protected long mResponseStatusID;
        protected long mCreatedBy;
        protected string mDateCreated;
        protected string mQuestion;

        protected string mAnswerResponse;
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

        public long QnAParticipantID
        {
            get { return mQnAParticipantID; }
            set { mQnAParticipantID = value; }
        }

        public long ResponseStatusID
        {
            get { return mResponseStatusID; }
            set { mResponseStatusID = value; }
        }

        public long CreatedBy
        {
            get { return mCreatedBy; }
            set { mCreatedBy = value; }
        }

        public string DateCreated
        {
            get { return mDateCreated; }
            set { mDateCreated = value; }
        }

        public string Question
        {
            get { return mQuestion; }
            set { mQuestion = value; }
        }

        public string AnswerResponse
        {
            get { return mAnswerResponse; }
            set { mAnswerResponse = value; }
        }

        #endregion

        #region "Methods"

        #region "Constructors"


        public QnAQuestions(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);

        }

        #endregion


        public void Clear()
        {
            ID = 0;
            mQnAParticipantID = 0;
            mResponseStatusID = 0;
            mCreatedBy = mObjectUserID;
            mDateCreated = "";
            mQuestion = "";
            mAnswerResponse = "";

        }

        #region "Retrieve Overloads"

        public virtual bool Retrieve()
        {

            return this.Retrieve(mID);

        }
        public bool UpdateResponse(int ID, string res)
        {
            try
            {
                string str = "update QnAQuestions set AnswerResponse='" + res + "' ,ResponseStatusID = 2 where ID=" + ID + "";
                db.ExecuteNonQuery(CommandType.Text, str);
                return true;
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return false;

            }
        }
        public DataSet getSelectedQuestion(int ID)
        {
            try
            {
                string str = "select QnAQ.ID,QP.QName,QP.Company,QnAQ.Question,QnAQ.AnswerResponse,QnAQ.DateCreated from QnAQuestions QnAQ inner join QnAParticipants QP on QP.ID = QnAQ .QnAParticipantID where QnAQ.ID =" + ID + "";
                DataSet ds = db.ExecuteDataSet(CommandType.Text, str);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;


                }
                else { return null; }
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
                sql = "SELECT * FROM QnAQuestions WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM QnAQuestions WHERE ID = " + mID;
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
                    SetErrorDetails("QnAQuestions not found.");

                    return false;

                }


            }
            catch (Exception e)
            {
                SetErrorDetails(e.Message);
                return false;

            }

        }

        public virtual System.Data.DataSet GetQnAQuestions()
        {

            return GetQnAQuestions(mID);

        }

        public virtual DataSet GetQnAQuestions(long ID)
        {

            string sql = null;

            if (ID > 0)
            {
                sql = "SELECT * FROM QnAQuestions WHERE ID = " + ID;
            }
            else
            {
                sql = "SELECT * FROM QnAQuestions WHERE ID = " + mID;
            }

            return GetQnAQuestions(sql);

        }

        protected virtual DataSet GetQnAQuestions(string sql)
        {

            return db.ExecuteDataSet(CommandType.Text, sql);

        }

        #endregion

        public DataSet getSavedQuestionsForParticipantsPendingResponse()
        {
            try
            {
                string str = "select QA.ID,QAP.QName,QAP.Company,QA.Question,QA.AnswerResponse from QnAQuestions QA inner join QnAParticipants QAP ON QAP.ID = QA.QnAParticipantID where ResponseStatusID=1 order by QA.ID desc";
                DataSet dsRetrieve = db.ExecuteDataSet(CommandType.Text, str);


                if (dsRetrieve != null && dsRetrieve.Tables.Count > 0 && dsRetrieve.Tables[0].Rows.Count > 0)
                {


                    return dsRetrieve;



                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                SetErrorDetails(ex.Message);
                return null;
            }
        }

        public DataSet getSavedQuestionsForParticipants(int ParticipantID)
        {
            try
            {
                string str = "select ID,Question,case ResponseStatusID when 1 then 'Pending Response' when 2 then 'Responded To' else 'To be attended to' end as ResponseStatus,DateCreated,AnswerResponse from QnAQuestions where QnAParticipantID =" + ParticipantID + "";
                DataSet dsRetrieve = db.ExecuteDataSet(CommandType.Text, str);


                if (dsRetrieve != null && dsRetrieve.Tables.Count > 0 && dsRetrieve.Tables[0].Rows.Count > 0)
                {


                    return dsRetrieve;



                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                SetErrorDetails(ex.Message);
                return null;
            }
        }

        protected internal virtual void LoadDataRecord(DataRow rw)
        {


            mID = ((object)rw["ID"] == DBNull.Value) ? 0 : int.Parse(rw["ID"].ToString());
            mQnAParticipantID = ((object)rw["QnAParticipantID"] == DBNull.Value) ? 0 : int.Parse(rw["QnAParticipantID"].ToString());
            mResponseStatusID = ((object)rw["ResponseStatusID"] == DBNull.Value) ? 0 : int.Parse(rw["ResponseStatusID"].ToString());
            mCreatedBy = ((object)rw["CreatedBy"] == DBNull.Value) ? 0 : int.Parse(rw["CreatedBy"].ToString());
            mDateCreated = ((object)rw["DateCreated"] == DBNull.Value) ? "" : (rw["DateCreated"].ToString());
            mQuestion = ((object)rw["Question"] == DBNull.Value) ? "" : rw["Question"].ToString();
            mAnswerResponse = ((object)rw["AnswerResponse"] == DBNull.Value) ? "" : rw["AnswerResponse"].ToString();


        }

        #region "Save"


        public virtual void GenerateSaveParameters(ref Database db, ref System.Data.Common.DbCommand cmd)
        {
            db.AddInParameter(cmd, "@ID", DbType.Int32, mID);
            db.AddInParameter(cmd, "@QnAParticipantID", DbType.Int32, mQnAParticipantID);
            db.AddInParameter(cmd, "@ResponseStatusID", DbType.Int32, mResponseStatusID);
            db.AddInParameter(cmd, "@DateCreated", DbType.String, mDateCreated);
            db.AddInParameter(cmd, "@Question", DbType.String, mQuestion);
            db.AddInParameter(cmd, "@AnswerResponse", DbType.String, mAnswerResponse);

        }

        public virtual bool Save()
        {

            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("sp_Save_QnAQuestions");

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

            //Return Delete("UPDATE QnAQuestions SET Deleted = 1 WHERE ID = " & mID) 
            return Delete("DELETE FROM QnAQuestions WHERE ID = " + mID);

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

        protected void SetErrorDetails(string str)
        {
            mMsgFlg = str;
        }

        #endregion

        #endregion

    }
}