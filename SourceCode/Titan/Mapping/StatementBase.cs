using System.Collections.Generic; 
using System.Data;

namespace Titan
{
    public class StatementBase : IStatement
    {


        public CommandBehavior CommandBehavior { get; set; }
        public string CommandText { get; set; }
        public CommandType CommandType { get; set; }
        public Dictionary<string, IParameter> Parameters { get; set; }
        public Dictionary<string, IParameter> OutParameters { get; set; }




        public virtual void CreateCache()
        {

            //生成缓存 
            OutParameters = new Dictionary<string, IParameter>(Parameters.Count);
            foreach (KeyValuePair<string, IParameter> kv in Parameters)
            {
                if (kv.Value.Direction != ParameterDirection.Input)
                {
                    OutParameters.Add(kv.Key, kv.Value);
                }

            }
        }
    }
}
