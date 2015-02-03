using System;
using System.Collections;
using System.Data;

namespace Nit.Data
{
    internal class CachingMechanism
    {
        private Hashtable parameterCache =  Hashtable.Synchronized(new Hashtable());

        private static string CreateKey(string connectionString, string commandString)
        {
            return connectionString + ":" + commandString;
        }

        public void AddParameterInCache(string connectionString, string commandString, IDataParameter[] parameters)
        {
            string key = CreateKey(connectionString, commandString);
            this.parameterCache.Add(key, parameters);
        }

        public IDataParameter[] GetParameterFormCache(string connectionString, string commandString)
        {
            string key = CreateKey(connectionString, commandString);
            IDataParameter[] parameters = (IDataParameter[])(this.parameterCache[key]);
            IDataParameter[] parametersClone = new IDataParameter[parameters.Length];
            for (int index = 0; index < parameters.Length; index++)
            {
                parametersClone[index] = (IDataParameter)((ICloneable)parameters[index]).Clone();
            }
            return parametersClone;
        }

        public bool IsParameterSetCached(string connectionString, string commandString)
        {
            string key = CreateKey(connectionString, commandString);
            return this.parameterCache.ContainsKey(key);
        }

        public void Clear()
        {
            this.parameterCache.Clear();
        }
    }
}
