using course_proj_english_tutorial.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Models
{
    /// <summary>
    /// базовая модель
    /// </summary>
    public class BaseModel
    {
        public Guid Id { get; set; }       
        public Exception Error { get; set; }
        public string ErrorInfo
        {
            get
            {
                if (Error != null)
                {
                    return Error.Message + (Error.InnerException != null ? ", " + Error.InnerException.Message : "");
                }
                else
                {
                    return "";
                }
            }
        }
        public DefaultEnums.Result Result { get; set; }
        public BaseModel()
        {
            Result = DefaultEnums.Result.ok;
        }
        public BaseModel(Exception exp)
        {
            Error = exp;
            Result = DefaultEnums.Result.error;
        }
        public static BaseModel ErrorFormat(Exception exp)
        {
            return new BaseModel(exp);
        }
    }

    public static class BaseModelUtilities<T>
        where T : BaseModel, new()
    {
        public static T DataFormat(T CurrentData)
        {
            CurrentData.Result = DefaultEnums.Result.ok;
            return CurrentData;
        }

        public static T ErrorFormat(Exception exp)
        {
            return new T() { Error = exp, Result = DefaultEnums.Result.error };
        }
    }
}
