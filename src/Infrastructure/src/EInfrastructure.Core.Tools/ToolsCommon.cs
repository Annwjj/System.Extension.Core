using System.Text;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    ///
    /// </summary>
    public static class ToolsCommon
    {
        #region 得到默认encoding

        /// <summary>
        /// 得到默认encoding
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Encoding Get(this Encoding encoding)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return encoding;
        }

        #endregion

        #region 安全转换为字符串，去除两端空格，当值为null时返回空

        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回空
        /// </summary>
        /// <param name="param">参数</param>
        public static string SafeString(this object param)
        {
            return param?.ToString().Trim() ?? string.Empty;
        }

        #endregion
    }
}
