using System.Data;

namespace Nit.Data
{
    /// <summary>
    /// 摘要: 继承 IDataParameter 的抽象参数类.
    /// </summary>
    public class Parameter : IDataParameter
    {
        private ParameterDirection _Direction = ParameterDirection.Input;
        private string _ParameterName;
        private object _Value;
        private string _SourceColumn;
        private DataRowVersion _SourceVersion;
        private bool _IsNullable = true;
        private DbType _DbType;
        /// <summary>
        /// 摘要: Parameter 构造函数.
        /// </summary>
        /// <param name="parameterName">parameterName</param>
        /// <param name="parameterValue">parameterValue</param>
        public Parameter(string parameterName, object parameterValue)
        {
            this._ParameterName = parameterName;
            this._Value = parameterValue;
        }
        /// <summary>
        /// 摘要: Parameter 构造函数. 
        /// </summary>
        /// <param name="parameterName">parameterName</param>
        /// <param name="parameterValue">parameterValue</param>
        /// <param name="direction">ParameterDirection</param>
        public Parameter(string parameterName, object parameterValue, ParameterDirection direction)
        {
            this._ParameterName = parameterName;
            this._Value = parameterValue;
            this._Direction = direction;
        }
        /// <summary>
        /// 摘要: Parameter 构造函数. 
        /// </summary>
        /// <param name="parameterName">parameterName</param>
        /// <param name="parameterValue">parameterValue</param>
        /// <param name="direction">ParameterDirection</param>
        /// <param name="dbType">DbType</param>
        /// <param name="sourceColumn">SourceColumn</param>
        /// <param name="sourceVersion">DataRowVersion</param>
        public Parameter(string parameterName, object parameterValue, ParameterDirection direction, DbType dbType, string sourceColumn, DataRowVersion sourceVersion)
        {
            this._ParameterName = parameterName;
            this._Value = parameterValue;
            this._Direction = direction;
            this._DbType = dbType;
            this._SourceColumn = sourceColumn;
            this._SourceVersion = sourceVersion;
        }
        /// <summary>
        /// 摘要: DbType
        /// </summary>
        public DbType DbType
        {
            get
            {
                return _DbType;
            }
            set
            {
                _DbType = value;
            }
        }
        /// <summary>
        /// 摘要: ParameterDirection
        /// </summary>
        public ParameterDirection Direction
        {
            get
            {
                return this._Direction;
            }
            set
            {
                this._Direction = value;
            }
        }
        /// <summary>
        /// 摘要: IsNullable
        /// </summary>
        public bool IsNullable
        {
            get { return _IsNullable; }
        }
        /// <summary>
        /// 摘要: ParameterName
        /// </summary>
        public string ParameterName
        {
            get
            {
                return _ParameterName;
            }
            set
            {
                _ParameterName = value;
            }
        }
        /// <summary>
        /// 摘要: SourceColumn
        /// </summary>
        public string SourceColumn
        {
            get
            {
                return _SourceColumn;
            }
            set
            {
                _SourceColumn = value;
            }
        }
        /// <summary>
        /// 摘要: DataRowVersion
        /// </summary>
        public DataRowVersion SourceVersion
        {
            get
            {
                return _SourceVersion;
            }
            set
            {
                _SourceVersion = value;
            }
        }
        /// <summary>
        /// 摘要: Value
        /// </summary>
        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
    }
}
