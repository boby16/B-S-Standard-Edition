namespace LoyalFilial.Framework.Core
{
    public interface IActResult
    {
        bool IsSucceed { get; set; }

        string ErrorMsg { get; set; }
    }

    public class ActResult : IActResult
    {
        public bool IsSucceed { get; set; }

        public string ErrorMsg { get; set; }

        public ActResult()
        {
            this.IsSucceed = true;
            this.ErrorMsg = string.Empty;
        }

        public ActResult(string errorMsg)
        {
            this.IsSucceed = false;
            this.ErrorMsg = errorMsg;
        }
    }

    public interface IActTResult<T> : IActResult where T : class
    {
        /// <summary>
        /// 返回数据实例
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        int ErrorCode { get; set; }
    }

    /// <summary>
    /// 统一返回泛型实例类定义
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActTResult<T> : IActTResult<T> where T : class
    {
        /// <summary>
        /// 返回数据实例
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 是否操作成功
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="isSucceed">是否成功</param>
        public ActTResult(T data, int errorCode, string errorMsg, bool isSucceed)
        {
            Data = data;
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;
            IsSucceed = isSucceed;
        }

        public ActTResult(T data)
        {
            Data = data;
            ErrorMsg = string.Empty;
            IsSucceed = true;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public ActTResult()
        {
            Data = default(T);
            IsSucceed = true;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        public ActTResult(string errorMsg)
        {
            Data = default(T);
            IsSucceed = false;
            ErrorMsg = errorMsg;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        public ActTResult(int errorCode)
        {
            Data = default(T);
            IsSucceed = false;
            ErrorCode = errorCode;
        }
    }
}