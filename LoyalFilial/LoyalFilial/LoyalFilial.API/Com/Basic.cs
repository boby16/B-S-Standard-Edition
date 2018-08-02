using LoyalFilial.Framework.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text; 
using System.Linq;

namespace LoyalFilial.APIService.Com
{
    public class Basic
    {
        #region  get web client ip
        #endregion        

        #region json object stream
        /// <summary>
        /// return json object stream
        /// </summary>
        /// <param name="ObjectToJson"></param>
        /// <returns></returns>
        public static Stream FunctionReturnDeal(string objectToJson)
        {
            string jsCode = "josnpCallbackFunctionInWCF(" + objectToJson + ")";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsCode));
        }
        #endregion  
      
        #region json string
        /// <summary>
        /// return json string
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string JosnReturnDeal(string Key,string Value)
        {
            return "{\"" + Key + "\":" + Value + "}";
        }
        #endregion
    }
}