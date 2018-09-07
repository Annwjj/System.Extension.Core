using EInfrastructure.Core.Interface.Storage.Config;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.Interface.Storage.Param
{
    /// <summary>
    /// �����ļ��ϴ�
    /// </summary>
    public class UploadByFormFileParam
    {
        public UploadByFormFileParam(string key, IFormFile file, UploadPersistentOps uploadPersistentOps = null)
        {
            Key = key;
            File = file;
            UploadPersistentOps = uploadPersistentOps;
        }

        /// <summary>
        /// �ļ�key
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// �ļ���
        /// </summary>
        public IFormFile File { get; private set; }

        /// <summary>
        /// �ϴ�����
        /// </summary>
        public UploadPersistentOps UploadPersistentOps { get; private set; }
    }
}