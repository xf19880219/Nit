using System.Data;

namespace Nit.Data
{
    /// <summary>
    /// 摘要: 用于缓存参数的类.
    /// </summary>
    public class ParameterCache
    {
        private CachingMechanism cache = new CachingMechanism();
        /// <summary>
        /// 摘要: 判断是否缓存参数.
        /// </summary>
        /// <param name="connectValue"></param>
        /// <param name="commandValue"></param>
        /// <returns></returns>
        public bool AlreadyCached(string connectValue, string commandValue)
        {
            return cache.IsParameterSetCached(connectValue, commandValue);
        }
        /// <summary>
        /// 摘要: 获得参数缓存数组.
        /// </summary>
        /// <param name="connectValue">connectValue</param>
        /// <param name="commandValue">commandValue</param>
        /// <returns>IDataParameter[]</returns>
        public IDataParameter[] GetParametersFromCached(string connectValue, string commandValue)
        {
            return cache.GetParameterFormCache(connectValue, commandValue);
        }
        /// <summary>
        /// 摘要: 将参数缓存.
        /// </summary>
        /// <param name="connectValue">connectValue</param>
        /// <param name="commandValue">commandValue</param>
        /// <param name="parameters">IDataParameter[]</param>
        public void AddParameterInCache(string connectValue, string commandValue, IDataParameter[] parameters)
        {
            cache.AddParameterInCache(connectValue, commandValue,parameters);
        }
        /// <summary>
        /// 摘要: 清除缓存.
        /// </summary>
        protected internal void Clear()
        {
            this.cache.Clear();
        } 
    }
}
