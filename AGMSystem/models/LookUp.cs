using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AGMSystem.models
{
    public class LookUp
    {
        #region vars
        protected string mMsgFlg;
        protected Database db;
        protected string mConnectionName;
        protected long mObjectUserID;
        #endregion
        #region props
        public string MsgFlg
        {
            get { return mMsgFlg; }
            set { mMsgFlg = value; }
        }
        #endregion
        #region Constructor
        public LookUp(string ConnectionName, long ObjectUserID)
        {
            mObjectUserID = ObjectUserID;
            mConnectionName = ConnectionName;
            db = new DatabaseProviderFactory().Create(ConnectionName);
        }
        #endregion
        #region methods


        public DataSet CheckEventValidations(int eventID, int memberID)
        {
            try
            {
                string str = "Select * from EventEvaluation where eventID=" + eventID + " and MemberID=" + memberID + "";
                return ReturnDs(str);

            }
            catch (Exception b)
            {
                MsgFlg = b.Message;
                return null;
            }
        }
       
        public DataSet GetRatings()
        {
            string str = "Select * from PresentationRating";
            return ReturnDs(str);
        }
        public DataSet GetEventEvaluations(int eventID)
        {
            string str = "select r.ID,FirstName,LastName,PensionFund,Email,PhoneNumber from RegistrationMembers r  where id in(select memberID from EventEvaluation where EventID=" + eventID + ") ";
            return ReturnDs(str);
        }
        public DataSet GetMemberEventEvaluations(int memberID)
        {
            string str = "select ee.ID,eq.Question,ee.Response from EventEvaluation ee inner join EventQuestions eq on ee.QuestionID=eq.ID  where MemberID= " + memberID + " ";
            return ReturnDs(str);
        }
        public DataSet GetMemberPresentationtEvaluations(int memberID)
        {
            string str = "select ee.ID,eq.Question,ee.Comment,ee.RatingID from PresentationEvaluation ee inner join PresentationQuestions eq on ee.QuestionID=eq.ID  where MemberID=" + memberID + " ";
            return ReturnDs(str);
        }
        public DataSet InsertPresenterEvaluation(int memberID, int questionID, int ratingID, string comment, int eventID, int presenterID)
        {
            string str = "insert into PresentationEvaluation(MemberID,QuestionID,RatingID,Comment,EventID,PresenterID) values(" + memberID + ","+ questionID + ","+ ratingID + ",'"+ comment + "',"+ eventID + ","+presenterID+")";
            return ReturnDs(str);
        }
        public DataSet InsertEventEvaluation(int memberID, int questionID, string response, int eventID)
        {
            string str = "insert into EventEvaluation(MemberID,QuestionID,Response,EventID) values(" + memberID + ","+ questionID + ",'"+ response + "',"+ eventID + ")";
            return ReturnDs(str);
        }
        public DataSet GetPresenterQuestions()
        {
            string str = "Select * from PresentationQuestions";
            return ReturnDs(str);
        }
        public DataSet GetEventQuestions()
        {
            string str = "Select * from EventQuestions";
            return ReturnDs(str);
        }
        public DataSet GetFilePath(int id)
        {
            string str = "Select FilePath from EmailAttachments where BroadcastMessagesListID="+id+";";
            return ReturnDs(str);
        }
        public DataSet GetUploads(bool ProcessID, long Batch)
        {
            string str = "select * from ClientsFileUpload where ProcessStatusID='" + ProcessID + "' and UploadBatchID='" + Batch + "'";
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
                mMsgFlg = ex.Message;
                return null;
            }
        }
        public DataSet GetUploadedEmployee(int empid)
        {
            try
            {
                string str = "SELECT TOP 1 * from EmployeesUploadBatch where EmployerID='" + empid + "' order by id DESC";
                return ReturnDs(str);
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return null;
            }
        }
        public bool ValidateMemberIDNumber(string IDNumber)
        {
            try
            {
                string str = "SELECT top 1 * FROM member where IdentityNo='" + IDNumber + "' ";
                if (ReturnDs(str) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                mMsgFlg = ex.Message;
                return false;
            }
        }
        public DataSet getclientuploaded(long UploadID)
        {
            try
            {
                string str = "SELECT [ID],[EmployerName],[Surname],[Forenames],[IDNumber],[Gender],[DateOfBirth] FROM ClientsFileUpload where UploadBatchID = '" + UploadID + "' and ProcessStatusID = 0 ";
                return ReturnDs(str);
            }
            catch (Exception e)
            {

                MsgFlg = e.Message;
                return null;
            }
            
        }
        public DataSet GetFailedMemberUploads()
        {
            string str = "select * from FailedMemberUploads ";
            return ReturnDs(str);
        }
        #endregion

    }
}