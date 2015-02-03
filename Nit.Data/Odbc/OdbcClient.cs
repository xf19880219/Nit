using System;
using System.Data.Odbc;
using System.Data;

namespace Nit.Data.Odbc
{
    /// <summary>
    ///  摘要: 根据 providerName 提供 System.Data.Odbc 操作对象.
    /// </summary>
    class OdbcClient : Database
    {
        /// <summary>
        ///  摘要: 根据 connectionString 提供 System.Data.Odbc 操作对象.
        /// </summary>
        /// <param name="connectionString">connectionString</param>
        public OdbcClient(string connectionString) : base(connectionString, OdbcFactory.Instance) { }
        /// <summary>
        /// 摘要: 根据 key,pageSize,pageIndex,sql,orderBy 分页返回 DataTable 值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="sql">sql</param>
        /// <param name="count">count</param>
        /// <param name="orderBy">orderBy</param>
        /// <returns>DataTable</returns>
        public override DataTable Pager(string key, int pageSize, int pageIndex, string sql, out int count, string orderBy)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 摘要: 根据 key,pageSize,pageIndex,sql,orderBy,ParameterCollection 分页返回 DataTable 值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="sql">sql</param>
        /// <param name="count">count</param>
        /// <param name="orderBy">orderBy</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>
        /// DataTable
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override DataTable Pager(string key, int pageSize, int pageIndex, string sql, out int count, string orderBy, ParameterCollection parameters)
        {
            throw new NotImplementedException();
        }
    }
}
