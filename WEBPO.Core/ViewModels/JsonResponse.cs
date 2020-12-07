using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Core.ViewModels
{

    public static class JsonResponse
    {
        public static string Success(string message, object data, MessageInfoType returnType)
        {
           return JsonConvert.SerializeObject(new { 
                Success = true,
                Message = message,
                Data = data,
                ReturnType = returnType
            });
        }

        public static string Error(string message, object data, MessageInfoType returnType)
        {
            return JsonConvert.SerializeObject(new
            {
                Success = false,
                Message = message,
                Data = data,
                ReturnType = returnType
            });
        }

    }

    [Serializable]
    public enum MessageInfoType
    {
        Success = 1,
        Error = 2,
        Warning = 3,
        Info = 4,
        Question = 5
    }

}
