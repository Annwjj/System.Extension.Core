// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Attributes;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    ///
    /// </summary>
    public class CustomAttributeCommonUnitTest
    {
        [Fact]
        public void GetCustomAttributeValue()
        {
            string result =
                CustomAttributeCommon<ENameAttribute, string>.GetCustomAttributeValue(typeof(UserItem),
                    x => x.Name, "Name");//输出EName
            string result2 =
                CustomAttributeCommon<EDescribeAttribute, string>.GetCustomAttributeValue(typeof(UserItem),
                    x => x.Describe, "Name");//输出EDescribe2
            string result3 =
                CustomAttributeCommon<EVersionAttribute, string>.GetCustomAttributeValue(typeof(UserItem),
                    x => x.Version);//输出EVersion3
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        [EVersion("EVersion3")]
        public class UserItem
        {
            /// <summary>
            /// 描述
            /// </summary>
            [EName("描述")]
            public string Desc { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            [EName("EName")]
            [EDescribe("EDescribe2")]
            public string Name { get; set; }
        }
    }
}
