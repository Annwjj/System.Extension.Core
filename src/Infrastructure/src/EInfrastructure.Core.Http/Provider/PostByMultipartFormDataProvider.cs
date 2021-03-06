﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Http.Enumerations;
using RestSharp;

namespace EInfrastructure.Core.Http.Provider
{
    /// <summary>
    ///
    /// </summary>
    public class PostByMultipartFormDataProvider : BaseProvider, IProvider
    {
        #region 得到请求

        /// <summary>
        /// 得到请求
        /// </summary>
        /// <param name="method">方法类型</param>
        /// <param name="url">地址</param>
        /// <param name="requestBody">数据</param>
        /// <param name="headers">请求头</param>
        /// <param name="timeOut">超时限制</param>
        /// <returns></returns>
        public RestRequest GetRequest(Method method, string url, RequestBody requestBody,
            Dictionary<string, string> headers,
            int timeOut)
        {
            RestRequest request = base.GetRestRequest(url, method, timeOut, headers);
            request.AddHeader("Content-Type", "multipart/form-data");
            var bodyDic = base.GetParams(requestBody.Data);
            foreach (var item in bodyDic)
            {
                request.AddParameter(item.Key, item.Value);
            }

            foreach (var file in requestBody.Files)
            {
                request.AddFile(file.Name, file.FileByte, file.FileName, file.ContextType);
            }

            return request;
        }

        #endregion
    }
}
