﻿using Newtonsoft.Json;

 namespace EInfrastructure.Tools.Jwt
{
    /// <summary>
    ///
    /// </summary>
    public class JwtResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public JwtResponse()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <param name="token"></param>
        /// <param name="expiresIn"></param>
        public JwtResponse(string accessToken, string refreshToken, string token, long expiresIn)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Token = token;
            ExpiresIn = expiresIn;
        }

        /// <summary>
        /// 调用凭证
        /// </summary>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 调用凭证
        /// </summary>
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 接口调用凭证超时时间，单位（秒）
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public long ExpiresIn { get; set; }

        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}
