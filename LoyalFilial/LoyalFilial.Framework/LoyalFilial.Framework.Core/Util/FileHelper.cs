using System.Text.RegularExpressions;
using System.IO;
using System.Web;

namespace LoyalFilial.Framework.Core.Util
{
    public class FileHelper
    {
        public static bool IsPhysicalPath(string path)
        {
            string RegexString = @"\b[a-z]:\\.* ";
            MatchCollection Matchs = Regex.Matches(path, RegexString, RegexOptions.IgnoreCase);
            if (Matchs.Count > 0)
                return true;
            return false;
        }


        /// <summary>
        /// 得到文件模板内容
        /// </summary>
        /// <param name="path">邮件模板相对路径</param>
        /// <param name="Name">邮件模板完整名称</param>
        /// <returns>模板内容</returns>
        public static string GetFileContent(string path, string name)
        {
            if (!IsPhysicalPath(path))
            {
                path = HttpContext.Current.Request.PhysicalApplicationPath + path;
            }
            return GetFileContent(string.Format("{0}\\{1}", path, name));
        }

        /// <summary>
        /// 得到文件模板内容
        /// </summary>
        /// <param name="fileName">文件模板完整名称</param>
        /// <returns></returns>
        public static string GetFileContent(string fileName)
        {
            string Content = null;
            if (File.Exists(fileName))
            {
                StreamReader reader = new StreamReader(fileName, System.Text.Encoding.Default, true);
                Content = reader.ReadToEnd();
                reader.Close();
            }
            return Content;
        }
    }
}
