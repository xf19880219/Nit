using System.Data.Common;
using System.Data;
using System.Globalization;

namespace Nit.Data
{
    /// <summary>
    /// 摘要: 定义多数据库的抽象基类,该类实现IExecuteable接口,此接口提供更为简便的 Ado.net 操作.
    /// </summary>
    abstract class Database : IExecuteable
    {
        private static readonly ParameterCache dbParameters = new ParameterCache();
        private readonly DbProviderFactory dbProviderFactory;
        private readonly string connectionString;

        /// <summary>
        /// 摘要: 实例化数据库操作基类
        /// </summary>
        /// <param name="connectionString">指定数据库连接字符串</param>
        /// <param name="dbProviderFactory">提供DbProviderFactory</param>
        protected Database(string connectionString, DbProviderFactory dbProviderFactory)
        {
            this.dbProviderFactory = dbProviderFactory;
            this.connectionString = connectionString;
        }
        /// <summary>
        /// 摘要: 创建参数数组.
        /// </summary>
        /// <param name="parameterCollection">ParameterCollection</param>
        /// <param name="sql">SQL</param>
        /// <returns>IDataParameter[]</returns>
        private IDataParameter[] CreateParameterArray(ParameterCollection parameterCollection, string sql)
        {
            IDataParameter[] dbParameterArray = new IDataParameter[parameterCollection.Count];
            if (dbParameters.AlreadyCached(connectionString, sql))
            {
                IDataParameter[] dataParameters = dbParameters.GetParametersFromCached(connectionString, sql);
                for (int index = 0; index < dataParameters.Length; index++)
                {
                    dataParameters[index].Value = parameterCollection[index].Value;
                }
                return dataParameters;
            }
            else
            {
                int indexParameter = 0;
                foreach (Parameter parameter in parameterCollection.Parameters)
                {
                    dbParameterArray[indexParameter] = this.CreateParameter();
                    dbParameterArray[indexParameter].ParameterName = parameter.ParameterName;
                    dbParameterArray[indexParameter].Value = parameter.Value;
                    dbParameterArray[indexParameter].Direction = parameter.Direction;
                    indexParameter++;
                }
                dbParameters.AddParameterInCache(connectionString, sql, dbParameterArray);
                return dbParameterArray;
            }
        }
        /// <summary>
        /// 摘要: 给参数赋值.
        /// </summary>
        /// <param name="dbCommand">DbCommand</param>
        /// <param name="parameterCollection">ParameterCollection</param>
        private void SetParameters(DbCommand dbCommand, ParameterCollection parameterCollection)
        {
            foreach (Parameter parameter in parameterCollection.Parameters)
            {
                if (dbCommand.Parameters[parameter.ParameterName].Direction != ParameterDirection.Input)
                {
                    parameter.Value = dbCommand.Parameters[parameter.ParameterName].Value;
                }
            }
        }
        /// <summary>
        /// 摘要: 创建连接字符传基类 DbConnection
        /// </summary>
        /// <returns></returns>
        public virtual DbConnection CreateConnection()
        {
            DbConnection dbConnection = dbProviderFactory.CreateConnection();
            dbConnection.ConnectionString = this.connectionString;
            return dbConnection;
        }
        /// <summary>
        /// 摘要: 根据数据库支持类创建变量 IDataParameter
        /// </summary>
        /// <returns></returns>
        public virtual IDataParameter CreateParameter()
        {
            return this.dbProviderFactory.CreateParameter();
        }
        /// <summary>
        /// 摘要: 根据 DbProviderFactory 创建 DbCommand 实例
        /// </summary>
        /// <returns></returns>
        public virtual DbCommand CreateCommand()
        {
            return this.CreateConnection().CreateCommand();
        }
        /// <summary>
        /// 摘要: 根据 DbProviderFactory 创建 DbDataAdapter 实例
        /// </summary>
        /// <returns></returns>
        public virtual DbDataAdapter CreateAdapter()
        {
            DbDataAdapter dbDataAdapter = this.dbProviderFactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = this.CreateCommand();
            return dbDataAdapter;
        }
        /// <summary>
        /// 摘要: 打开数据库操作实例现有连接
        /// </summary>
        /// <param name="dbConnection"></param>
        public virtual void OpenConnection(DbConnection dbConnection)
        {
            if (dbConnection != null && dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }
        /// <summary>
        /// 摘要: 关闭数据库操作实例现有连接
        /// </summary>
        /// <param name="dbConnection"></param>
        public virtual void CloseConnection(DbConnection dbConnection)
        {
            if (dbConnection != null && dbConnection.State != ConnectionState.Closed)
            {
                dbConnection.Close();
            }
        }
        /// <summary>
        /// 摘要: 根据SQL返回 IDataReader
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>IDataReader</returns>
        public virtual IDataReader ExecuteDataReader(string sql)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.CommandText = sql;
            OpenConnection(dbCommand.Connection);
            return dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 摘要: 根据SQL，ParameterCollection返回 IDataReader
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>IDataReader</returns>
        public virtual IDataReader ExecuteDataReader(string sql, ParameterCollection parameters)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.CommandText = sql;
            dbCommand.Parameters.AddRange(CreateParameterArray(parameters, sql));
            return dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 摘要: 根据SQL返回 DataSet数据集
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>DataSet</returns>
        public virtual DataSet ExecuteDataSet(string sql)
        {
            using (DataSet dataSet = new DataSet())
            {
                dataSet.Locale = CultureInfo.InvariantCulture;
                dataSet.CaseSensitive = true;
                DbDataAdapter dataAdapter = this.CreateAdapter();
                dataAdapter.SelectCommand.CommandText = sql;
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }
        /// <summary>
        /// 摘要: 根据SQL，ParameterCollection返回 DataSet数据集
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>DataSet</returns>
        public virtual DataSet ExecuteDataSet(string sql, ParameterCollection parameters)
        {
            using (DataSet dataSet = new DataSet())
            {
                dataSet.Locale = CultureInfo.InvariantCulture;
                DbDataAdapter dataAdapter = this.CreateAdapter();
                dataAdapter.SelectCommand.CommandText = sql;
                dataAdapter.SelectCommand.Parameters.AddRange(CreateParameterArray(parameters, sql));
                dataAdapter.Fill(dataSet);
                SetParameters(dataAdapter.SelectCommand, parameters);
                return dataSet;
            }
        }
        /// <summary>
        /// 摘要: 根据SQL返回 DataTable数据集
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>DataTable</returns>
        public virtual DataTable ExecuteDataTable(string sql)
        {
            using (DataTable dataTable = new DataTable())
            {
                dataTable.Locale = CultureInfo.InvariantCulture;
                dataTable.CaseSensitive = true;
                DbDataAdapter dataAdapter = this.CreateAdapter();
                dataAdapter.SelectCommand.CommandText = sql;
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        /// <summary>
        /// 摘要: 根据SQL，ParameterCollection返回 DataTable数据集
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>DataTable</returns>
        public virtual DataTable ExecuteDataTable(string sql, ParameterCollection parameters)
        {
            using (DataTable dataTable = new DataTable())
            {
                dataTable.Locale = CultureInfo.InvariantCulture;
                dataTable.CaseSensitive = true;
                DbDataAdapter dataAdapter = this.CreateAdapter();
                dataAdapter.SelectCommand.CommandText = sql;
                dataAdapter.SelectCommand.Parameters.AddRange(CreateParameterArray(parameters, sql));
                dataAdapter.Fill(dataTable);
                SetParameters(dataAdapter.SelectCommand, parameters);
                return dataTable;
            }
        }
        /// <summary>
        /// 摘要: 根据 key,pageSize,pageIndex,sql 分页返回 DataTable 值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="sql">sql</param>
        /// <param name="count">count</param>
        /// <returns>DataTable</returns>
        public virtual DataTable Pager(string key, int pageSize, int pageIndex, string sql, out int count)
        {
            return Pager(key, pageSize, pageIndex, sql, out count, key);
        }
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
        public abstract DataTable Pager(string key, int pageSize, int pageIndex, string sql, out int count, string orderBy);
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
        public virtual DataTable Pager(string key, int pageSize, int pageIndex, string sql, out int count, ParameterCollection parameters)
        {
            return Pager(key, pageSize, pageIndex, sql, out count, key, parameters);
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
        /// <returns>DataTable</returns>
        public abstract DataTable Pager(string key, int pageSize, int pageIndex, string sql, out int count, string orderBy, ParameterCollection parameters);
        /// <summary>
        /// 摘要: 根据SQL返回影响行数.
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>int</returns>
        public virtual int ExecuteNonQuery(string sql)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.CommandText = sql;
            OpenConnection(dbCommand.Connection);
            int res = dbCommand.ExecuteNonQuery();
            CloseConnection(dbCommand.Connection);
            return res;
        }
        /// <summary>
        /// 摘要: 根据SQL，ParameterCollection返回 影响行数.
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>int</returns>
        public virtual int ExecuteNonQuery(string sql, ParameterCollection parameters)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.Parameters.AddRange(CreateParameterArray(parameters, sql));
            dbCommand.CommandText = sql;
            OpenConnection(dbCommand.Connection);
            int res = dbCommand.ExecuteNonQuery();
            SetParameters(dbCommand, parameters);
            CloseConnection(dbCommand.Connection);
            return res;
        }
        /// <summary>
        /// 摘要: 根据SQL返回 object值
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>object</returns>
        public virtual object ExecuteScalar(string sql)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.CommandText = sql;
            OpenConnection(dbCommand.Connection);
            object res = dbCommand.ExecuteScalar();
            CloseConnection(dbCommand.Connection);
            return res;
        }
        /// <summary>
        /// 摘要: 根据SQL，ParameterCollection返回 object值
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>object</returns>
        public virtual object ExecuteScalar(string sql, ParameterCollection parameters)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.CommandText = sql;
            dbCommand.Parameters.AddRange(CreateParameterArray(parameters, sql));
            OpenConnection(dbCommand.Connection);
            object res = dbCommand.ExecuteScalar();
            SetParameters(dbCommand, parameters);
            CloseConnection(dbCommand.Connection);
            return res;
        }
        /// <summary>
        /// 摘要: 调用存储 Procedure 
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <returns>IDataReader</returns>
        public virtual IDataReader ExecuteProcDataReader(string procedureName)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.CommandText = procedureName;
            dbCommand.CommandType = CommandType.StoredProcedure;
            OpenConnection(dbCommand.Connection);
            return dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 摘要: 调用存储 Procedure 
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>IDataReader</returns>
        public virtual IDataReader ExecuteProcDataReader(string procedureName, ParameterCollection parameters)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.CommandText = procedureName;
            dbCommand.CommandType = CommandType.StoredProcedure;
            dbCommand.Parameters.AddRange(CreateParameterArray(parameters, procedureName));
            OpenConnection(dbCommand.Connection);
            return dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 摘要: 调用存储 Procedure 
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <returns>DataSet</returns>
        public virtual DataSet ExecuteProcDataSet(string procedureName)
        {
            using (DataSet dataSet = new DataSet())
            {
                dataSet.Locale = CultureInfo.InvariantCulture;
                DbDataAdapter dataAdapter = this.CreateAdapter();
                dataAdapter.SelectCommand.CommandText = procedureName;
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }
        /// <summary>
        /// 摘要: 调用存储 Procedure 
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>DataSet</returns>
        public virtual DataSet ExecuteProcDataSet(string procedureName, ParameterCollection parameters)
        {
            using (DataSet dataSet = new DataSet())
            {
                dataSet.Locale = CultureInfo.InvariantCulture;
                DbDataAdapter dataAdapter = this.CreateAdapter();
                dataAdapter.SelectCommand.CommandText = procedureName;
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.AddRange(CreateParameterArray(parameters, procedureName));
                dataAdapter.Fill(dataSet);
                SetParameters(dataAdapter.SelectCommand, parameters);
                return dataSet;
            }
        }
        /// <summary>
        /// 摘要: 调用存储 Procedure
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <returns>DataTable</returns>
        public virtual DataTable ExecuteProcDataTable(string procedureName)
        {
            using (DataTable dataTable = new DataTable())
            {
                dataTable.Locale = CultureInfo.InvariantCulture;
                dataTable.CaseSensitive = true;
                DbDataAdapter dataAdapter = this.CreateAdapter();
                dataAdapter.SelectCommand.CommandText = procedureName;
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        /// <summary>
        /// 摘要: 调用存储 Procedure
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>DataTable</returns>
        public virtual DataTable ExecuteProcDataTable(string procedureName, ParameterCollection parameters)
        {
            using (DataTable dataTable = new DataTable())
            {
                dataTable.Locale = CultureInfo.InvariantCulture;
                dataTable.CaseSensitive = true;
                DbDataAdapter dataAdapter = this.CreateAdapter();
                dataAdapter.SelectCommand.CommandText = procedureName;
                dataAdapter.SelectCommand.Parameters.AddRange(CreateParameterArray(parameters, procedureName));
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.Fill(dataTable);
                SetParameters(dataAdapter.SelectCommand, parameters);
                return dataTable;
            }
        }
        /// <summary>
        /// 摘要: 调用存储 Procedure
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <returns>int</returns>
        public virtual int ExecuteProcNonQuery(string procedureName)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.CommandType = CommandType.StoredProcedure;
            dbCommand.CommandText = procedureName;
            OpenConnection(dbCommand.Connection);
            int res = dbCommand.ExecuteNonQuery();
            CloseConnection(dbCommand.Connection);
            return res;
        }
        /// <summary>
        /// 摘要: 调用存储 Procedure
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>int</returns>
        public virtual int ExecuteProcNonQuery(string procedureName, ParameterCollection parameters)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.CommandType = CommandType.StoredProcedure;
            dbCommand.CommandText = procedureName;
            dbCommand.Parameters.AddRange(CreateParameterArray(parameters, procedureName));
            OpenConnection(dbCommand.Connection);
            int res = dbCommand.ExecuteNonQuery();
            SetParameters(dbCommand, parameters);
            CloseConnection(dbCommand.Connection);
            return res;
        }
        /// <summary>
        /// 摘要: 调用存储 procedureName
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <returns>object</returns>
        public virtual object ExecuteProcScalar(string procedureName)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.CommandType = CommandType.StoredProcedure;
            dbCommand.CommandText = procedureName;
            OpenConnection(dbCommand.Connection);
            object res = dbCommand.ExecuteScalar();
            CloseConnection(dbCommand.Connection);
            return res;
        }
        /// <summary>
        /// 摘要: 调用存储 Procedure
        /// </summary>
        /// <param name="procedureName">procedureName</param>
        /// <param name="parameters">ParameterCollection</param>
        /// <returns>object</returns>
        public virtual object ExecuteProcScalar(string procedureName, ParameterCollection parameters)
        {
            DbCommand dbCommand = this.CreateCommand();
            dbCommand.CommandType = CommandType.StoredProcedure;
            dbCommand.CommandText = procedureName;
            dbCommand.Parameters.AddRange(CreateParameterArray(parameters, procedureName));
            OpenConnection(dbCommand.Connection);
            object res = dbCommand.ExecuteScalar();
            SetParameters(dbCommand, parameters);
            CloseConnection(dbCommand.Connection);
            return res;
        }
    }
}