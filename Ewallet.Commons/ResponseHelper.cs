using Ewallet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.ModelBinding;

namespace Ewallet.Commons
{
    public static class ResponseHelper
    {
        public static ResponseDTO<T> CreateResponse<T>(string message, T data,bool status,Exception error=null)
        {
            var res = new ResponseDTO<T>();
            res.Message = message;
            res.Data = data;
            res.IsSuccessful = status;
  
            if(error!=null)
                res.Errors.Add("error", new List<string>() { error.Message, error.StackTrace, error.Source, error.HelpLink });
            return res;
        }



        private static Dictionary<string, List<String>> CreateErrorFromModelState(ModelStateDictionary modelState)
        {
            if (modelState == null)
                return null;



            var dictionary = new Dictionary<string, List<string>>();
            foreach (var error in modelState)
            {
                dictionary[error.Key] = error.Value.Errors.Select(x => x.ErrorMessage).ToList();
            }



            return dictionary;
        }
    }
}
