// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Reflection;

namespace EInfrastructure.Core.Tools.Component
{
    /// <summary>
    /// 服务提供者
    /// </summary>
    public interface IServiceProvider
    {
        /// <summary>
        /// 得到服务集合
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns></returns>
        IEnumerable<TService> GetServices<TService>() where TService : class;

        /// <summary>
        /// 得到服务集合
        /// </summary>
        /// <param name="assemblies"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        IEnumerable<TService> GetServices<TService>(Assembly[] assemblies) where TService : class;

        /// <summary>
        /// 得到服务
        /// </summary>
        /// <returns></returns>
        TService GetService<TService>(Assembly[] assemblies) where TService : class;

        /// <summary>
        /// 得到服务
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns></returns>
        TService GetService<TService>() where TService : class;
    }
}
