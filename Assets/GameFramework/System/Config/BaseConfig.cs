/// <summary>
/// 配置文件基类
/// </summary>
namespace GameFramework
{
    public abstract class BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public virtual object UniqueID
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }

}
