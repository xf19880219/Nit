using System.Collections.Generic;

namespace Nit.Data
{
    /// <summary>
    /// 摘要: 为实现多数据操作提供的,DbParameter集合.
    /// </summary>
    public class ParameterCollection 
    {
        IList<Parameter> _Parameters = new List<Parameter>();
        /// <summary>
        /// 摘要: IList[Parameter]集合
        /// </summary>
        public IList<Parameter> Parameters
        {
            get { return _Parameters; }
        }
        /// <summary>
        /// 摘要: 获取指定 Parameter 的索引.
        /// </summary>
        /// <param name="item">Parameter</param>
        /// <returns>Int</returns>
        public int IndexOf(Parameter item)
        {
            return _Parameters.IndexOf(item);
        }
        /// <summary>
        /// 摘要: 在指定位置插入 Parameter.
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="item">Parameter</param>
        public void Insert(int index, Parameter item)
        {
            _Parameters.Insert(index, item);
        }
        /// <summary>
        /// 摘要: 移除指定索引位置的 Parameter.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _Parameters.RemoveAt(index);
        }
        /// <summary>
        /// 摘要: 根据索引值获得 Parameter.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Parameter</returns>
        public Parameter this[int index]
        {
            get
            {
                return _Parameters[index];
            }
            set
            {
                _Parameters[index] = value;
            }
        }
        /// <summary>
        /// 摘要: 向集合添加 Parameter.
        /// </summary>
        /// <param name="item">Parameter</param>
        public void Add(Parameter item)
        {
            _Parameters.Add(item);
        }
        /// <summary>
        /// 摘要: 向集合添加 Parameter.
        /// </summary>
        /// <param name="parameterName">parameterName</param>
        /// <param name="parameterValue">parameterValue</param>
        public void Add(string parameterName, object parameterValue)
        {
            Parameter Parameter = new Parameter(parameterName,parameterValue);
            _Parameters.Add(Parameter);
        }
        /// <summary>
        /// 摘要: 清除 ParameterCollection.
        /// </summary>
        public void Clear()
        {
            _Parameters.Clear();
        }
        /// <summary>
        /// 摘要: 是否存在指定 Parameters
        /// </summary>
        /// <param name="item">Parameter</param>
        /// <returns>bool</returns>
        public bool Contains(Parameter item)
        {
            return _Parameters.Contains(item);
        }
        /// <summary>
        /// 摘要: 指定位置批量插入 Parameter[].
        /// </summary>
        /// <param name="array">Parameter[]</param>
        /// <param name="arrayIndex">Index</param>
        public void CopyTo(Parameter[] array, int arrayIndex)
        {
            _Parameters.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// 摘要: Parameter数量.
        /// </summary>
        public int Count
        {
            get { return _Parameters.Count; }
        }
        /// <summary>
        /// 摘要: 是否只读.
        /// </summary>
        public bool IsReadOnly
        {
            get { return _Parameters.IsReadOnly; }
        }
        /// <summary>
        /// 摘要: 移除指定的Parameter.
        /// </summary>
        /// <param name="item">Parameter</param>
        /// <returns>bool</returns>
        public bool Remove(Parameter item)
        {
            return _Parameters.Remove(item);
        }
    }
}
