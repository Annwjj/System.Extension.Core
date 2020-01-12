using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Tools;
using Newtonsoft.Json;

namespace EInfrastructure.Tools.Jwt
{
    /// <summary>
    /// 参数信息
    /// </summary>
    public class JwtPlyload
    {
        private readonly IJsonService _jsonService;

        /// <summary>
        /// 
        /// </summary>
        public JwtPlyload()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jsonService"></param>
        public JwtPlyload(IJsonService jsonService)
        {
            _jsonService = jsonService;
            Id = DateTime.Now.GetTimeSpan().ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jsonService"></param>
        /// <param name="scope">范围</param>
        /// <param name="token">用户标识</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="expireTime">过期时间</param>
        public JwtPlyload(IJsonService jsonService, string scope, string token, long createTime, long expireTime) :
            this(jsonService)
        {
            Scope = scope;
            Token = token;
            CreateTime = createTime;
            ExpireTime = expireTime;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jsonService"></param>
        /// <param name="scope">范围</param>
        /// <param name="token">用户标识</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="expireTime">过期时间</param>
        public JwtPlyload(IJsonService jsonService, List<string> scope, string token, long createTime,
            long expireTime) : this(jsonService,
            scope?.Count > 1 ? (scope.ConvertListToString(',')) : scope?.FirstOrDefault() ?? "", token, createTime,
            expireTime)
        {
        }

        /// <summary>
        /// 范围
        /// </summary>
        [JsonProperty(PropertyName = "scope", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Scope { get; set; }

        /// <summary>
        /// jwt所面向的用户 www.example.com
        /// </summary>
        [JsonProperty(PropertyName = "sub", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Sub { get; set; }

        /// <summary>
        /// 接收jwt的一方 jrocket@example.com
        /// </summary>
        [JsonProperty(PropertyName = "aud", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Aud { get; set; }

        /// <summary>
        /// 签发者 John Wu JWT
        /// </summary>
        [JsonProperty(PropertyName = "iss", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Iss { get; set; }

        /// <summary>
        /// JWT ID 针对当前 token 的唯一标识
        /// </summary>
        [JsonProperty(PropertyName = "jti", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        [JsonProperty(PropertyName = "token", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Token { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty(PropertyName = "iat", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long CreateTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [JsonProperty(PropertyName = "exp", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long ExpireTime { get; set; }

        /// <summary>
        /// 转为Json
        /// </summary>
        /// <returns></returns>
        public string SerializeToJson(Action<JwtPlyload> action = null)
        {
            action?.Invoke(this);
            // ReSharper disable once SuspiciousTypeConversion.Global
            return Base64UrlEncoder.Encode(_jsonService.Serializer(this));
        }
    }
}
