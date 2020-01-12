using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Tools;
using Newtonsoft.Json;

namespace EInfrastructure.Tools.Jwt
{
    /// <summary>
    /// 头信息
    /// </summary>
    public class JwtHeaders
    {
        private readonly IJsonService _jsonService;

        /// <summary>
        ///
        /// </summary>
        public JwtHeaders(){}

        /// <summary>
        ///
        /// </summary>
        /// <param name="jsonService"></param>
        public JwtHeaders(IJsonService jsonService)
        {
            _jsonService = jsonService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="alg">加密算法，默认：HS256</param>
        /// <param name="type">类别 JWT</param>
        public JwtHeaders(string alg = "HS256", string type = "JWT")
        {
            Alg = alg;
            Type = type;
        }

        /// <summary>
        /// 加密算法，默认：HS256
        /// </summary>
        [JsonProperty(PropertyName = "alt")]
        public string Alg { get; set; }

        /// <summary>
        /// 类别 JWT
        /// </summary>
        [JsonProperty(PropertyName = "typ")]
        public string Type { get; set; }

        /// <summary>
        /// 转为Json
        /// </summary>
        /// <returns></returns>
        public virtual string SerializeToJson()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return Base64UrlEncoder.Encode(_jsonService.Serializer(this));
        }
    }
}
