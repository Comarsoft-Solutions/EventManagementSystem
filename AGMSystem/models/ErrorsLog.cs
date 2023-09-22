using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGMSystem.models
{
    public class ErrorsLog
    {
        public void SetErrorDetails(string v)
        {
            throw new NotImplementedException();
        }
        public static object Catchnull(object objOld, object objNew, bool CheckEmptyString = false)
        {

            if (objOld == DBNull.Value || objOld == null)
            {
                return objNew;
            }
            else
            {
                if (CheckEmptyString)
                {
                    if (string.IsNullOrEmpty(objOld.ToString()))
                    {
                        return objNew;
                    }
                    else
                    {
                        return objOld;
                    }
                }
                else
                {
                    return objOld;
                }
            }

        }
    }
}