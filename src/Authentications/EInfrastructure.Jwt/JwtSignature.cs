using EInfrastructure.Core.Config.ExceptionExtensions;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Tools;

namespace EInfrastructure.Tools.Jwt
{
    /// <summary>
    ///
    /// </summary>
    public class JwtSignature
    {
        private readonly IJsonService _jsonService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="jsonService"></param>
        public JwtSignature(IJsonService jsonService)
        {
            _jsonService = jsonService;
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="jsonService"></param>
        /// <param name="headers">请求头</param>
        /// <param name="claims">请求参数</param>
        /// <param name="secretKey">秘钥</param>
        public JwtSignature(IJsonService jsonService, JwtHeaders headers, JwtPlyload claims, string secretKey)
        {
            Alg = headers.Alg;
            Content = $"{headers.SerializeToJson()}.{claims.SerializeToJson()}";
            SecretKey = secretKey;
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="content">请求头与请求参数加密后的值</param>
        /// <param name="secretKey">秘钥</param>
        /// <param name="alg">加密算法，默认：HS256</param>
        public JwtSignature(string content, string secretKey, string alg)
        {
            Content = content;
            SecretKey = secretKey;
            Alg = alg;
        }

        /// <summary>
        /// 请求头与请求参数加密后的值
        /// </summary>
        private string Content { get; set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        private string SecretKey;

        /// <summary>
        /// 加密算法，默认：HS1,HS256,HS512
        /// </summary>
        private string Alg { get; set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        private string Signature;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Signature))
            {
                switch (Alg)
                {
                    case "HS1":
                        Signature = SecurityCommon.HMacSha1(Content, SecretKey);
                        break;
                    case "HS256":
                        Signature = SecurityCommon.HMacSha256(Content, SecretKey);
                        break;
                    case "HS384":
                        Signature = SecurityCommon.HMacSha384(Content, SecretKey);
                        break;
                    case "HS512":
                        Signature = SecurityCommon.HMacSha512(Content, SecretKey);
                        break;
                    default:
                        throw new BusinessException("不支持的加密算法");
                }
            }

            return Signature;
        }

        /// <summary>
        /// 转为Json
        /// </summary>
        /// <returns></returns>
        public virtual string SerializeToJson()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return Base64UrlEncoder.Encode(_jsonService.Serializer(Signature));
        }
    }
}
