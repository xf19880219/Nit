using System;
using System.Configuration;
using System.Reflection;

[assembly: CLSCompliant(true)]
namespace Nit.Data
{
    /// <summary>
    /// 摘要: 提供创建 Database 的工厂类.
    /// </summary>
    public static class DatabaseFactory
    {
        /// <summary>
        /// 摘要: 创建 IExecuteable 用于提供操作数据库的接口.
        /// </summary>
        /// <param name="connectionName">connectionName</param>
        /// <returns>IExecuteable</returns>
        public static IExecuteable CreateInstance(string connectionName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connectionName].ProviderName;
            return CreateInstance(connectionString, providerName);
        }
        /// <summary>
        /// 摘要: 创建 IExecuteable 用于提供操作数据库的接口.
        /// </summary>
        /// <param name="connectionString">connectionString</param>
        /// <param name="providerName">providerName</param>
        /// <returns>IExecuteable</returns>
        public static IExecuteable CreateInstance(string connectionString, string providerName)
        {
            providerName = ProviderFilter(providerName);
            return (IExecuteable)Assembly.Load((typeof(Database).Assembly.FullName)).CreateInstance(providerName, false, System.Reflection.BindingFlags.Default, null, new object[] { connectionString }, null, null);
        }
        /// <summary>
        /// 摘要: 根据系统默认生成的 ProviderName 映射为自定义的 ProviderName.
        /// </summary>
        /// <param name="providerName">providerName</param>
        /// <returns>Nit.Data.providerName</returns>
        private static string ProviderFilter(string providerName)
        {
            switch (providerName)
            {
                case "System.Data.Odbc": providerName = "Nit.Data.Odbc.OdbcClient"; break;
                case "System.Data.OleDb": providerName = "Nit.Data.OleDb.OleDbClient"; break;
                case "System.Data.SqlClient": providerName = "Nit.Data.SqlServer.SqlServerClient"; break;
                default: break;
            }
            return providerName;
        }
    }
}
