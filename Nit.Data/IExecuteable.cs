using System.Data.Common;
using System.Data;

namespace Nit.Data
{
    /// <summary>
    /// 摘要: 定义为Database基类执行简便化操作,所提供的接口.
    /// </summary>
    public interface IExecuteable
    {
        /// <summary>
        /// 摘要: 提供基础的Ado.Net需要的 DbDataAdapter 对象
        /// </summary>
        /// <returns>DbDataAdapter</returns>
        DbDataAdapter CreateAdapter();
        /// <summary>
        /// 摘要: 提供基础的Ado.Net需要的 IDataParameter 对象
        /// </summary>
        /// <returns>IDataParameter</returns>
        DbCommand CreateCommand();
        /// <summary>
        /// 摘要: 提供基础的Ado.Net需要的 DbConnection 对象
        /// </summary>
        /// <returns>DbConnection</returns>
        DbConnection CreateConnection();
        /// <summary>
        /// 摘要: 提供基础的Ado.Net需要的 DbParameter 对象
        /// </summary>
        /// <returns>DbParameter</returns>
        IDataParameter CreateParameter();
        /// <summary>
        /// 摘要: IExecuteable接口 打开 指定 DbConnection.
        /// </summary>
        /// <param name="dbConnection">DbConnection</param>
        void OpenConnection(DbConnection dbConnection);
        /// <summary>
        /// 摘要: IExecuteable接口 关闭 指定 DbConnection.
        /// </summary>
        /// <param name="dbConnection">DbConnection</param>
        void CloseConnection(DbConnection dbConnection);
        /// <summary>
        /// 摘要: 根据SQL返回 IDataReader
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>IDataReader</returns>
        IDataReader ExecuteDataReader(string sql);
        /// <summary>
        /// 摘要: 根据SQL,ParameterCollection返回 IDataReader
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>IDataReader</returns>
        IDataReader ExecuteDataReader(string sql, ParameterCollection parameters);
        /// <summary>
        /// 摘要: 根据SQL返回 DataSet 数据集
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>DataSet</returns>
        DataSet ExecuteDataSet(string sql);
        /// <summary>
        /// 摘要: 根据SQL,ParameterCollection返回 DataSet 数据集
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>DataSet</returns>
        DataSet ExecuteDataSet(string sql, ParameterCollection parameters);
        /// <summary>
        /// 摘要: 根据SQL返回 DataTable 数据集
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>DataTable</returns>
        DataTable ExecuteDataTable(string sql);
        /// <summary>
        /// 摘要: 根据SQL,ParameterCollection返回 DataTable
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>DataTable</returns>
        DataTable ExecuteDataTable(string sql, ParameterCollection parameters);
        /// <summary>
        /// 摘要: 根据SQL返回影响行数.
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>int</returns>
        int ExecuteNonQuery(string sql);
        /// <summary>
        /// 摘要: 根据 SQL，ParameterCollection 返回影响行数.
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>int</returns>
        int ExecuteNonQuery(string sql, ParameterCollection parameters);
        /// <summary>
        /// 摘要: 调用存储 Procedure 
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <returns>IDataReader</returns>
        IDataReader ExecuteProcDataReader(string procedureName);
        /// <summary>
        /// 摘要: 调用存储 Procedure 
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>IDataReader</returns>
        IDataReader ExecuteProcDataReader(string procedureName, ParameterCollection parameters);
        /// <summary>
        /// 摘要: 调用存储 Procedure 
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <returns>DataSet</returns>
        DataSet ExecuteProcDataSet(string procedureName);
        /// <summary>
        /// 摘要: 调用存储 Procedure 
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>DataSet</returns>
        DataSet ExecuteProcDataSet(string procedureName, ParameterCollection parameters);
        /// <summary>
        /// 摘要: 调用存储 Procedure
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <returns>DataTable</returns>
        DataTable ExecuteProcDataTable(string procedureName);
        /// <summary>
        /// 摘要: 调用存储 Procedure
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>DataTable</returns>
        DataTable ExecuteProcDataTable(string procedureName, ParameterCollection parameters);
        /// <summary>
        /// 摘要: 调用存储 Procedure
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <returns>int</returns>
        int ExecuteProcNonQuery(string procedureName);
        /// <summary>
        /// 摘要: 调用存储 Procedure
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>int</returns>
        int ExecuteProcNonQuery(string procedureName, ParameterCollection parameters);
        /// <summary>
        /// 摘要: 调用存储 Procedure
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <returns>object</returns>
        object ExecuteProcScalar(string procedureName);
        /// <summary>
        /// 摘要: 调用存储 Procedure
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>object</returns>
        object ExecuteProcScalar(string procedureName, ParameterCollection parameters);
        /// <summary>
        /// 摘要: 根据SQL返回 object值
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>object</returns>
        object ExecuteScalar(string sql);
        /// <summary>
        /// 摘要: 根据 SQL,ParameterCollection返回 object值
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>object</returns>
        object ExecuteScalar(string sql, ParameterCollection parameters);
        /// <summary>
        /// 摘要: 根据 key,pageSize,pageIndex,sql 分页返回 DataTable 值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="sql">sql</param>
        /// <param name="count">count</param>
        /// <returns>DataTable</returns>
        DataTable Pager(string key, int pageSize, int pageIndex, string sql, out int count);
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
        DataTable Pager(string key, int pageSize, int pageIndex, string sql, out int count, string orderBy);
        /// <summary>
        /// 摘要: 根据 key,pageSize,pageIndex,sql,orderBy,ParameterCollection 分页返回 DataTable 值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="sql">sql</param>
        /// <param name="count">count</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>DataTable</returns>
        DataTable Pager(string key, int pageSize, int pageIndex, string sql, out int count, ParameterCollection parameters);
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
        /// <returns>DataTable</returns>
        DataTable Pager(string key, int pageSize, int pageIndex, string sql, out int count, string orderBy, ParameterCollection parameters);
    }
}