using System;
using System.Globalization;
using EInfrastructure.Core.Config.ExceptionExtensions;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Tools;

namespace EInfrastructure.Tools.Jwt
{
    /// <summary>
    /// Jwt
    /// </summary>
    public class Jwt
    {
        private readonly IJsonService _jsonService;
        private readonly string _secrectKey;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonService"></param>
        /// <param name="secrectKey">秘钥</param>
        public Jwt(IJsonService jsonService, string secrectKey)
        {
            _jsonService = jsonService;
            _secrectKey = secrectKey;
        }

        /// <summary>
        /// 得到Token
        /// </summary>
        /// <param name="headers">请求头</param>
        /// <param name="claims">参数</param>
        /// <param name="secretKey">秘钥</param>
        /// <returns></returns>
        public JwtResponse GetToken(JwtHeaders headers, JwtPlyload claims, string secretKey)
        {
            return new JwtResponse(GetAccessToken(headers, claims, secretKey),
                GetRefreshToken(headers, claims, secretKey), claims.Token,
                long.Parse((TimeCommon.JsTimeStampToDateTime(claims.ExpireTime) -
                            TimeCommon.JsTimeStampToDateTime(claims.CreateTime)).TotalSeconds
                    .ToString(CultureInfo.InvariantCulture)));
        }

        #region 校验Jwt

        /// <summary>
        /// 校验Jwt
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <param name="plyloadAction"></param>
        /// <returns></returns>
        public bool IsVerify(string jwtToken, Action<JwtHeaders, JwtPlyload> plyloadAction = null)
        {
            if (string.IsNullOrEmpty(jwtToken) || jwtToken.Split('.').Length != 3)
            {
                return false;
            }

            var header = jwtToken.Split('.')[0];
            var content = jwtToken.Split('.')[1];
            var headerObj = Get<JwtHeaders>(header);
            var contentObj = Get<JwtPlyload>(content);
            if (headerObj == null || contentObj == null || string.IsNullOrEmpty(jwtToken.Split('.')[2]) ||
                GetAccessToken(headerObj, contentObj, _secrectKey) != jwtToken)
            {
                return false;
            }

            plyloadAction?.Invoke(headerObj, contentObj);
            return true;
        }

        #endregion

        #region 刷新Token有效期

        /// <summary>
        /// 刷新Token有效期
        /// </summary>
        /// <param name="refreshToken">刷新Token</param>
        /// <param name="accessToken"></param>
        /// <param name="secretKey">安全</param>
        /// <returns></returns>
        /// <exception cref="AuthException"></exception>
        public JwtResponse RefreshToken(string refreshToken, string accessToken, string secretKey)
        {
            JwtHeaders accessHeaders = null, refreshHeader = null;
            JwtPlyload accessClaims = null, refreshClaims = null;
            if (!IsVerify(accessToken, (header, plyload) =>
            {
                accessHeaders = header;
                accessClaims = plyload;
            }) || !IsVerify(accessToken, (header, plyload) =>
            {
                refreshHeader = header;
                refreshClaims = plyload;
            }))
            {
                throw new AuthException();
            }

            if (refreshClaims.ExpireTime < DateTime.Now.GetTimeSpan() ||
                _jsonService.Serializer(accessHeaders) != _jsonService.Serializer(refreshHeader))
            {
                throw new AuthException();
            }

            return GetToken(accessHeaders,
                new JwtPlyload(_jsonService, "all", accessClaims.Token, DateTime.Now.GetTimeSpan(),
                    DateTime.Now.AddDays(1).GetTimeSpan()), secretKey);
        }

        #endregion

        #region private methods

        #region 得到信息

        /// <summary>
        /// 得到信息
        /// </summary>
        /// <param name="param"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T Get<T>(string param) where T : class, new()
        {
            if (string.IsNullOrEmpty(param))
            {
                return default(T);
            }

            return _jsonService.Deserialize<T>(Base64UrlEncoder.Decode(param));
        }

        #endregion

        #region 得到AccessToken

        /// <summary>
        /// 得到AccessToken
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="claims"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        private static string GetAccessToken(JwtHeaders headers, JwtPlyload claims, string secretKey)
        {
            var content = $"{headers.SerializeToJson()}.{claims.SerializeToJson()}";
            return $"{content}.{new JwtSignature(content, secretKey, headers.Alg).SerializeToJson()}";
        }

        #endregion

        #region 得到RefreshToken

        /// <summary>
        /// 得到RefreshToken
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="claims"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string GetRefreshToken(JwtHeaders headers, JwtPlyload claims, string secretKey)
        {
            var plyloadContent = claims.SerializeToJson((plyload) =>
            {
                plyload.ExpireTime = TimeCommon.JsTimeStampToDateTime(plyload.ExpireTime).AddDays(7).GetTimeSpan();
                plyload.Id = null;
            });
            var content = $"{headers.SerializeToJson()}.{plyloadContent}";
            return $"{content}.{new JwtSignature(content, secretKey, headers.Alg).SerializeToJson()}";
        }

        #endregion

        #endregion
    }
}